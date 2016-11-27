using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaulTechGuy.GpioCore
{
	public class ActionTaskItem
	{
		[JsonIgnore]
		public Task Task { get; set; }

		// we'll call this "task" since it looks better in the json output
		[JsonProperty(PropertyName = "task")]
		public ActionQueueItem QueueAction { get; set; }

		[JsonIgnore]
		public CancellationTokenSource CancellationTokenSource { get; set; }

		public ActionTaskItem()
		{

		}

		public ActionTaskItem(ActionQueueItem queueAction, Task task, CancellationTokenSource token)
		{
			Task = task;
			QueueAction = queueAction;
			CancellationTokenSource = token;
		}
	}
}
