using PaulTechGuy.GpioCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaulTechGuy.GpioCore
{
	public interface IActionHandler
	{
		void Action(ActionBase baseAction, CancellationToken cancelToken, dynamic config);
	}
}
