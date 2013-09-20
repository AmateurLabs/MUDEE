using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fleck;

namespace MUDEE {
	public static class Server {
		private static WebSocketServer socketServer;
		public static List<Client> clients;
		

		public static void Init() {
			clients = new List<Client>();
			socketServer = new WebSocketServer("ws://www.amateurlabs.com:3000");
			socketServer.SupportedSubProtocols = new string[] { "mud" };
			socketServer.Start(socket => {
				socket.OnBinary = bytes => OnBinary(socket, bytes);
				socket.OnClose = () => OnClose(socket);
				socket.OnError = e => OnError(socket, e);
				socket.OnMessage = msg => OnMessage(socket, msg);
				socket.OnOpen = () => OnOpen(socket);
			});
		}

		static void OnBinary(IWebSocketConnection socket, byte[] bytes) {

		}

		static void OnMessage(IWebSocketConnection socket, string msg) {
			Console.WriteLine("[" + socket.ConnectionInfo.Id + "] " + msg);
		}

		static void OnClose(IWebSocketConnection socket) {
			Client client = Client.FromSocket(socket);
			clients.Remove(client);
			Console.WriteLine("Client " + socket.ConnectionInfo.Id + " Disconnected");
		}

		static void OnError(IWebSocketConnection socket, Exception e) {
			Console.WriteLine("Websocket Error: " + e);
		}

		static void OnOpen(IWebSocketConnection socket) {
			Client client = new Client(socket);
			Console.WriteLine("Client " + socket.ConnectionInfo.Id + " Connected");
			client.Send(Helpers.DrawBox(0, 0, 60, 40));
		}

		public static void Broadcast(string msg) {
			foreach (Client client in clients) {
				client.Send(msg);
			}
		}
	}
}
