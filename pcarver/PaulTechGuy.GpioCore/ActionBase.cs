using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaulTechGuy.GpioCore
{
	public abstract class ActionBase
	{
		[JsonProperty(PropertyName = "instance", Required = Required.Always)]
		public string InstanceName { get; set; }

		[JsonProperty(PropertyName = "taskId", Required = Required.Default)]
		public Guid TaskId { get; set; } = Guid.Empty;

	}
}
