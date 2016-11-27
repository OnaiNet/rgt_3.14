using PaulTechGuy.GpioCore;
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

namespace PaulTechGuy.PiGpioConsoleHost.Controllers
{
	public class GpioController : ApiController
	{
		#region public methods

		[HttpPost]
		[Route("~/gpio")]
		public async Task<IHttpActionResult> PostActionCollection(ActionBase[] actions)
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

		[HttpGet]
		[Route("~/task")]
		public ActionTaskItem[] GetActionTaskCollection()
		{
			var tasks = ActionQueueManager.Instance.Tasks();

			return tasks;
		}

		[HttpDelete]
		[Route("~/task/{id:guid}")]
		public IHttpActionResult DeleteActionTask(Guid id)
		{
			bool requestCancelExists = ActionQueueManager.Instance.CancelTask(id);

			if (requestCancelExists)
			{
				return Content(HttpStatusCode.OK, new { message = $"Cancel requested submitted, id: {id.ToString()}" });
			}
			else
			{
				return Content(HttpStatusCode.NotFound, new { message = $"Task not found, id: {id.ToString()}" });
			}
		}

		#endregion

		#region private methods

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

		private void Error(string message, params object[] args)
		{
			Console.Error.WriteLine(message, args);
		}

		#endregion
	}
}
