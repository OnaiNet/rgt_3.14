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
		public async Task<IHttpActionResult> PostRgbSimpleAction(ActionBase[] actions)
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

		private async Task DoAction(ActionBase[] actions)
		{
			Console.WriteLine($"Received {actions.Length} action(s); queuing...");
			await Task.Run(() =>
			{
				foreach (var action in actions)
				{
					try
					{
						string fromHost = Request.GetOwinContext().Request.RemoteIpAddress;
						ActionQueueItem item = new ActionQueueItem(action, action.InstanceName, fromHost);
						ActionQueueManager.Instance.Enqueue(item);
					}
					catch(Exception ex)
					{
						Error($"Error while queuing action: {ex.ToString()}");
					}
				}
			});
		}

		public void Error(string message, params object[] args)
		{
			Console.Error.WriteLine(message, args);
		}
	}
}
