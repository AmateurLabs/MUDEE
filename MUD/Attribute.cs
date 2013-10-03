using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEE {
	public abstract class Attribute : Component {

		public abstract ALDNode Serialize();
		public abstract void Deserialize(ALDNode data);
	}
}
