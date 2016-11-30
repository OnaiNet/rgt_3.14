using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using PaulTechGuy.TranslationCore;

namespace PaulTechGuy.TranslateApiConsoleHost.Controllers
{
	public class TranslateController : ApiController
	{
		[HttpGet]
		[Route("translate")]
		public async Task<IHttpActionResult> Get(string text, string lang)
		{
			TranslationItem translation = await DoTranslation(text, lang);
			return Json<TranslationItem>(translation);
		}

		private async Task<TranslationItem> DoTranslation(string text, string lang)
		{
			if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(lang))
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			string[] parts = lang.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(s => s.Trim().ToLower())
				.ToArray();

			var newText = text;
			TranslationItem item = null;
			for (int i = 0; i < parts.Length - 1; ++i)
			{
				string part = $"{parts[i]}-{parts[i + 1]}";
				UriBuilder builder = new UriBuilder("https://translate.yandex.net/api/v1.5/tr.json/translate");
				builder.Query = $"key=trnsl.1.1.20161115T000752Z.e4ef69f185b49000.8af2c89b99ea6cf6063ad452099e5f3e999e4487&text={newText}&lang={part}";

				HttpResponseMessage response = await client.GetAsync(builder.Uri);
				if (response.IsSuccessStatusCode)
				{
					item = await response.Content.ReadAsAsync<TranslationItem>();
					newText = item.Text[0];

					// adjust the language returned since right now it holds just the last translation
					item.Language = lang;
				}
			}

			var uri = Request.GetOwinContext().Request.Uri;
			var host = Request.GetOwinContext().Request.RemoteIpAddress;
			Console.WriteLine($" Translated uri: {uri}");
			Console.WriteLine($"Translated from: {host}");
			Console.WriteLine($"Translated lang: {lang}");
			Console.WriteLine($"Translated from: {text}");
			Console.WriteLine($"  Translated to: {item.Text[0]}");
			Console.WriteLine();

			return item;
		}
	}
}
