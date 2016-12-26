using System.Collections.Generic;

namespace Pong.Structure.Messaging
{
    public class Message
    {
        public Message(string main)
        {
            Main = main;
        }

        public string Main { get; set; }

        private Dictionary<string, string> _arguments;
        public Dictionary<string, string> Arguments => _arguments ?? (_arguments = new Dictionary<string, string>());

        public override bool Equals(object obj)
        {
            var message = obj as Message;
            if (message != null)
            {
                return message.Main == Main;
            }
            return base.Equals(obj);
        }
    }
}