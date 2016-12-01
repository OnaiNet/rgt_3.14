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
		[Route("~/gpio/action")]
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
		[Route("~/gpio/task")]
		public ActionTaskItem[] GetActionTaskCollection()
		{
			var tasks = ActionQueueManager.Instance.Tasks();

			return tasks;
		}

		[HttpDelete]
		[Route("~/gpio/task/{id}")]
		public IHttpActionResult DeleteActionTask(string id)
		{
			bool requestCancelExists = ActionQueueManager.Instance.CancelTask(id);
			if (requestCancelExists)
				return Ok();
			else
				return BadRequest();
		}

		[HttpGet]
		[Route("~/gpio/task/{id}")]
		public IHttpActionResult GetActionTask(string id)
		{
			ActionTaskItem taskItem = ActionQueueManager.Instance.GetTask(id);
			if (taskItem != null)
			{
				return Ok(taskItem);
			}
			else
			{
				return BadRequest();
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
