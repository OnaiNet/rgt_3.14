using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaulTechGuy.TranslationCore
{
	public class TranslationItem
	{
		[JsonProperty(PropertyName ="code")]
		public int HttpCode { get; set; }

		[JsonProperty(PropertyName ="lang")]
		public string Language { get; set; }

		[JsonProperty(PropertyName ="text")]
		public string[] Text { get; set; }
	}
}