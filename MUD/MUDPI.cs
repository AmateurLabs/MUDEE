using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEE {
	class MUDPI {
		/* *** Multi-User Domain Protocol Interface ***
		 * MUDPI is a client-server protocol, unlike Telnet.
		 * MUDPI supports ASCII, but Unicode support is recommended for message content.
		 * It operates via escape sequences initiated by MCB and terminated by MCE.
		 * The client and server must recieve MHS and MHC commands, respectively, before accepting other input.
		 * BMC and EMC are the two C1-block Private Use control characters, as per ECMA-48.
		 * Commands 0-16 and 21-31 are reserved in order to prevent conflicts with ASCII Control Characters.
		 * A server may not use any command besides CSR and MC_ until it recieves a corresponding CSC command.
		 * The command Request system applies only to servers; clients can send any commands freely.
		 * CSC arguments 65-95 are assumed to confirm the corresponding Disable command as well.
		 * Clients are not obligated to answer CSR commands, but responding with a CSC or CSD is recommended.
		 * Each command can take any number of byte parameters, but functionality must not change drastically with parameter count.
		 * Fonts must be monospaced to ensure proper display on non-rectangular buffered clients.
		 * The formatting codes are designed to roughly match CSS, rather than ANSI.
		 */

		public enum CommandCode : byte {
			//0-16: Reserved (Non-Printable ASCII Codes)

			//17-20: MUDPI Control Characters
			MCB = 17, //MUDPI Command Begin
			MCE = 18, //MUDPI Command End
			MHS = 19, //MUDPI Handshake Server
			MHC = 20, //MUDPI Handshake Client

			//21-31: Reserved (Non-Printable ASCII Codes)

			//32, 64, 96: Command Support Codes
			CSR = 32, //Command Support Request [Command] //The command to request support for. 0=None, 1=All.
			CSC = 64, //Command Support Confirm [Command]  //The command to confirm support for. 0=None, 1=All.
			CSD = 96, //Command Support Deny [Command] //The command to deny support for. 0=None, 1=All.

			//33-47: Get-Set Commands
			BSG = 33, //Buffer Size Get //Requests that client send its buffer size via BSS.
			BSS = 34, //Buffer Size Set [Width, Height] //The size of the client's character buffer.
			CPG = 35, //Cursor Position Get //Requests that client send its cursor position via CPS. 
			CPS = 36, //Cursor Position Set [X, Y]
			KSG = 37, //Key State Get [Key] //Requests that client send the state of the given key. Key is a JS KeyboardEvent keycode.
			KSS = 38, //Key State Set [Key, State] //State is 1=Down, 2=Hold, 3=Up, else Neutral. Down and Up should only be sent once per press.
			//39-47 Reserved

			//48-57: Reserved (Custom Commands)

			//58-61: Clear Commands
			CWT = 58, //Clear Window Text
			CBT = 59, //Clear Buffer Text [X, Y, Width, Height]
			CRT = 60, //Clear Row Text
			CCT = 61, //Clear Column Text

			//62, 63, 95, 127: Extension Commands
			ECG = 62, //Extension Command Get [Subcommand, Values...]
			ECS = 63, //Extension Command Set [Subcommand, Values...]
			ECE = 95, //Extension Command Enable [Subcommand, Values...]
			ECD = 127, //Extension Command Disable [Subcommand]

			//65-90: Formatting Enables, 97-122 Formatting Disables
			FCE = 65, //Foreground Color Enable [R, G, B]
			FCD = 97, //Foreground Color Disable
			BCE = 66, //Background Color Enable [R, G, B]
			BCD = 98, //Background Color Disable
			TCE = 67, //Text Capitalization Enable [Type] //Type is 1=Capitalize, 2=Uppercase, 3=Lowercase. Other values map to Normal.
			TCD = 99, //Text Capitalization Disable
			TBE = 68, //Text Bold Enable
			TBD = 100, //Text Bold Disable
			TIE = 69, //Text Italics Enable
			TID = 101, //Text Italics Disable
			TUE = 70, //Text Underline Enable
			TUD = 102, //Text Underline Disable
			TOE = 71, //Text Overline Enable
			TOD = 103, //Text Overline Disable
			TLE = 72, //Text Line-through Enable
			TLD = 104, //Text Line-through Disable
			TSE = 73, //Text Shadow Enable [X, Y, Blur, R, G, B] //X and Y offsets are centered at 127, not 0. Blur is in pixels.
			TSD = 105, //Text Shadow Disable
			TWE = 74, //Text Weight Enable [Weight] //Ranges from 0-9, Normal=4, Bold=7
			TWD = 106, //Text Weight Disable
			TME = 75, //Text Mask Enable [Char] //Text between TME and TMD is masked as ASCII character Char without modifying the buffer.
			TMD = 107, //Text Mask Disable
			TFE = 76, //Text Flash Enable [Speed] //Blinks at a rate of Speed * 50 milliseconds
			TFD = 108, //Text Flash Disable
			WCE = 77, //Window Color Enable [R, G, B]
			WCD = 109, //Window Color Disable
			//78-90, 110-122 Reserved
			
			//91-94: Special Enables, 123-126: Special Disables
			BSE = 91, //Block Scope Enable [X, Y, Width, Height] //The block of characters to start writing to.
			BSD = 123, //Block Scope Disable
			PSE = 92, //Prompt Scope Enable //Sets cursor here and lets the client submit a message.
			PSD = 124, //Prompt Scope Disable //Marks the end of the prompt. Fill writeable area between PSE and PSD with spaces.
			KIE = 93, //Keyboard Input Enable //Allows client to send key info via KSS.
			KID = 125, //Keyboard Input Disable //Tells client not to send any key info via KSS.
			USE = 94, //Unicode Support Enable
			USD = 126, //Unicode Support Disable
		}

		public static string FormatCommand(CommandCode code, params byte[] parameters) {
			string output = "";
			byte[] bytes = new byte[parameters.Length + 3];
			bytes[0] = (byte)CommandCode.MCB;
			bytes[1] = (byte)code;
			for (int i = 0; i < parameters.Length; i++) {
				bytes[2 + i] = parameters[i];
			}
			bytes[2 + parameters.Length] = (byte)CommandCode.MCE;
			for (int i = 0; i < bytes.Length; i++) {
				output += char.ConvertFromUtf32(bytes[i]);
			}
			return output;
		}
	}
}
