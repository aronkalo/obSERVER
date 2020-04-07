using System;
using System.Threading.Tasks;
using obServer.Network.Interface;
using obServer.Network.NetworkController;
using obServer.Network.Structs;


namespace obServer.Network.NetworkElements
{
    public sealed class GameClient : GameBase, IGameClient
    {
        public GameClient() : base()
        {
            Connected += OnConnect;
            OfflinePool = new RequestPool();
            Network = new UdpUser();
            Task.Factory.StartNew(() =>
            {
                //Network.PingServer();
                StartListening();
                Connected?.Invoke(this, EventArgs.Empty);
            });
        }
        private bool ActiveSession { get; set; }
        private RequestPool OfflinePool { get; set; }
        private UdpUser Network { get; set; }
        public EventHandler<IReceivedEventArgs> Receive { get; set; }
        private EventHandler Connected { get; set; }
        private void OnConnect(object sender, EventArgs e)
        {
            Request prevRequest = new Request();
            while (OfflinePool.NotNullElement())
            {
                var request = OfflinePool.GetRequest();
                if (request.Operation != prevRequest.Operation || request.Parameters != prevRequest.Parameters)
                {
                    Send(request.Operation, request.Parameters);
                }
                prevRequest = request;
            }
        }
        public void Send(Operation operation, string parameters)
        {
            if (this.Network != null)
            {
                Task.Factory.StartNew(() => Network.Send(operation, parameters));
            }
            else
            {
                OfflinePool.AddPoolElement(new Request() { Operation = operation, Parameters = parameters });
            }
        }

        public void StartListening()
        {
            ActiveSession = true;
            Task.Factory.StartNew( async () =>
                {
                    while (ActiveSession)
                    {
                        var received = await Network.Receive();
                        Receive?.Invoke(this, new ReceivedEventArgs(received));
                    }
                });
        }
    }
}
