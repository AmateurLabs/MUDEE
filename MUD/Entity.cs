using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEE {
	public class Entity {
		private static uint nextUID;
		public readonly uint UID;
		private Entity parent;
		private List<Entity> children;

		public Entity Parent {
			get {
				return parent;
			}
			set {
				parent = value;
				parent.children.Add(this);
			}
		}

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
			children = new List<Entity>();
		}

		public Entity Clone() {
			Entity entity = new Entity();
			foreach (Component component in components) {
				entity.Add(component.Clone());
			}
			return entity;
		}

		public void Update() {
			foreach (Component component in components) {
				component.OnUpdate();
			}
			foreach (Entity child in children) {
				child.Update();
			}
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

		public ALDNode Serialize() {
			ALDNode data = new ALDNode();
			foreach (Component component in components) {
				ALDNode componentNode = new ALDNode("" + component.GetType().Name, "");
				if (component is Attribute) {
					Attribute attribute = component as Attribute;
					foreach (ALDNode attributeData in attribute.Serialize()) {
						componentNode.AddNode(attributeData);
					}
				}
				data.AddNode(componentNode);
			}
			return data;
		}

		public void Deserialize(ALDNode data) {
			foreach (ALDNode node in data) {
				Component c = Component.CreateInstanceOf(node.Name);
				if (c is Attribute) {
					((Attribute)c).Deserialize(node);
				}
				Add(c);				
			}
		}
	}
}
