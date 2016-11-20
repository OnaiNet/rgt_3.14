using GpioCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiGpioConsoleHost
{
	public interface IActionHandler
	{
		void Action(ActionBase baseAction, dynamic config);
	}
}
