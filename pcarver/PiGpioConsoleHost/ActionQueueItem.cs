using GpioCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiGpioConsoleHost
{
	public class ActionQueueItem
	{
		public ActionBase Action { get; set; }

		public string InstanceName { get; set; }

		public string Host { get; set; }

		public ActionQueueItem()
		{

		}

		public ActionQueueItem(ActionBase action, string instanceName, string host)
		{
			Action = action;
			InstanceName = instanceName;
			Host = host;
		}
	}
}
