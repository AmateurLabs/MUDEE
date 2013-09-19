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

        static void Main(string[] args)
        {
			server.Start(socket => {
				socket.OnBinary = OnBinary;
				socket.OnClose = OnClose;
				socket.OnError = OnError;
				socket.OnMessage = OnMessage;
				socket.OnOpen = OnOpen;
			});
        }

		static void OnBinary(byte[] bytes) {

		}

		static void OnMessage(string msg) {

		}

		static void OnClose() {

		}

		static void OnError(Exception e) {

		}

		static void OnOpen() {

		}
    }
}
