using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MUDEE;
using MUDEE.Attributes;

namespace MUDEE.Behaviours {
	public class DeathBehaviour : Behaviour {

		private HealthAttribute health;

		public override void OnAdd() {
			health = Bind<HealthAttribute>();
			entity.Dispatcher.Subscribe<HealthDepletedMessage>(OnHealthDepleted);
		}

		public override Component Clone() {
			throw new NotImplementedException();
		}

		public void OnHealthDepleted(HealthDepletedMessage msg) {
			Console.WriteLine("Health is depleted, according to DeathBehaviour");
		}
	}
}
