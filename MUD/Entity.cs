using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEE {
	public class Entity {
		private static uint nextUID;
		public uint UID;

		private List<Component> components;
		private Dispatcher dispatcher;

		public Dispatcher Dispatcher {
			get {
				if (dispatcher == null) {
					dispatcher = new Dispatcher();
				}
				return dispatcher;
			}
		}

		public Entity() {
			UID = nextUID;
			nextUID++;
			components = new List<Component>();
		}

		public Entity(Prefab prefab) {

		}

		public Entity Clone() {
			Entity entity = new Entity();
			foreach (Component component in components) {
				entity.Add(component.Clone());
			}
			return entity;
		}

		public void Add(Component component) {
			if (component.entity != this) {
				components.Add(component);
				component.entity = this;
				component.OnAdd();
			}
		}

		public void Remove(Component component) {
			if (component.entity == this) {
				component.OnRemove();
				components.Remove(component);
				component.entity = null;
			}
		}

		public T Get<T>() where T : Component {
			foreach (Component component in components) {
				if (component is T) {
					return (T)component;
				}
			}
			return null;
		}

		public T[] GetAll<T>() where T : Component {
			List<T> result = new List<T>();
			foreach (Component component in components) {
				if (component is T) {
					result.Add((T)component);
				}
			}
			return result.ToArray();
		}

		public bool Has<T>() where T : Component {
			return (Get<T>() != null);
		}
	}
}
