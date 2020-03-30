using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using obServer.Network.Interface;
using obServer.Network.NetworkController;
using obServer.Network.Structs;


namespace obServer.Network.NetworkElements
{
    public sealed class GameServer : GameBase, IGameServer
    {
        public GameServer() : base() { }

        private UdpListener Listener { get; set; }
        private RequestPool RequestPool { get; set; }
        private bool ActiveSession { get; set; }

        private List<GameClientInfo> Clients { get; set; }

        public void ReadyClient(string ipAddress)
        {
            Clients.ForEach(x => x.SetReady(ipAddress)); 
        }

        public bool AllReady
        {
            get
            {
                return Clients.All(x => x.Ready);
            }
        }

        public void StartListening()
        {
            if (this.Listener == null)
            {
                Listener = new UdpListener();
                ActiveSession = true;
                this.RequestPool = new RequestPool();
                Task.Factory.StartNew(async () =>
                {
                    while (ActiveSession)
                    {
                        var recieved = await Listener.Receive();
                        CheckClients(recieved);
                        RequestPool.AddPoolElement(recieved);
                    }
                });
            }
            else
            {
                throw new Exception("Already listening");
            }
        }

        private void CheckClients(Request recieved)
        {
            if (Clients.Where( x => x.EndPoint == recieved.Sender).Count() == 0)
            {
                Clients.Add(new GameClientInfo() {
                    EndPoint = recieved.Sender,
                    Id = Guid.NewGuid(),
                    Ready = false,
                });
            }
        }

        private void Reply(Request request)
        {
            Listener.Reply(request, request.Reply);
        }

        private void Send(Request request, GameClientInfo clientInfo)
        {
            Listener.Send(clientInfo.EndPoint, request.Operation, request.Parameters, request.Reply);
        }

        public Request GetRequest()
        {
            return RequestPool.GetRequest();
        }

        public void StopListening()
        {
            ActiveSession = false;
        }

        public void ReplyHandler(Request reply)
        {
            if (reply.Broadcast)
            {
                foreach (var gameClient in Clients)
                {
                    Send(reply, gameClient);
                }
            }
            else
            {
                Reply(reply);
            }
        }
    }
}
