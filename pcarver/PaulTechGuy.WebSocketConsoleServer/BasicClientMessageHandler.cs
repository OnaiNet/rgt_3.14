using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PaulTechGuy.TranslationCore;
using vtortola.WebSockets;

namespace PaulTechGuy.WebSocketConsoleServer
{
	public class BasicClientMessageHandler : IClientMessageHandler
	{
		private readonly string BroadcastMessagePrefix = "broadcast ";
		private readonly ConcurrentDictionary<string, WebSocket> _sockets;

		public BasicClientMessageHandler(ConcurrentDictionary<string, WebSocket> sockets)
		{
			_sockets = sockets;
		}

		public bool HandleMessage(WebSocket socket, string message)
		{
			bool doClose = false;
			message = message.Trim();

			if (message.ToLower().StartsWith(BroadcastMessagePrefix))
			{
				BroadcastMessageAsync(message);
			}
			else
			{
				switch (message.ToLower())
				{
					case "echo":
						SendClient(socket, message);
						break;

					case "time":
						SendClient(socket, DateTime.UtcNow.ToString());
						break;

					case "id":
						SendClient(socket, socket.Guid.ToString());
						break;

					case "close":
						doClose = true;
						break;
				}

			}

			return doClose;
		}

		private void SendClient(WebSocket socket, string message)
		{
			Console.WriteLine($"Sending to client {socket.Guid.ToString()}: {message}");
			socket.WriteString(message);
		}

		private async void BroadcastMessageAsync(string message)
		{
			string languages = null;
			bool doTranslate = Convert.ToBoolean(ConfigurationManager.AppSettings["TranslateServerEnabled"]);
			var text = message.Substring(BroadcastMessagePrefix.Length);
			if (doTranslate)
			{
				languages = GetLanguages(text, Convert.ToInt32(ConfigurationManager.AppSettings["TranslateCount"]));
				TranslationItem item = await DoTranslation(text, languages);

				// only a single translation item so use index [0]
				text = item.Text[0];
			}

			if (!string.IsNullOrWhiteSpace(text))
			{
				foreach(var client in _sockets)
				{
					var languageKey = doTranslate ? $" as [{languages}]" : string.Empty;
					Console.WriteLine($"broadcasting to {client.Value.Guid.ToString()}{languageKey}: " + text);
					client.Value.WriteString(text);
				}
				BroadcastGpioSignal();
			}
		}

		private async void BroadcastGpioSignal()
		{
			bool doSignal = Convert.ToBoolean(ConfigurationManager.AppSettings["BroadcastGpioSignal"]);
			if (!doSignal)
			{
				return;
			}

			// broadcast signal to gpio as json
			string json = File.ReadAllText("BroadcastGpioSignal.json");
			await GpioClientUtility.GpioClient.SendActionAsync<dynamic>(JsonConvert.DeserializeObject(json), "/gpio/action");
		}

		private async Task<TranslationItem> DoTranslation(string text, string lang)
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var host = ConfigurationManager.AppSettings["TranslateServerHost"];
			var port = Convert.ToInt32(ConfigurationManager.AppSettings["TranslateServerPort"]);
			TranslationItem item = null; UriBuilder builder = new UriBuilder(
				$"http://{host}:{port}/translate");
			builder.Query = $"text={text}&lang={lang}";

			HttpResponseMessage response = await client.GetAsync(builder.Uri);
			if (response.IsSuccessStatusCode)
			{
				item = await response.Content.ReadAsAsync<TranslationItem>();
			}

			return item;
		}

		private string GetLanguages(string text, int translationCount)
		{
			var sourceLanguage = ConfigurationManager.AppSettings["TranslateServerSourceLanguage"].Trim();
			string[] destLanguages = new string[] { "es", "fr", "ru", "it", "he", "de", "pl", "el", "tr", "sv", };
			int seed = text.GetHashCode();
			Random rnd = new Random(seed);
			Dictionary<int, string> selected = new Dictionary<int, string>();
			while(selected.Count < translationCount)
			{
				int nextIndex = -1;
				do
				{
					nextIndex = rnd.Next(destLanguages.Length);
				} while (selected.ContainsKey(nextIndex));
				selected.Add(nextIndex, destLanguages[nextIndex]);
			}

			// add the starting and ending language
			selected.Add(-9999, sourceLanguage);
			selected.Add(9999, sourceLanguage);

			var queryLanguage = string.Join("-", selected.Keys
				.OrderBy(k => k)
				.Select(k => selected[k])
				.ToArray());

			return queryLanguage;
		}
	}
}
