using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vtortola.WebSockets;

namespace PaulTechGuy.WebSocketConsoleServer
{
	public interface IClientMessageHandler
	{
		/// <summary>
		/// Handle a client message coming in on the socket.
		/// </summary>
		/// <param name="socket">An open web socket</param>
		/// <returns>Return true if the handler signals to close the socket; otherwise false</returns>
		bool HandleMessage(WebSocket socket, string message);
	}
}
