using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PaulTechGuy.GpioClientUtility
{
	public static class GpioClient
	{
		public static async Task SendActionAsync<T>(T action)
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var host = ConfigurationManager.AppSettings["GpioServerHost"];
			var port = Convert.ToInt32(ConfigurationManager.AppSettings["GpioServerPort"]);

			var uri = $"http://{host}:{port}/gpio";

			HttpResponseMessage response = await client.PostAsJsonAsync<T>(uri, action)
				.ContinueWith(t => t.Result.EnsureSuccessStatusCode());
		}

	}
}
