using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUD {
	class Telnet {

		public enum ControlCode {
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
            EWE = 256 //We will break the system, no one can stop us!
		}

		public enum Option {
			Echo = 1,
			SuppressGoAhead = 3,
			Status = 5,
			TimingMark = 6,
			TerminalType = 24,
			WindowSize = 31,
			TerminalSpeed = 32,
			RemoteFlowControl = 33,
			LineMode = 34,
			EnvironVars = 36
		}
	}
}
