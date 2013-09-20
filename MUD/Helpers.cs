using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEE {
	static class Helpers {
		public static string DrawBox(byte x, byte y, byte w, byte h) {
			string[,] buffer = new string[w, h];
			buffer[0, 0] = char.ConvertFromUtf32(0x250C);
			buffer[w - 1, 0] = char.ConvertFromUtf32(0x2510);
			buffer[w - 1, h - 1] = char.ConvertFromUtf32(0x2518);
			buffer[0, h - 1] = char.ConvertFromUtf32(0x2514);
			for (int t = 1; t < w - 1; t++) {
				buffer[t, 0] = char.ConvertFromUtf32(0x2500);
				buffer[t, h - 1] = char.ConvertFromUtf32(0x2500);
			}
			for (int t = 1; t < h - 1; t++) {
				buffer[w - 1, t] = char.ConvertFromUtf32(0x2502);
				buffer[0, t] = char.ConvertFromUtf32(0x2502);
			}
			string output = "";
			output += MUDPI.FormatCommand(MUDPI.CommandCode.BSE, x, y, w, h);
			for (int bufY = 0; bufY < h; bufY++) {
				for (int bufX = 0; bufX < w; bufX++) {
					if (buffer[bufX, bufY] == null) {
						buffer[bufX, bufY] = " ";
					}
					output += buffer[bufX, bufY];
				}
			}
			output += MUDPI.FormatCommand(MUDPI.CommandCode.BSD);
			return output;
		}
	}
}
