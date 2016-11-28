using PaulTechGuy.GpioCore;
using Newtonsoft.Json;
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
using System.Text;

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
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Route("~/task")]
		public HttpResponseMessage GetActionTaskCollection()
		{
			// we don't want to have the Json type name handling $type in the response so we need
			// to use a response message and manually set the Content member as a json string
			var tasks = ActionQueueManager.Instance.Tasks();
			HttpResponseMessage responseMsg = new HttpResponseMessage
			{
				Content = new StringContent(JsonConvert.SerializeObject(tasks, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None }))
			};
			responseMsg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return responseMsg;
		}

		[HttpDelete]
		[Route("~/task/{id:guid}")]
		public IHttpActionResult DeleteActionTask(Guid id)
		{
			bool requestCancelExists = ActionQueueManager.Instance.CancelTask(id);

			if (requestCancelExists)
			{
				return Ok();
			}
			else
			{
				return NotFound();
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
					catch (Exception ex)
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
