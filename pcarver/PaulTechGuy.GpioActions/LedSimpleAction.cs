using Newtonsoft.Json;
using PaulTechGuy.GpioCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaulTechGuy.GpioActions
{
	public class LedSimpleAction : ActionBase
	{
		[JsonProperty(PropertyName = "preDelay", Required = Required.Always)]
		public int PreDelayMs { get; set; } = 0;

		[JsonProperty(PropertyName = "postDelay", Required = Required.Always)]
		public int PostDelayMs { get; set; } = 0;

		[JsonProperty(PropertyName = "startDuration", Required = Required.Always)]
		public int StartDurationMs { get; set; } = 0;

		[JsonProperty(PropertyName = "endDuration", Required = Required.Always)]
		public int EndDurationMs { get; set; } = 0;

		[JsonProperty(PropertyName = "loops", Required = Required.Always)]
		public int LoopCount { get; set; } = 0;

		[JsonProperty(PropertyName = "startValue", Required = Required.Always)]
		public PinValue LedStartValue { get; set; } = PinValue.Low;

		[JsonProperty(PropertyName = "endValue", Required = Required.Always)]
		public PinValue LedEndValue { get; set; } = PinValue.Low;

	}
}
