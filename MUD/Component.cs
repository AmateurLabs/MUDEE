using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEE {
	public abstract class Component {

		public Entity entity;

		public abstract Component Clone();

		public T Bind<T>() where T : Attribute {
			return entity.Get<T>();
		}

		public virtual void OnAdd() {

		}

		public virtual void OnUpdate() {

		}

		public virtual void OnRemove() {

		}

		public static Component Load(ALDNode node) {
			Component component = CreateInstanceOf(node.Name);
			if (component is Attribute) {
				((Attribute)component).Deserialize(node);
			}
			return component;
		}

		public static Component CreateInstanceOf(string name) {
			try {
				string typeName = "";
				if (name.StartsWith("A")) {
					typeName = "MUDEE.Attributes." + name;
				} else if (name.StartsWith("B")) {
					typeName = "MUDEE.Behaviours." + name;
				}else {

				}
				return (Component)Activator.CreateInstance(null, typeName).Unwrap();
			} catch (Exception e) {
				throw e;
			}
		}
	}
}
