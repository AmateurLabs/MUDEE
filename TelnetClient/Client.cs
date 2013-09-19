using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Telnet {
	class Client {
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
			thread.Start();
		}
		
		public void Connect(string ip, string port) {
			int portNum = 0;
			if (!int.TryParse(port, out portNum)) {
				portNum = 23;
			}
			try {
				Console.WriteLine("Connecting to " + ip);
				client.Connect(ip, portNum);
				stream = client.GetStream();
			} catch (SocketException e) {
				Console.WriteLine(e);
			}
		}

		public void Run() {
			while (true) {
				if (client.Connected && client.Available > 0) {
					Read();
				}
				Thread.Sleep(0);
			}
		}

		public void Send(string input) {
			Send(Telnet.encoding.GetBytes(input));
		}

		public void Send(byte[] bytes) {
			stream.Write(bytes, 0, bytes.Length);
		}

		public void Read() {
			while (true) {
				int read = stream.ReadByte();
				if (read == -1) {
					return;
				}
				byte b = (byte)read;
				buffer[offset] = b;
				if (b == (byte)Telnet.ControlCode.LF) {
					ParseBuffer();
					offset = 0;
					return;
				}
				if (b == (byte)Telnet.ControlCode.NUL && buffer[offset - 1] == (byte)Telnet.ControlCode.CR) {
					ParseBuffer();
					offset = 0;
					return;
				}
				offset++;
			}
		}

		public void ParseBuffer() {
			int len = offset;
			for (int i = 0; i <= len; i++) {
				if (i <= len - 3 && buffer[i] == (byte)Telnet.ControlCode.IAC) {
					if (i > 0 && buffer[i - 1] != (byte)Telnet.ControlCode.IAC) {
						if (buffer[i + 1] != (byte)Telnet.ControlCode.IAC) {
							ReadTelnetCommand(i);
							i += 2;
							continue;
						}
					}
				}
			}
			Console.WriteLine(Telnet.encoding.GetString(buffer, 0, len + 1));
		}

		public void ReadTelnetCommand(int index) {
			Telnet.ControlCode code = (Telnet.ControlCode)buffer[index + 1];
			Telnet.TelnetOption option = (Telnet.TelnetOption)buffer[index + 2];
			switch (code) {
				case Telnet.ControlCode.WILL: //The server is enabling this option
					if (ServerOptionSupported((byte)option)) {
						serverOptions[(byte)option] = true;
						Send(new byte[] { (byte)Telnet.ControlCode.IAC, (byte)Telnet.ControlCode.DO, (byte)option });
					}else{
						serverOptions[(byte)option] = false;
						Send(new byte[] { (byte)Telnet.ControlCode.IAC, (byte)Telnet.ControlCode.DONT, (byte)option });
					}
					break;
				case Telnet.ControlCode.WONT: //The server is disabling this option
					serverOptions[(byte)option] = false;
					Send(new byte[] { (byte)Telnet.ControlCode.IAC, (byte)Telnet.ControlCode.DONT, (byte)option });
					break;
				case Telnet.ControlCode.DO: //The server wants us to turn on this option
					if (OptionSupported((byte)option)) {
						options[(byte)option] = true;
						Send(new byte[] { (byte)Telnet.ControlCode.IAC, (byte)Telnet.ControlCode.WILL, (byte)option });
					} else {
						options[(byte)option] = false;
						Send(new byte[] { (byte)Telnet.ControlCode.IAC, (byte)Telnet.ControlCode.WONT, (byte)option });
					}
					break;
				case Telnet.ControlCode.DONT: //The server wants us to turn off this option
					options[(byte)option] = false;
					Send(new byte[] { (byte)Telnet.ControlCode.IAC, (byte)Telnet.ControlCode.WONT, (byte)option });
					break;
			}
		}

		public static bool ServerOptionSupported(byte option) {

			return false;
		}

		public static bool OptionSupported(byte option) {
			switch ((Telnet.TelnetOption)option) {
				case Telnet.TelnetOption.TerminalType:
					return true;
				default:
					return false;
			}
		}
	}
}
