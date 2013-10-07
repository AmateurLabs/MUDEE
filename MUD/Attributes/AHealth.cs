using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEE.Attributes {
	public class AHealth : Attribute {

		private int health;
		private int maxHealth;

		public int Health {
			get {
				return health;
			}
			set {
				int oldHealth = health;
				health = value;
				if (health < 0) {
					health = 0;
				}
				if (health > maxHealth) {
					health = maxHealth;
				}
				entity.Dispatcher.Publish(new MHealthChanged(health - oldHealth));
				if (health == 0) {
					entity.Dispatcher.Publish(new MHealthDepleted(oldHealth - health));
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
			data.AddNode(new ALDNode("Health", ""+health));
			data.AddNode(new ALDNode("MaxHealth", ""+maxHealth));
			return data;
		}

		public override void Deserialize(ALDNode data) {
			health = 0;
			maxHealth = 0;
			if (data.Contains("MaxHealth")) {
				maxHealth = (int)data["MaxHealth"].Value;
				health = maxHealth;
			}
			if (data.Contains("Health")) {
				health = (int)data["Health"].Value;
			}
		}

		public override Component Clone() {
			throw new NotImplementedException();
		}
	}

	public class MHealthDepleted : Message {
		public int KillDamage;

		public MHealthDepleted(int killDamage) {
			KillDamage = killDamage;
		}
	}

	public class MHealthChanged : Message {
		public int Change;

		public MHealthChanged(int change) {
			Change = change;
		}
	}
}
