using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MUD.Client {
	/*class Client {
		public TcpClient client;
		private NetworkStream stream;
		public Thread thread;
		public byte[] buffer;
		private int offset;
		public Dictionary<byte, bool> options;
		public Dictionary<byte, bool> serverOptions;

		public Client() {
			client = new TcpClient(AddressFamily.InterNetwork);
			buffer = new byte[1024];
			options = new Dictionary<byte, bool>();
			serverOptions = new Dictionary<byte, bool>();
			thread = new Thread(new ThreadStart(Run));
			thread.IsBackground = true;
		}
		
		public void Connect(string ip, string port) {
			int portNum = 0;
			if (!int.TryParse(port, out portNum)) {
				portNum = 23;
			}
			try {
				Console.WriteLine("Connecting to " + ip);
				client.Connect(ip, portNum);
				thread.Start();
				stream = client.GetStream();
			} catch (Exception e) {
				Console.WriteLine(e);
			}
		}

		public void Run() {
			while (true) {
				if (client.Connected) {
					Read();
				} else {
					Console.WriteLine("Disconnected");
					break;
				}
				Thread.Sleep(0);
			}
		}

		public void Send(string input) {
			
		}

		public void Send(byte[] bytes) {
			stream.Write(bytes, 0, bytes.Length);
		}

		public void Read() {
			//while (offset < buffer.Length) {
				int read = stream.ReadByte();
				if (read == -1) {
					Console.Write("No more data D:");
					/*if (offset > 0) {
						ParseBuffer();
						offset = 0;
					}
					return;
				}
				byte b = (byte)read;
				Console.Write((char)b);
				//buffer[offset] = b;
				/*if (b == (byte)Telnet.ControlCode.LF) {
					//ParseBuffer();
					offset = 0;
					return;
				}
				if (b == (byte)Telnet.ControlCode.NUL && buffer[offset - 1] == (byte)Telnet.ControlCode.CR) {
					//ParseBuffer();
					offset = 0;
					return;
				}
				//offset++;
			//}
			//ParseBuffer();
			//offset = 0;
		}

		public void ParseBuffer() {
			int len = offset + 1;
			for (int i = 0; i < len; i++) {
				if (i <= len - 3 && buffer[i] == (byte)Telnet.ControlCode.IAC) {
					if (i == 0 || (i > 0 && buffer[i - 1] != (byte)Telnet.ControlCode.IAC)) {
						if (buffer[i + 1] != (byte)Telnet.ControlCode.IAC) {
							ReadTelnetCommand(i);
							buffer[i] = 0;
							buffer[i + 1] = 0;
							buffer[i + 2] = 0;
							i += 2;
							continue;
						}
					}
				}
				Console.Write((char)buffer[i]);
				//Console.Write(Telnet.encoding.GetString(new byte[] { buffer[i] }));
			}
			//Console.WriteLine(Telnet.encoding.GetString(buffer, 0, len));
		}

		public void ReadTelnetCommand(int index) {
			Telnet.ControlCode code = (Telnet.ControlCode)buffer[index + 1];
			Telnet.TelnetOption option = (Telnet.TelnetOption)buffer[index + 2];
			Console.WriteLine("Recieved Telnet Command: IAC " + code + " " + option);
			switch (code) {
				case Telnet.ControlCode.WILL: //The server is enabling this option
					if (ServerOptionSupported((byte)option)) {
						serverOptions[(byte)option] = true;
						Console.WriteLine("Send Telnet Command: IAC " + Telnet.ControlCode.DO + " " + option);
						Send(new byte[] { (byte)Telnet.ControlCode.IAC, (byte)Telnet.ControlCode.DO, (byte)option });
					}else{
						serverOptions[(byte)option] = false;
						Console.WriteLine("Send Telnet Command: IAC " + Telnet.ControlCode.DONT + " " + option);
						Send(new byte[] { (byte)Telnet.ControlCode.IAC, (byte)Telnet.ControlCode.DONT, (byte)option });
					}
					break;
				case Telnet.ControlCode.WONT: //The server is disabling this option
					serverOptions[(byte)option] = false;
					Console.WriteLine("Send Telnet Command: IAC " + Telnet.ControlCode.DONT + " " + option);
					Send(new byte[] { (byte)Telnet.ControlCode.IAC, (byte)Telnet.ControlCode.DONT, (byte)option });
					break;
				case Telnet.ControlCode.DO: //The server wants us to turn on this option
					if (OptionSupported((byte)option)) {
						options[(byte)option] = true;
						Console.WriteLine("Send Telnet Command: IAC " + Telnet.ControlCode.WILL + " " + option);
						Send(new byte[] { (byte)Telnet.ControlCode.IAC, (byte)Telnet.ControlCode.WILL, (byte)option });
					} else {
						options[(byte)option] = false;
						Console.WriteLine("Send Telnet Command: IAC " + Telnet.ControlCode.WONT + " " + option);
						Send(new byte[] { (byte)Telnet.ControlCode.IAC, (byte)Telnet.ControlCode.WONT, (byte)option });
					}
					break;
				case Telnet.ControlCode.DONT: //The server wants us to turn off this option
					options[(byte)option] = false;
					Console.WriteLine("Send Telnet Command: IAC " + Telnet.ControlCode.WONT + " " + option);
					Send(new byte[] { (byte)Telnet.ControlCode.IAC, (byte)Telnet.ControlCode.WONT, (byte)option });
					break;
			}
		}

		public static bool ServerOptionSupported(byte option) {
			switch ((Telnet.TelnetOption)option) {
				default:
					return false;
			}
		}

		public static bool OptionSupported(byte option) {
			switch ((Telnet.TelnetOption)option) {
				default:
					return false;
			}
		}
	}*/
}
