using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpioCore
{
	public enum PinValue
	{
		High = PaulTechGuy.RPi.GpioLib.PinValue.High,
		Low  = PaulTechGuy.RPi.GpioLib.PinValue.Low,
	}

	public class RgbSimpleAction : GpioActionBase
	{
		[JsonProperty(PropertyName = "preDelay")]
		public int PreDelayMs { get; set; }

		[JsonProperty(PropertyName = "postDelay")]
		public int PostDelayMs { get; set; }

		[JsonProperty(PropertyName = "duration")]
		public int DurationMs { get; set; }

		[JsonProperty(PropertyName = "loops")]
		public int LoopCount { get; set; }

		[JsonProperty(PropertyName = "pins")]
		public int[] RgbPins { get; set; } = new int[3];

		[JsonProperty(PropertyName = "startValues")]
		public PinValue[] RgbStartValues { get; set; } = new PinValue[3];

		[JsonProperty(PropertyName = "endValues")]
		public PinValue[] RgbEndValues { get; set; } = new PinValue[3];
	}
}
