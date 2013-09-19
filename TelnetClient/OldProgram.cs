using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MUD.Client;

namespace MUD.Client {
	/*class Program {

		static Client client;

		static void Main(string[] args) {
			client = new Client();
			Console.InputEncoding = Telnet.encoding;
			Console.OutputEncoding = Telnet.encoding;
			for (byte i = 0; i < byte.MaxValue; i++) {
				char c = Telnet.encoding.GetChars(new byte[] { i })[0];
				switch (i) {
					case 0:
					case 7:
					case 8:
					case 9:
					case 10:
					case 13:
						Console.WriteLine("{0:000} {1}", i, "?");
						break;
					default:
						Console.WriteLine("{0:000} {1}", i, c);
						break;
				}
			}
			while (client.client.Connected != true) {
				Console.WriteLine("Enter destination IP and port:");
				string connStr = Console.ReadLine();
				string[] connArgs = connStr.Split(' ');
				Console.Clear();
				if (connArgs.Length == 0) {
					continue;
				} else if (connArgs.Length == 1) {
					client.Connect(connArgs[0], "23");
				} else if (connArgs.Length == 2) {
					client.Connect(connArgs[0], connArgs[1]);
				}
				Console.WriteLine();
			}
			while (true) {
				if (client.client.Connected != true) return;
				string input = Console.ReadLine();
				client.Send(input);
			}
		}
	}*/
}
