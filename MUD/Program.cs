using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fleck;

namespace MUDEE
{
    class Program
    {
		static void Main(string[] args) {
			Server.Init();
			bool quit = false;
			while (!quit) {
				string input = Console.ReadLine();
				if (input == "quit") {
					quit = true;
				} else {
					Server.Broadcast(input);
				}
			}
		}
    }
}
