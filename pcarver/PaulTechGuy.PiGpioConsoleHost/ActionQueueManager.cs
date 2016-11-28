using PaulTechGuy.GpioActions;
using PaulTechGuy.GpioCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaulTechGuy.PiGpioConsoleHost
{
	public class ActionQueueManager
	{
		private static readonly ConcurrentDictionary<Guid, ActionTaskItem> _actionTasks = new ConcurrentDictionary<Guid, ActionTaskItem>();
		private static readonly BlockingCollection<ActionQueueItem> _actionQueue = new BlockingCollection<ActionQueueItem>();
		private static readonly Lazy<ActionQueueManager> _instance
			= new Lazy<ActionQueueManager>(() => new ActionQueueManager());
		private static CancellationTokenSource _cancellationSource = null;
		private static Task _managerTask = null;
		private static ActionConfigManager _configManager;
		private static Dictionary<string, IActionHandler> _actionHandlers = new Dictionary<string, IActionHandler>
		{
			{ nameof(RgbSimpleAction), new HandlerRgbSimpleAction() },
			{ nameof(LedSimpleAction), new HandlerLedSimpleAction() },
			{ nameof(BuzzerSimpleAction), new HandlerBuzzerSimpleAction() },
		};

		// do we allow threaded actions?
		private bool _actionThreading = Convert.ToBoolean(ConfigurationManager.AppSettings["ActionAllowThreading"]);

		// do we allow threaded actions?
		private bool _actionSimulation = Convert.ToBoolean(ConfigurationManager.AppSettings["ActionSimulateEnabled"]);

		// private to prevent direct instantiation.
		private ActionQueueManager()
		{
		}

		// accessor for instance
		public static ActionQueueManager Instance
		{
			get
			{
				return _instance.Value;
			}
		}

		public void Start()
		{
			// make sure we have a fresh action config manager
			var configFileDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "instance");
			_configManager = new ActionConfigManager(configFileDir);
			_configManager.Start();

			_cancellationSource = new CancellationTokenSource();
			_managerTask = Task.Run(() => { ProcessQueue(_cancellationSource.Token); }, _cancellationSource.Token);
		}

		private void ProcessQueue(CancellationToken cancelToken)
		{
			while (!_actionQueue.IsCompleted && !cancelToken.IsCancellationRequested)
			{
				ActionQueueItem queueItem = null;
				// Blocks if number.Count == 0
				// IOE means that Take() was called on a completed collection.
				// Some other thread can call CompleteAdding after we pass the
				// IsCompleted check but before we call Take. 
				// In this example, we can simply catch the exception since the 
				// loop will break on the next iteration.
				try
				{
					queueItem = _actionQueue.Take();
				}
				catch (InvalidOperationException)
				{
					// when we shut down, we expect this exception is the queue is complete; but
					// if not complete...
					if (!_actionQueue.IsCompleted)
					{
						throw;
					}
				}

				if (queueItem != null)
				{
					bool doThreaded = _actionThreading && queueItem.Action.TaskId != Guid.Empty;
					if (doThreaded)
					{
						try
						{
							// create a task item without a real task at this point; we'll set it once we know
							// we can add it successfully (and no dups)
							ActionTaskItem taskItem = new ActionTaskItem(queueItem, null, new CancellationTokenSource());
							if (!_actionTasks.TryAdd(queueItem.Action.TaskId, taskItem))
							{
								Error($"Duplicate threadId found; ignoring action.  ID: {queueItem.Action.TaskId.ToString()}");
								continue;
							}
							else
							{
								Log($"Adding thread id: {queueItem.Action.TaskId.ToString()}");
							}

							// run the task, saving "t" as this task; if we did Run and Continue in one statement,
							// the returned task would be the Continue task and would be waiting
							Task t = Task.Run(() => Process(queueItem, taskItem.CancellationTokenSource));
							t.ContinueWith(task =>
							{
								if (task.IsFaulted)
								{
									Error(task.Exception.ToString());
								}

								ActionTaskItem tempTaskItem;
								if (!_actionTasks.TryRemove(queueItem.Action.TaskId, out tempTaskItem))
								{
									Error($"Unable to remove action thread by thread id: {queueItem.Action.TaskId.ToString()}");
								}
								else
								{
									Log($"Removing thread id: {queueItem.Action.TaskId.ToString()}");
								}

							});

							// not that the task is running, set the task item Task
							taskItem.Task = t;
						}
						finally
						{
						}
					}
					else
					{
						Process(queueItem, cancelSource: null);
					}
				}
			}
		}

		private void Process(ActionQueueItem item, CancellationTokenSource cancelSource)
		{
			try
			{
				var simulated = _actionSimulation ? " (simulated)" : string.Empty;

				var threaded = cancelSource != null ? " thread" : string.Empty;
				Console.WriteLine($"Start{threaded}{simulated} action {item.Action.GetType().Name}, instance: {item.Action.InstanceName} ({item.Host})");

				if (!_actionSimulation)
				{
					CancellationToken token = cancelSource == null ? CancellationToken.None : cancelSource.Token;
					IActionHandler handler = _actionHandlers[item.InstanceName];
					handler.Action(item.Action, token, _configManager[item.InstanceName]);
				}

				Console.WriteLine($"End{threaded}{simulated} action {item.Action.GetType().Name}, instance: {item.Action.InstanceName} ({item.Host})");
			}
			catch (Exception ex)
			{
				Error($"Error while processing action: {ex.ToString()}");
			}
		}

		public void Stop()
		{
			if (_managerTask != null)
			{
				StopActionTasks();
				_configManager.Stop();
				_cancellationSource.Cancel();
				_actionQueue.CompleteAdding();
				_managerTask.Wait();
				_managerTask = null;
			}
		}

		private void StopActionTasks()
		{
			int msDelay = Convert.ToInt32(ConfigurationManager.AppSettings["ActionStopWaitMs"]);
			foreach (var taskItem in _actionTasks)
			{
				// cancel the task and then wait for max time
				taskItem.Value.CancellationTokenSource.Cancel();
				var suffix = $"{taskItem.Value.QueueAction.GetType().Name}, instance: {taskItem.Value.QueueAction.InstanceName} ({taskItem.Value.QueueAction.Host})";
				if (taskItem.Value.Task.Wait(msDelay >= 0 ? msDelay : -1))
				{
					Log($"Stopped thread action {suffix}");
				}
				else
				{
					Error($"Stop failed, thread action {suffix}");
				}
			}
		}

		public void Enqueue(ActionQueueItem item)
		{
			if (!_configManager.Exists(item.InstanceName))
			{
				throw new ApplicationException("Instance not found: " + item.InstanceName);
			}

			_actionQueue.Add(item);
		}

		public ActionTaskItem[] Tasks()
		{
			ActionTaskItem[] tasks = _actionTasks.Values
				.ToArray();

			return tasks;
		}

		public bool CancelTask(Guid taskId)
		{
			ActionTaskItem taskItem;
			bool exists = false;
			if (_actionTasks.TryGetValue(taskId, out taskItem))
			{
				exists = true;
				taskItem.CancellationTokenSource.Cancel();
				// not much we can do here...this lil' call could be done via a web
				// service and we can't wait around
				//
				// we basically need to reply on the task behaving nicely since we've
				// given it a cancellation token

				Log($"Task cancellation requested, id: {taskItem.ToString()}");
			}
			else
			{
				Error($"Cancel task, id does not exist: {taskId.ToString()}");
			}

			return exists;
		}

		private void Log(string message, params object[] args)
		{
			Console.WriteLine(message, args);
		}

		private void Error(string message, params object[] args)
		{
			Console.Error.WriteLine(message, args);
		}

	}
}
