using GpioCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PiGpioConsoleHost
{
	public class ActionQueueManager
	{
		private static readonly ConcurrentDictionary<Guid, ActionQueueItem> _actionThreads = new ConcurrentDictionary<Guid, ActionQueueItem>();
		private static readonly BlockingCollection<ActionQueueItem> _actionQueue = new BlockingCollection<ActionQueueItem>();
		private static readonly Lazy<ActionQueueManager> _instance
			= new Lazy<ActionQueueManager>(() => new ActionQueueManager());
		private static CancellationTokenSource _cancellationSource = null;
		private static Task _managerTask = null;
		private static ActionConfigManager _configManager;
		private static Dictionary<string, IActionHandler> _actionHandlers = new Dictionary<string, IActionHandler>
		{
			{ nameof(RgbSimpleAction), new HandlerRgbSimpleAction() },
			{ nameof(LedSimpleAction), new HandlerLedSimpleAction() },
			{ nameof(BuzzerSimpleAction), new HandlerBuzzerSimpleAction() },
		};

		// do we allow threaded actions?
		private bool _actionThreading = Convert.ToBoolean(ConfigurationManager.AppSettings["ActionAllowThreading"]);

		// do we allow threaded actions?
		private bool _actionSimulation = Convert.ToBoolean(ConfigurationManager.AppSettings["ActionSimulateEnabled"]);

		// private to prevent direct instantiation.
		private ActionQueueManager()
		{
		}

		// accessor for instance
		public static ActionQueueManager Instance
		{
			get
			{
				return _instance.Value;
			}
		}

		public void Start()
		{
			// make sure we have a fresh action config manager
			var configFileDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "instance");
			_configManager = new ActionConfigManager(configFileDir);
			_configManager.Start();

			_cancellationSource = new CancellationTokenSource();
			_managerTask = Task.Run(() => { ProcessQueue(_cancellationSource.Token); }, _cancellationSource.Token);
		}

		private void ProcessQueue(CancellationToken cancelToken)
		{
			while (!_actionQueue.IsCompleted && !cancelToken.IsCancellationRequested)
			{
				ActionQueueItem item = null;
				// Blocks if number.Count == 0
				// IOE means that Take() was called on a completed collection.
				// Some other thread can call CompleteAdding after we pass the
				// IsCompleted check but before we call Take. 
				// In this example, we can simply catch the exception since the 
				// loop will break on the next iteration.
				try
				{
					item = _actionQueue.Take();
				}
				catch (InvalidOperationException)
				{
					// when we shut down, we expect this exception is the queue is complete; but
					// if not complete...
					if (!_actionQueue.IsCompleted)
					{
						throw;
					}
				}

				if (item != null)
				{
					bool doThreaded = _actionThreading && item.Action.ThreadId != Guid.Empty;
					if (doThreaded)
					{
						try
						{
							if (!_actionThreads.TryAdd(item.Action.ThreadId, item))
							{
								Error($"Duplicate threadId found; ignoring action.  ID: {item.Action.ThreadId.ToString()}");
								continue;
							}
							else
							{
								Log($"Adding thread id: {item.Action.ThreadId.ToString()}");
							}

							Task.Run(() => Process(item, doThreaded))
								.ContinueWith(task =>
								{
									if (task.IsFaulted)
									{
										Error(task.Exception.ToString());
									}

									ActionQueueItem temp;
									if (!_actionThreads.TryRemove(item.Action.ThreadId, out temp))
									{
										Error($"Unable to remove action thread by thread id: {item.Action.ThreadId.ToString()}");
									}
									else
									{
										Log($"Removing thread id: {item.Action.ThreadId.ToString()}");
									}

								});
						}
						finally
						{
						}
					}
					else
					{
						Process(item, doThreaded);
					}
				}
			}
		}

		private void Process(ActionQueueItem item, bool isThreaded)
		{
			try
			{
				var simulated = _actionSimulation ? " (simulated)" : string.Empty;

				var threaded = isThreaded ? " thread" : string.Empty;
				Console.WriteLine($"Start{threaded}{simulated} action {item.Action.ClassType}, Instance: {item.Action.InstanceName} ({item.Host})");

				if (!_actionSimulation)
				{
					IActionHandler handler = _actionHandlers[item.InstanceName];
					handler.Action(item.Action, _configManager[item.InstanceName]);
				}

				Console.WriteLine($"End{threaded}{simulated} action {item.Action.ClassType}, Instance: {item.Action.InstanceName} ({item.Host})");
			}
			catch (Exception ex)
			{
				Error($"Error while processing action: {ex.ToString()}");
			}
		}

		public void Stop()
		{
			if (_managerTask != null)
			{
				_configManager.Stop();
				_cancellationSource.Cancel();
				_actionQueue.CompleteAdding();
				_managerTask.Wait();
				_managerTask = null;
			}
		}

		public void Enqueue(ActionQueueItem item)
		{
			if (!_configManager.Exists(item.InstanceName))
			{
				throw new ApplicationException("Instance not found: " + item.InstanceName);
			}

			_actionQueue.Add(item);
		}

		public void Log(string message, params object[] args)
		{
			Console.WriteLine(message, args);
		}

		public void Error(string message, params object[] args)
		{
			Console.Error.WriteLine(message, args);
		}

	}
}
