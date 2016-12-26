using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Pong.Structure.Messaging;

namespace Pong.Structure
{
    public class Blackboard
    {
        private static List<MessageListener> _messageListeners;

        public static void AddMessageListener(MessageListener listener)
        {
            if (_messageListeners == null) _messageListeners = new List<MessageListener>();

            _messageListeners.Add(listener);
        }

        private static List<Entity> _entities;

        public static List<Entity> Entities => _entities ?? (_entities = new List<Entity>());

        public static int WindowWidth { get; set; }
        public static int WindowHeight { get; set; }
        public static Vector2 Center => new Vector2(WindowWidth / 2f, WindowHeight / 2f);

        public static Entity GetEntity(string name)
        {
            return Entities.First(e => e.Name == name);
        }
        public static Entity[] GetEntities(string searchTerm)
        {
            return Entities.Where(e => e.Name.Contains(searchTerm)).ToArray();
        }

        public static void PostMessage(Message message)
        {
            if (_messageListeners == null) _messageListeners = new List<MessageListener>();
            else
            {
                foreach (var listener in _messageListeners)
                {
                    if (Equals(listener.Message, message)) listener.OnMessageRecieved(message.Arguments);
                }
            }
        }
    }
}
