using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpioCore
{
	public class RgbSimpleAction : ActionBase
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

		[JsonProperty(PropertyName = "startValues", Required = Required.Always)]
		public PinValue[] RgbStartValues { get; set; } = new PinValue[3] { PinValue.Low, PinValue.Low, PinValue.Low };

		[JsonProperty(PropertyName = "endValues", Required = Required.Always)]
		public PinValue[] RgbEndValues { get; set; } = new PinValue[3] { PinValue.Low, PinValue.Low, PinValue.Low };

	}
}
