using Newtonsoft.Json;
using PaulTechGuy.GpioCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaulTechGuy.GpioCore
{
	public class ActionQueueItem
	{
		[JsonProperty(PropertyName = "action")]
		public ActionBase Action { get; set; }

		[JsonIgnore]
		public string InstanceName { get; set; }

		[JsonProperty(PropertyName = "host")]
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
