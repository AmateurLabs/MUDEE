using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEE {
	public class Dispatcher {

		private readonly Dictionary<Type, MulticastDelegate> handlers;

		public Dispatcher() {
			handlers = new Dictionary<Type, MulticastDelegate>();
		}

		public void Subscribe<T>(MessageHandler<T> handler) where T : Message {
			Type t = typeof(T);
			if (handlers.ContainsKey(t)) {
				MessageHandler<T> existingHandler = (MessageHandler<T>)handlers[t];
				existingHandler += handler;
			} else {
				handlers.Add(t, handler);
			}
		}

		public void Unsubscribe<T>(MessageHandler<T> handler) where T : Message {
			Type t = typeof(T);
			if (handlers.ContainsKey(t)) {
				MessageHandler<T> existingHandler = (MessageHandler<T>)handlers[t];
				existingHandler -= handler;
			}
		}

		public void Publish<T>(T msg) where T : Message {
			Type t = typeof(T);
			if (!handlers.ContainsKey(t)) {
				return;
			}
			((MessageHandler<T>)handlers[t])(msg);
		}
	}

	public delegate void MessageHandler<in T>(T msg);

	public abstract class Message {
		public Component sender;
	}
}
