using obServer.Network.Interface;
using obServer.Network.NetworkElements;
using obServer.Network.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.Repository.Network
{
    public class RepoGameServer : IRepoGameServer
    {
        public RepoGameServer()
        {
            gameServer = new GameServer();
        }

        private IGameServer gameServer;
        public void StartListening()
        {
            gameServer.StartListening();
        }

        public Request GetRequest()
        {
            return gameServer.GetRequest();
        }

        public void StopListening()
        {
            gameServer.StopListening();
        }

        public void ReplyHandler(Request request)
        {
            gameServer.ReplyHandler(request);
        }
    }
}
