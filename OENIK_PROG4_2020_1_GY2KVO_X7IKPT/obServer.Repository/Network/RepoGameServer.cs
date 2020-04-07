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

        public EventHandler<IReceivedEventArgs> ReceiveRequest { get; set; }

        private void OnReceive(object sender, IReceivedEventArgs e)
        {
            ReceiveRequest?.Invoke(sender, e);
        }


        private IGameServer gameServer;
        public void StartListening()
        {
            gameServer.StartListening();
        }
        public void StopListening()
        {
            gameServer.StopListening();
        }

        public void ReplyHandler(Request request)
        {
            gameServer.ReplyHandler(request);
        }

        public bool AllReady { get { return gameServer.AllReady; } }

        public void ReadyClient(string address)
        {
            gameServer.ReadyClient(address);
        }

        public Request? GetRequest()
        {
            return gameServer.GetRequest();
        }
    }
}
