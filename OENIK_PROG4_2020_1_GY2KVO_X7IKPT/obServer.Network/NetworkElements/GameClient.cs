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
            Network = new UdpUser();
        }
        private bool ActiveSession { get; set; }
        private UdpUser Network { get; set; }
        public EventHandler<IReceivedEventArgs> Receive { get; set; }
        public void Send(Operation operation, string parameters)
        {
            Task.Factory.StartNew(() => Network.Send(operation, parameters));
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
