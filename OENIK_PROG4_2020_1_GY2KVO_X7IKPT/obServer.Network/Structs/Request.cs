using System.Net;
using obServer.Network.Structs;

namespace obServer.Network.Structs
{
    public struct Request
    {
        private IPEndPoint sender;

        public IPEndPoint Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        private Operation operation;

        public Operation Operation
        {
            get { return operation; }
            set { operation = value; }
        }

        private string parameters;

        public string Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        private string reply;

        public string Reply
        {
            get { return reply; }
            set { reply = value; }
        }

        private bool broadcast;

        public bool Broadcast
        {
            get { return broadcast; }
            set { broadcast = value; }
        }
    }
}
