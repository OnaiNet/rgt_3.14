﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpioCore
{
	public class RgbSimpleAction : ActionBase
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

		[JsonProperty(PropertyName = "startValues")]
		public PinValue[] RgbStartValues { get; set; } = new PinValue[3] { PinValue.Low, PinValue.Low, PinValue.Low };

		[JsonProperty(PropertyName = "endValues")]
		public PinValue[] RgbEndValues { get; set; } = new PinValue[3] { PinValue.Low, PinValue.Low, PinValue.Low };

	}
}