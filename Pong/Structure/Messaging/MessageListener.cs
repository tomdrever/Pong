using System.Collections.Generic;

namespace Pong.Structure.Messaging
{
    public class MessageListener
    {
        public Message Message { get; set; }

        public delegate void OnMessageRecievedDelegate(Dictionary<string, string> args);

        public OnMessageRecievedDelegate OnMessageRecieved { get; set; }
    }
}
