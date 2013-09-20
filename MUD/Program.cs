using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fleck;

namespace MUDEE
{
    class Program
    {
		static WebSocketServer server;
		static List<IWebSocketConnection> clients;

        static void Main(string[] args)
        {
			clients = new List<IWebSocketConnection>();
			server = new WebSocketServer("ws://www.amateurlabs.com:3000");
			server.SupportedSubProtocols = new string[] { "mud" };
			server.Start(socket => {
				socket.OnBinary = bytes => OnBinary(socket, bytes);
				socket.OnClose = () => OnClose(socket);
				socket.OnError = e => OnError(socket, e);
				socket.OnMessage = msg => OnMessage(socket, msg);
				socket.OnOpen = () => OnOpen(socket);
			});
			bool quit = false;
			while (!quit) {
				string input = Console.ReadLine();
				if (input == "quit") {
					quit = true;
				} else {
					OnServerMsg(input);
				}
			}
        }

		static void OnBinary(IWebSocketConnection socket, byte[] bytes) {
			
		}

		static void OnMessage(IWebSocketConnection socket, string msg) {
			Console.WriteLine("[" + socket.ConnectionInfo.Id + "] " + msg);
		}

		static void OnClose(IWebSocketConnection socket) {
			clients.Remove(socket);
			Console.WriteLine("Client " + socket.ConnectionInfo.Id + " Disconnected");
		}

		static void OnError(IWebSocketConnection socket, Exception e) {
			Console.WriteLine("Websocket Error: " + e);
		}

		static void OnOpen(IWebSocketConnection socket) {
			clients.Add(socket);
			Console.WriteLine("Client " + socket.ConnectionInfo.Id + " Connected");
		}

		static void OnServerMsg(string msg) {
			foreach (IWebSocketConnection socket in clients) {
				socket.Send(msg);
			}
		}
    }
}
