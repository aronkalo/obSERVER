using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using obServer.Network.Interface;
using obServer.Network.NetworkController;
using obServer.Network.Structs;


namespace obServer.Network.NetworkElements
{
    public sealed class GameServer : GameBase, IGameServer
    {
        public GameServer() : base() { Clients = new List<GameClientInfo>(); RequestPool = new RequestPool(); Receive += OnReceive; }

        private static object clientLock = new Object();
        private UdpListener Listener { get; set; }
        private RequestPool RequestPool { get; set; }
        private bool ActiveSession { get; set; }
        private List<GameClientInfo> Clients { get; set; }
        private EventHandler<ReceivedEventArgs> Receive { get; set; }

        public void ReadyClient(string ipAddress)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                Clients[i].SetReady(ipAddress);
            }
        }

        public bool AllReady
        {
            get
            {
                return Clients.All(x => x.Ready);
            }
        }

        private void OnReceive(object sender, ReceivedEventArgs e)
        {
            Task.Factory.StartNew(() => 
            {
                RequestPool.AddPoolElement(e.ReceivedRequest);
                CheckClients(e.ReceivedRequest);
            });
        }

        public void StartListening()
        {
            if (this.Listener == null)
            {
                Listener = new UdpListener();
                ActiveSession = true;
                Task.Factory.StartNew(async () =>
                {
                    while (ActiveSession)
                    {
                        var received = await Listener.Receive();
                        Receive?.Invoke(this, new ReceivedEventArgs(received));
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
            lock (clientLock)
            {
                if (Clients.Where(x => x.EndPoint.ToString() == recieved.Sender.ToString()).Count() == 0)
                {
                    Clients.Add(new GameClientInfo()
                    {
                        EndPoint = recieved.Sender,
                        Id = Guid.NewGuid(),
                        Ready = false,
                    });
                }
            }
        }

        private void Reply(Request request)
        {
            Listener.Reply(request, request.Reply);
        }

        private void Send(Request request)
        {
            Listener.Send(request.Operation, request.Parameters, request.Reply);
        }

        public void StopListening()
        {
            ActiveSession = false;
        }

        public void ReplyHandler(Request reply)
        {
            if (reply.Broadcast)
            {
                Send(reply);
            }
            else
            {
                Reply(reply);
            }
        }

        public Request? GetRequest()
        {
            if (RequestPool.NotNullElement())
            {
                return RequestPool.GetRequest();
            }
            return null;
        }
    }
}
