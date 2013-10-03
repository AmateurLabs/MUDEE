using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEE.Attributes {
	public class HealthAttribute : Attribute {

		private int health;
		private int maxHealth;

		public int Health {
			get {
				return health;
			}
			set {
				health = value;
				if (health < 0) {
					health = 0;
				}
				if (health > maxHealth) {
					health = maxHealth;
				}
				if (health == 0) {
					entity.Dispatcher.Publish(new HealthDepletedMessage());
				}
			}
		}

		public int MaxHealth {
			get {
				return maxHealth;
			}
			set {
				maxHealth = value;
			}
		}

		public override ALDNode Serialize() {
			ALDNode data = new ALDNode();
			data.AddNode(new ALDNode("health", ""+health));
			data.AddNode(new ALDNode("maxHealth", ""+maxHealth));
			return data;
		}

		public override void Deserialize(ALDNode data) {
			health = 0;
			maxHealth = 0;
			if (data.Contains("health")) {
				
			}
		}

		public override Component Clone() {
			throw new NotImplementedException();
		}
	}

	public class HealthDepletedMessage : Message {

	}

	public delegate void HealthDepletedHandler(HealthDepletedMessage msg);
}
