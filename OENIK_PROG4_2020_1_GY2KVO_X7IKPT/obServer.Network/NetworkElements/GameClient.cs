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

        public void Send(Operation operation, string parameters)
        {
            Network.Send(operation, parameters);
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
                        ResponsePool.AddPoolElement(received);
                    }
                });
        }

        public Request GetResponse()
        {
            return ResponsePool.GetRequest();
        }

        //[STAThread]
        //private void HandleResponseMethod()
        //{
        //    Thread responseThread = new Thread(() =>
        //    {

        //            if (ResponsePool.NotNullElement())
        //            {
        //                Request oldestRequest = ResponsePool.GetRequest();
        //                if (oldestRequest.Operation !=0)
        //                {
        //                    clientLogic.HandleResponse(oldestRequest);
        //                }
        //            }
                
        //    });
        //    responseThread.SetApartmentState(ApartmentState.STA);
        //    responseThread.Start();
        //}

        //public IEnumerable<Tuple<Guid, double[], GameObjectType>> GetChanges()
        //{
        //    return clientLogic.GetUpdatedVisuals();
        //}

        //public IEnumerable<Tuple<Guid, double[], GameObjectType>> GetDeletes()
        //{
        //    return clientLogic.GetDeletedVisuals();
        //}

        //public double[] HandleMovement(double[] playerParams, Vector movement, Vector rotation)
        //{
        //    double sum = movement.X + movement.Y;
        //    double angle = Vector.AngleBetween(new Vector(0, -1), rotation);
        //    if (sum != 0 || angle != playerParams[4])
        //    {
        //        double x = playerParams[0] + (movement.X * playerParams[5]);
        //        double y = playerParams[1] + (movement.Y * playerParams[5]);
        //        var rs = clientLogic.DoMovePlayer(x, y, angle);

        //        foreach (var r in rs)
        //        {
        //            Send(r.Operation, r.Parameters);
        //        }
        //        return new double[] { x, y };
        //    }
        //    return new double[] { playerParams[0], playerParams[1] };
        //}

        //public void HandleShoot(bool fire)
        //{
        //    if (fire)
        //    {
        //        Request[] rs = clientLogic.DoShootWeapon();
        //        foreach (var r in rs)
        //        {
        //            Send(r.Operation, r.Parameters);
        //        }
        //    }
        //}

        //public void AddPlayer()
        //{
        //    Request r = clientLogic.DoAddPlayer();
        //    Send(r.Operation, r.Parameters);
        //}

        //public void PingReady()
        //{
        //    Send(GameOperation.CheckServerAvaliable, "READY");
        //}

        //public void BulletFly(double deltaTime)
        //{
        //    clientLogic.BulletFly(deltaTime);
        //}
    }
}
