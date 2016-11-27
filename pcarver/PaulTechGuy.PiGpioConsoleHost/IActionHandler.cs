using PaulTechGuy.GpioCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaulTechGuy.PiGpioConsoleHost
{
	public interface IActionHandler
	{
		void Action(ActionBase baseAction, CancellationToken cancelToken, dynamic config);
	}
}
