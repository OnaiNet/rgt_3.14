using GpioCore;
using PaulTechGuy.RPi.GpioLib;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PiGpioConsoleHost
{
	public class GpioActionManager
	{
		private static readonly BlockingCollection<GpioActionBase> _queue = new BlockingCollection<GpioActionBase>();
		private static readonly Lazy<GpioActionManager> _instance
			= new Lazy<GpioActionManager>(() => new GpioActionManager());
		private static CancellationTokenSource _cancellationSource = null;
		private static Task _managerTask = null;

		// private to prevent direct instantiation.
		private GpioActionManager()
		{
		}

		// accessor for instance
		public static GpioActionManager Instance
		{
			get
			{
				return _instance.Value;
			}
		}

		public void Start()
		{
			_cancellationSource = new CancellationTokenSource();
			_managerTask = Task.Run(() => { ProcessQueue(_cancellationSource.Token); }, _cancellationSource.Token);
		}

		private void ProcessQueue(CancellationToken cancelToken)
		{
			while (!_queue.IsCompleted && !cancelToken.IsCancellationRequested)
			{
				GpioActionBase baseAction = null;
				// Blocks if number.Count == 0
				// IOE means that Take() was called on a completed collection.
				// Some other thread can call CompleteAdding after we pass the
				// IsCompleted check but before we call Take. 
				// In this example, we can simply catch the exception since the 
				// loop will break on the next iteration.
				try
				{
					baseAction = _queue.Take();
				}
				catch (InvalidOperationException) { }

				if (baseAction != null)
				{
					Process(baseAction);
				}
			}
		}

		private void Process(GpioActionBase baseAction)
		{
			if (baseAction is RgbSimpleAction)
			{
				HandleRbgSimpleAction(baseAction);
			}
		}

		public void Stop()
		{
			if (_managerTask != null)
			{
				_cancellationSource.Cancel();
				_queue.CompleteAdding();
				_managerTask.Wait();
				_managerTask = null;
			}
		}

		public void Enqueue(GpioActionBase action)
		{
			_queue.Add(action);
		}

		private void HandleRbgSimpleAction(GpioActionBase baseAction)
		{
			RgbSimpleAction action = (RgbSimpleAction)baseAction;
			Console.WriteLine($"Processing action: {nameof(this.HandleRbgSimpleAction)}");

			using (DigitalPin pin1 = GpioHeader.Instance.CreatePin(action.RgbPins[0], DigitalPinDirection.Output))
			using (DigitalPin pin2 = GpioHeader.Instance.CreatePin(action.RgbPins[1], DigitalPinDirection.Output))
			using (DigitalPin pin3 = GpioHeader.Instance.CreatePin(action.RgbPins[2], DigitalPinDirection.Output))
			{
				if (action.PreDelayMs > 0)
				{
					Thread.Sleep(action.PreDelayMs);
				}

				DigitalPin[] pins = new DigitalPin[] { pin1, pin2, pin3 };

				for (int loopCounter = 0; loopCounter < action.LoopCount; ++loopCounter)
				{
					for (int i = 0; i < pins.Length; ++i)
					{
						pins[i].Output((PaulTechGuy.RPi.GpioLib.PinValue)action.RgbStartValues[i]);
					}

					if (action.DurationMs > 0)
					{
						Thread.Sleep(action.DurationMs);
					}

					for (int i = 0; i < pins.Length; ++i)
					{
						// only output if it changes
						if (action.RgbEndValues[i] != action.RgbStartValues[i])
						{
							pins[i].Output((PaulTechGuy.RPi.GpioLib.PinValue)action.RgbEndValues[i]);
						}
					}

					if (action.PostDelayMs > 0)
					{
						Thread.Sleep(action.PostDelayMs);
					}
				}
			}
		}
	}
}
