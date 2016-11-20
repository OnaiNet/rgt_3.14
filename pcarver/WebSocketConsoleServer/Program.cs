using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using vtortola.WebSockets;

namespace WebSocketConsoleServer
{
	class Program
	{
		private ConcurrentDictionary<string, WebSocket> _connectedSockets = new ConcurrentDictionary<string, WebSocket>();

		static void Main(string[] args)
		{
			new Program().Run(args);
		}

		private void Run(string[] args)
		{
			var host = ConfigurationManager.AppSettings["BroadcastServerHost"];
			var port = Convert.ToInt32(ConfigurationManager.AppSettings["BroadcastServerPort"]);

			CancellationTokenSource cancellation = new CancellationTokenSource();
			var endpoint = new IPEndPoint(host.Trim().ToLower() == "*" ? IPAddress.Any : IPAddress.Parse(host), port);
			WebSocketListener server = new WebSocketListener(endpoint);
			var rfc6455 = new vtortola.WebSockets.Rfc6455.WebSocketFactoryRfc6455(server);
			server.Standards.RegisterStandard(rfc6455);
			server.Start();

			Console.WriteLine($"{this.GetType().Namespace} on host {endpoint.ToString()}");

			var task = Task.Run(() => AcceptWebSocketClientsAsync(server, cancellation.Token));

			Console.ReadKey(true);
			Log("Server stopping");
			cancellation.Cancel();
			task.Wait();
		}

		private async Task AcceptWebSocketClientsAsync(WebSocketListener server, CancellationToken token)
		{
			while (!token.IsCancellationRequested)
			{
				try
				{
					var ws = await server.AcceptWebSocketAsync(token).ConfigureAwait(false);
					if (ws != null)
					{
						try
						{
							Task.Run(() => HandleConnectionAsync(ws, token));
						}
						finally
						{
						}
					}
				}
				catch (Exception aex)
				{
					Log("Error Accepting clients: " + aex.GetBaseException().Message);
				}
			}
			Log("Server Stop accepting clients");
		}

		private async Task HandleConnectionAsync(WebSocket ws, CancellationToken cancellation)
		{
			try
			{
				_connectedSockets.AddOrUpdate(ws.Guid.ToString(), ws, (k, v) => v);
				while (ws.IsConnected && !cancellation.IsCancellationRequested)
				{
					String msg = await ws.ReadStringAsync(cancellation).ConfigureAwait(false);
					if (!string.IsNullOrWhiteSpace(msg))
					{
						string remoteHost = ws.RemoteEndpoint.ToString();
						Console.WriteLine($"Received: {msg} ({remoteHost})");
						IClientMessageHandler messageHandler = new BasicClientMessageHandler(_connectedSockets);
						if (messageHandler.HandleMessage(ws, msg))
						{
							break;
						}
					}
				}
			}
			catch (Exception aex)
			{
				Log("Error Handling connection: " + aex.GetBaseException().Message);
				try { ws.Close(); }
				catch { }
			}
			finally
			{
				_connectedSockets.TryRemove(ws.Guid.ToString(), out ws);
				ws.Dispose();
				Log("Client connection closed");
			}
		}

		static void Log(string message)
		{
			Console.WriteLine(message);
		}
	}
}
