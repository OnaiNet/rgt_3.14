using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaulTechGuy.GpioCore;
using System.Threading;
using PaulTechGuy.RPi.GpioLib;

namespace PaulTechGuy.PiGpioConsoleHost
{
	public class HandlerLedSimpleAction : IActionHandler
	{
		public void Action(ActionBase baseAction, CancellationToken cancelToken, dynamic config)
		{
			LedSimpleAction action = (LedSimpleAction)baseAction;

			// note that config is dynamic so we cast the pin values to integer
			using (DigitalPin pin = GpioHeader.Instance.CreatePin((int)config.pin, DigitalPinDirection.Output))
			{
				if (action.PreDelayMs > 0)
				{
					Thread.Sleep(action.PreDelayMs);
				}

				for (int loopCounter = 0; loopCounter < action.LoopCount; ++loopCounter)
				{
					pin.Output((PaulTechGuy.RPi.GpioLib.PinValue)action.LedStartValue);

					if (action.StartDurationMs > 0)
					{
						Thread.Sleep(action.StartDurationMs);
					}

					// only output if it changes
					if (action.LedEndValue != action.LedStartValue)
					{
						pin.Output((PaulTechGuy.RPi.GpioLib.PinValue)action.LedEndValue);
					}

					if (action.EndDurationMs > 0)
					{
						Thread.Sleep(action.EndDurationMs);
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
