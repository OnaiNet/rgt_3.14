using GpioCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReceiveFromMachineConsole
{
	class Program
	{
		private string _receiveText = null;
		private readonly UTF8Encoding _encoding = new UTF8Encoding();
		private ManualResetEvent _receiveEvent = new ManualResetEvent(false);

		static void Main(string[] args)
		{
			new Program().Run(args);
		}

		private void Run(string[] args)
		{
			CancellationTokenSource cancelSource = new CancellationTokenSource();
			Receive(cancelSource.Token);

			Menu();
		}

		private void Menu()
		{
			bool done = false;
			while (!done)
			{
				Console.WriteLine(@"Menu
    receive TEXT: Simulate receive from previous machine and broadcast TEXT
            quit: Quit the program");
				Console.Write("\nChoice: ");
				var choice = Console.ReadLine().Trim().ToLower();
				if (choice.StartsWith("receive "))
				{
					_receiveText = choice.Substring("receive ".Length);
					_receiveEvent.Set();
				}
				else
				{
					switch (choice)
					{
						case "": // user just pressed enter so simply loop and show menu again
							break;

						case "quit":
							done = true;
							break;

						//case "receive":
						//	_receiveEvent.Set();
						//	break;

						default:
							Console.WriteLine("%Invalid choice!");
							break;
					}
				}
			}
		}

		private async void Receive(CancellationToken cancelToken)
		{
			await Task.Run(() =>
			{
				// wait for a phrase from previous machine somehow
				//
				// until we figure this out, just wait on the cancel token

				var host = ConfigurationManager.AppSettings["BroadcastServerHost"];
				var port = Convert.ToInt32(ConfigurationManager.AppSettings["BroadcastServerPort"]);
				while (true)
				{
					if (_receiveEvent.WaitOne(250))
					{
						BroadcastGpioSignalAsync();
						string uri = $"ws://{host}:{port}";
						SendPhrase(uri, $"broadcast {_receiveText}");
						_receiveEvent.Reset();
					}
					else if (cancelToken.IsCancellationRequested)
					{
						// need to cancel
						break;
					}
				}
			});
		}

		private async void SendPhrase(string uri, string message)
		{
			ClientWebSocket socket = null;
			try
			{
				socket = new ClientWebSocket();
				await socket.ConnectAsync(new Uri(uri), CancellationToken.None);
				await Send(socket, message);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Exception sending to endpoint: {uri}\n\n{ex.ToString()}");
			}
			finally
			{
				if (socket != null)
				{
					socket.Dispose();
					Console.WriteLine();
				}
			}
		}

		private async Task Send(ClientWebSocket socket, string message)
		{
			byte[] buffer = _encoding.GetBytes(message);
			await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, endOfMessage: true, cancellationToken: CancellationToken.None);
			Console.WriteLine("Broadcasting: " + message);
		}

		private async void BroadcastGpioSignalAsync()
		{
			bool doSignal = Convert.ToBoolean(ConfigurationManager.AppSettings["BroadcastGpioSignal"]);
			if (!doSignal)
			{
				return;
			}

			string json = File.ReadAllText("BroadcastGpioSignal.json");
			await GpioClientUtility.GpioClient.SendActionAsync<dynamic>(JsonConvert.DeserializeObject(json));
		}
	}
}
