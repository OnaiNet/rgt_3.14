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
	public class HandlerRgbSimpleAction : IActionHandler
	{
		public void Action(ActionBase baseAction, CancellationToken cancelToken, dynamic config)
		{
			RgbSimpleAction action = (RgbSimpleAction)baseAction;

			// note that config is dynamic so we cast the pin values to integer
			using (DigitalPin pin1 = GpioHeader.Instance.CreatePin((int)config.pins[0], DigitalPinDirection.Output))
			using (DigitalPin pin2 = GpioHeader.Instance.CreatePin((int)config.pins[1], DigitalPinDirection.Output))
			using (DigitalPin pin3 = GpioHeader.Instance.CreatePin((int)config.pins[2], DigitalPinDirection.Output))
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

					if (action.StartDurationMs > 0)
					{
						Thread.Sleep(action.StartDurationMs);
					}

					for (int i = 0; i < pins.Length; ++i)
					{
						// only output if it changes
						if (action.RgbEndValues[i] != action.RgbStartValues[i])
						{
							pins[i].Output((PaulTechGuy.RPi.GpioLib.PinValue)action.RgbEndValues[i]);
						}
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
