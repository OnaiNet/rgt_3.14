using GpioCore;
using Newtonsoft.Json;
using PaulTechGuy.RPi.GpioLib;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace PiGpioConsoleHost.Controllers
{
	public class GpioController : ApiController
	{
		[HttpPost]
		[Route("~/gpio")]
		public async Task<IHttpActionResult> PostRgbSimpleAction(RgbSimpleAction[] actions)
		{
			if (actions == null) // invalid json
			{
				return BadRequest("Invalid actions (check JSON format)");
			}

			await DoAction(actions);
			return Ok();
		}

		private async Task DoAction(RgbSimpleAction[] actions)
		{
			await Task.Run(() =>
			{
				foreach (var action in actions)
				{
					GpioActionManager.Instance.Enqueue(action);
				}
			});
		}
	}
}
