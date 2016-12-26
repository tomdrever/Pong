using System.Collections.Generic;

namespace Pong.Structure.Messaging  
{
    public class Messageboard
    {
        private static List<MessageListener> _messageListeners;

        public static void AddMessageListener(MessageListener listener)
        {
            if (_messageListeners == null) _messageListeners = new List<MessageListener>();

            _messageListeners.Add(listener);
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
