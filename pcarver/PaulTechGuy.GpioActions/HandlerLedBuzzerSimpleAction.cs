using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaulTechGuy.GpioCore;
using System.Threading;
using PaulTechGuy.RPi.GpioLib;

namespace PaulTechGuy.GpioActions
{
	public class HandlerLedBuzzerSimpleAction : IActionHandler
	{
		public void Action(ActionBase baseAction, CancellationToken cancelToken, dynamic config)
		{
			LedBuzzerSimpleAction action = (LedBuzzerSimpleAction)baseAction;

			// note that config is dynamic so we cast the pin values to integer
			using (DigitalPin pinLed = GpioHeader.Instance.CreatePin((int)config.pinBuzzer, DigitalPinDirection.Output))
			using (DigitalPin pinBuzzer = GpioHeader.Instance.CreatePin((int)config.pinLed, DigitalPinDirection.Output))
			{
				if (cancelToken.IsCancellationRequested)
				{
					return;
				}

				if (action.PreDelayMs > 0)
				{
					Thread.Sleep(action.PreDelayMs);
				}

				if (cancelToken.IsCancellationRequested)
				{
					return;
				}

				for (int loopCounter = 0; loopCounter < action.LoopCount; ++loopCounter)
				{
					pinLed.Output((PaulTechGuy.RPi.GpioLib.PinValue)action.StartValue);
					pinBuzzer.Output((PaulTechGuy.RPi.GpioLib.PinValue)action.StartValue);

					if (action.StartDurationMs > 0)
					{
						Thread.Sleep(action.StartDurationMs);
					}

					// only output if it changes
					if (action.EndValue != action.StartValue)
					{
						pinLed.Output((PaulTechGuy.RPi.GpioLib.PinValue)action.EndValue);
						pinBuzzer.Output((PaulTechGuy.RPi.GpioLib.PinValue)action.EndValue);
					}

					if (action.EndDurationMs > 0)
					{
						Thread.Sleep(action.EndDurationMs);
					}

					if (cancelToken.IsCancellationRequested)
					{
						return;
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
