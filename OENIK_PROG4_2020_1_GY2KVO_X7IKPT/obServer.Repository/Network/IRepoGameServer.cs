using obServer.Network.Interface;
using obServer.Network.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.Repository.Network
{
    public interface IRepoGameServer
    {
        void StartListening();

        void StopListening();

        void ReplyHandler(Request request);

        bool AllReady { get; }

        void ReadyClient(string address);

        Request? GetRequest();
    }
}
