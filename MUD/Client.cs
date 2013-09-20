using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fleck;

namespace MUDEE {
	public class Client {
		private IWebSocketConnection socket;
		public static Dictionary<IWebSocketConnection, Client> socketDict;

		static Client() {
			socketDict = new Dictionary<IWebSocketConnection, Client>();
		}

		public static Client FromSocket(IWebSocketConnection socket) {
			Client client = null;
			if (socketDict.TryGetValue(socket, out client)) {
				return client;
			}
			return null;
		}

		public Client(IWebSocketConnection socket) {
			this.socket = socket;
			socketDict.Add(socket, this);
		}

		public void Send(string msg) {
			socket.Send(msg);
		}
	}
}
