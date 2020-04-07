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
    public class RepoGameClient : IRepoGameClient
    {
        public RepoGameClient()
        {
            gameClient = new GameClient();
            gameClient.Receive += OnReceive;
        }

        public EventHandler<IReceivedEventArgs> ReceiveRequest { get; set; }

        private void OnReceive(object sender, IReceivedEventArgs e)
        {
            ReceiveRequest?.Invoke(sender, e);
        }


        private IGameClient gameClient;

        public void Send(Operation operation, string parameters)
        {
            gameClient.Send(operation, parameters);
        }

        public void StartListening()
        {
            gameClient.StartListening();
        }


        public EventHandler<ReceivedEventArgs> Receive;
    }
}
