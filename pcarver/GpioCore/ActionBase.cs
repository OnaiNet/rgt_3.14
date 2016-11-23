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
		[JsonProperty(PropertyName = "plugin")]
		public string PluginName { get; set; }

		[JsonProperty(PropertyName = "instance")]
		public string InstanceName { get; set; }

		[JsonProperty(PropertyName = "threaded")]
		public bool IsThreaded { get; set; } = false;

		public static ActionBase JsonCreate(dynamic json)
		{
			ActionBase action = null;
			try
			{
				Type type = Type.GetType($"GpioCore.{json.plugin}, GpioCore");
				action = (ActionBase)JsonConvert.DeserializeObject(json.data.ToString(), type);
			}
			catch (Exception)
			{
				throw new ApplicationException($"Invalid plugin: {json.plugin}");
			}

			// any case properties
			action.PluginName = json.plugin;
			action.InstanceName = json.instance;
			action.IsThreaded = json.threaded;

			return action;
		}
	}
}
