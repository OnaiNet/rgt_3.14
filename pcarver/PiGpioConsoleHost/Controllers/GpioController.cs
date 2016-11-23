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
		public async Task<IHttpActionResult> PostRgbSimpleAction(dynamic[] actions)
		{
			if (actions == null) // invalid json
			{
				return BadRequest("Invalid actions (check JSON format)");
			}

			try
			{
				await DoAction(actions);
				return Ok();
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		private async Task DoAction(dynamic[] actions)
		{
			Console.WriteLine($"Received {actions.Length} action(s); queuing...");
			await Task.Run(() =>
			{
				foreach (var json in actions)
				{
					string host = Request.GetOwinContext().Request.RemoteIpAddress;
					ActionBase obj = ActionBase.JsonCreate(json);
					ActionQueueItem item = new ActionQueueItem (obj, obj.InstanceName, host);
					ActionQueueManager.Instance.Enqueue(item);
				}
			});
		}
	}
}
