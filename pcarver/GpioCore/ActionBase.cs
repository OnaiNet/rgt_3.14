using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpioCore
{
	public class ActionBase
	{
		[JsonProperty(PropertyName = "$type", Required = Required.Always)]
		public string ClassType { get; set; }

		[JsonProperty(PropertyName = "instance", Required = Required.Always)]
		public string InstanceName { get; set; }

		[JsonProperty(PropertyName = "threadId", Required = Required.Default)]
		public Guid ThreadId { get; set; } = Guid.Empty;

	}
}
