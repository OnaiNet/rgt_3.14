using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpioCore
{
	public class BuzzerSimpleAction : ActionBase
	{
		[JsonProperty(PropertyName = "preDelay")]
		public int PreDelayMs { get; set; } = 0;

		[JsonProperty(PropertyName = "postDelay")]
		public int PostDelayMs { get; set; } = 0;

		[JsonProperty(PropertyName = "startDuration")]
		public int StartDurationMs { get; set; } = 0;

		[JsonProperty(PropertyName = "endDuration")]
		public int EndDurationMs { get; set; } = 0;

		[JsonProperty(PropertyName = "loops")]
		public int LoopCount { get; set; } = 0;

		[JsonProperty(PropertyName = "startValue")]
		public PinValue LedStartValue { get; set; } = PinValue.Low;

		[JsonProperty(PropertyName = "endValue")]
		public PinValue LedEndValue { get; set; } = PinValue.Low;

	}
}
