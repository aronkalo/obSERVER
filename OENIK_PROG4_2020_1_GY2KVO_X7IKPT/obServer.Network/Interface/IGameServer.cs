using obServer.Network.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.Network.Interface
{
    public interface IGameServer : IGameBase
    {
        void StartListening();

        Request GetRequest();

        void StopListening();

        void ReplyHandler(Request request);

        bool AllReady { get; }

        void ReadyClient(string address);

        EventHandler<IReceivedEventArgs> Receive { get; set; }


    }
}
