using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEE.Attributes {
	public class ALink : Attribute {

		public override ALDNode Serialize() {
			throw new NotImplementedException();
		}

		public override void Deserialize(ALDNode data) {
			throw new NotImplementedException();
		}

		public override Component Clone() {
			throw new NotImplementedException();
		}
	}
}
