using System;
using System.Threading.Tasks;
using obServer.Network.Interface;
using obServer.Network.NetworkController;
using obServer.Network.Structs;


namespace obServer.Network.NetworkElements
{
    public sealed class GameClient : GameBase, IGameClient
    {
        public GameClient(int serverPort, int clientPort) : base()
        {
            var ServerIp = UdpUser.SearchForServers(serverPort, clientPort);
            this.Network = UdpUser.ConnectTo(ServerIp, clientPort);
        }
        private bool ActiveSession { get; set; }
        private RequestPool ResponsePool { get; set; }
        private UdpUser Network { get; set; }
        public EventHandler<IReceivedEventArgs> Receive { get; set; }

        public void Send(Operation operation, string parameters)
        {
            Task.Factory.StartNew(() => Network.Send(operation, parameters));
        }

        public void StartListening()
        {
            ActiveSession = true;
            this.ResponsePool = new RequestPool();
            Task.Factory.StartNew(async () =>
                {
                    while (ActiveSession)
                    {
                        var received = await Network.Receive();
                        Receive?.Invoke(this, new ReceivedEventArgs() { ReceivedRequest = received });
                    }
                });
        }

        public Request GetResponse()
        {
            return ResponsePool.GetRequest();
        }
    }
}
