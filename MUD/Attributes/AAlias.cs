using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEE.Attributes {
	public class AAlias : Attribute {
		private string[] aliases;

		public string Alias {
			get {
				if (aliases.Length == 0) {
					return "";
				}
				return aliases[0];
			}

			set {
				if (aliases.Length == 0) {
					aliases = new string[1];
				}
				aliases[0] = value;
			}
		}

		public string[] Aliases {
			get {
				return aliases;
			}

			set {
				aliases = value;
			}
		}

		public override ALDNode Serialize() {
			ALDNode data = new ALDNode();
			for (int i = 0; i < aliases.Length; i++) {
				data.AddNode(new ALDNode(aliases[i], ""));
			}
			return data;
		}

		public override void Deserialize(ALDNode data) {
			List<string> aliasList = new List<string>();
			foreach (ALDNode childNode in data) {
				aliasList.Add(childNode.Name);
			}
			aliases = aliasList.ToArray();
		}

		public override Component Clone() {
			throw new NotImplementedException();
		}
	}
}
