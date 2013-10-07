using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MUDEE;
using MUDEE.Attributes;

namespace MUDEE.Behaviours {
	public class BDeath : Behaviour {

		private AHealth health;

		public override void OnAdd() {
			health = Bind<AHealth>();
			entity.Dispatcher.Subscribe<MHealthDepleted>(OnHealthDepleted);
		}

		public override Component Clone() {
			throw new NotImplementedException();
		}

		public void OnHealthDepleted(MHealthDepleted msg) {
			Console.WriteLine("Health is depleted, according to DeathBehaviour. Killing strike did " + msg.KillDamage + " dmg.");
		}
	}
}
