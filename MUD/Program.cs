using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fleck;
using MUDEE.Attributes;
using MUDEE.Behaviours;

namespace MUDEE
{
    class Program
    {
		static void Main(string[] args) {
			Server.Init();

			Entity e = new Entity();
			e.Deserialize(ALDNode.ParseFile("C:\\Test.ald"));
			Console.WriteLine(e.Serialize().Serialize());

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
