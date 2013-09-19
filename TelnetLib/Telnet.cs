using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telnet {
	public class Telnet {

		public static Encoding encoding = Encoding.GetEncoding(437);
		//public static Encoding encoding = Encoding.GetEncoding(437);

		public enum ControlCode : byte {
			//Required Control Codes
			NUL = 0, //NULL
			LF = 10, //Line Feed
			CR = 13, //Carriage Return

			//Optional Control Codes
			BEL = 7, //BELL
			BS = 8, //Back Space
			HT = 9, //Horizontal Tab
			VT = 11, //Vertical Tab
			FF = 12, //Form Feed

			//ANSI
			ESC = 27, //ANSI escape
			OPEN = 91, //Opening bracket
			CLOSE = 93, //Closing bracket
			ARGSEP = 59, //Argument Seperator ;
			
			//Commands
			SE = 240, //End of Subnegotiation parameters
			NOP = 241, //No operation
			DM = 242, //Data mark
			BRK = 243, //Break
			IP = 244, //Suspend
			AO = 245, //Abort output
			AYT = 246, //Are you there
			EC = 247, //Erase character
			EL = 248, //Erase line
			GA = 249, //Go ahead
			SB = 250, //Subnegotiation
			WILL = 251, //will
			WONT = 252, //wont
			DO = 253, //do
			DONT = 254, //dont
			IAC = 255, //Interpret as command
		}

		public enum TelnetOption : byte {
			Echo = 1,
			SuppressGoAhead = 3,
			Status = 5,
			TimingMark = 6,
			TerminalType = 24,
			WindowSize = 31,
			TerminalSpeed = 32,
			RemoteFlowControl = 33,
			LineMode = 34,
			EnvironVars = 36,
		}

		public enum ANSICode : byte {
			CUU = 65, //Cursor Up
			CUD = 66, //Cursor Down
			CUF = 67, //Cursor Forward
			CUB = 68, //Cursor Back
			CNL = 69, //Cursor Next Line
			CPL = 70, //Cursor Previous Line
			CHA = 71, //Cursor Horizontal Absolute
			CUP = 72, //Cursor Position
			ED = 74, //Erase Display
			EL = 75, //Erase in Line
			SU = 83, //Scroll Up
			SD = 84, //Scroll Down
			HVP = 102, //Horizontal and Vertical Position
			SGR = 109, //Select Graphic Rendition
			DSR = 110, //Device Status Report
			SCP = 115, //Save Cursor Position
			RCP = 117, //Restore Cursor Position
			DECTCEMSET = 104, //Show Cursor
			DECTCEMRESET = 108, //Hide Cursor
		}

		public enum SGRParam : byte {
			Reset = 0,
			Bold = 1,
			Faint = 2,
			ItalicOn = 3,
			UnderlineSingle = 4,
			BlinkSlow = 5,
			BlinkRapid = 6,
			ImageNegative = 7,
			Conceal = 8,
			CrossedOut = 9,
			DefaultFont = 10,
			AltFont1 = 11,
			AltFont2 = 12,
			AltFont3 = 13,
			AltFont4 = 14,
			AltFont5 = 15,
			AltFont6 = 16,
			AltFont7 = 17,
			AltFont8 = 18,
			AltFont9 = 19,
			Fraktur = 20,
			BoldOff = 21,
			NormalIntensity = 22,
			ItalicOff = 22,
			UnderlineNone = 23,
			BlinkOff = 24,
			ImagePositive = 27,
			Reveal = 28,
			NotCrossedOut = 29,
			ForeBlack = 30,
			ForeRed = 31,
			ForeGreen = 32,
			ForeYellow = 33,
			ForeBlue = 34,
			ForeMagenta = 35,
			ForeCyan = 36,
			ForeWhite = 37,
			ForeXTERM = 38,
			ForeDefault = 39,
			BackBlack = 40,
			BackRed = 41,
			BackGreen = 42,
			BackYellow = 43,
			BackBlue = 44,
			BackMagenta = 45,
			BackCyan = 46,
			BackWhite = 47,
			BackXTERM = 48,
			BackDefault = 49,
			Framed = 51,
			Encircled = 52,
			Overlined = 53,
			NotFramedOrEncircled = 54,
			NotOverlined = 55,
			IdeoUnderline = 60,
			IdeoDoubleUnderline = 61,
			IdeoOverline = 62,
			IdeoDoubleOverline = 63,
			IdeoStressMarking = 64,
		}

		public byte[] ANSIBytes(ANSICode code) {
			byte[] bytes = new byte[3];
			bytes[0] = (byte)ControlCode.ESC;
			bytes[1] = (byte)ControlCode.OPEN;
			bytes[2] = (byte)code;
			return bytes;
		}

		public byte[] ANSIBytes(ANSICode code, byte p0) {
			byte[] bytes = new byte[4];
			bytes[0] = (byte)ControlCode.ESC;
			bytes[1] = (byte)ControlCode.OPEN;
			bytes[2] = p0;
			bytes[3] = (byte)code;
			return bytes;
		}

		public byte[] ANSIBytes(ANSICode code, byte p0, byte p1) {
			byte[] bytes = new byte[6];
			bytes[0] = (byte)ControlCode.ESC;
			bytes[1] = (byte)ControlCode.OPEN;
			bytes[2] = p0;
			bytes[3] = (byte)ControlCode.ARGSEP;
			bytes[4] = p1;
			bytes[5] = (byte)code;
			return bytes;
		}
	}
}
