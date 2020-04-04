using obServer.Model.GameModel;
using obServer.Model.Interfaces;
using obServer.Network.Interface;
using obServer.Network.Structs;
using obServer.Repository.Network;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace obServer.Logic
{
    public class ServerLogic : BaseLogic
    {
        public ServerLogic(int width, int height)
        {
            gameServer = new RepoGameServer();
            gameServer.ReceiveRequest += OnReceive;
            gameServer.StartListening();
            model = new ServerSideModel();
        }

        private IRepoGameServer gameServer;
        private int readyPlayers = 0;
        private ServerSideModel model;
        private void OnReceive(object sender, IReceivedEventArgs e)
        {
            Task.Factory.StartNew(() => HandleRequests(e.ReceivedRequest));
        }

        public void Update(double deltaTime)
        {

        }

        protected override void HandleRequests(Request request)
        {
                    switch (request.Operation)
                    {
                        case Operation.Connect:
                            HandleConnect(request);
                            break;
                        case Operation.Disconnect:
                            HandleDisconnect(request);
                            break;
                        case Operation.CheckServerAvaliable:
                            HandleReady(request);
                            break;
                        case Operation.SendObject:
                            HandleSendObject(request);
                            break;
                        case Operation.Remove:
                            HandleRemove(request);
                            break;
                        case Operation.Die:
                            HandleDie(request);
                            break;
                        case Operation.Hit:
                            HandleHit(request);
                            break;
                        case Operation.SendChatMessage:
                            HandleSendMessage(request);
                            break;
                        case Operation.Shoot:
                            HandleShoot(request);
                            break;
                        case Operation.Move:
                            HandleMove(request);
                            break;
                        case Operation.Pickup:
                            HandlePickup(request);
                            break;
                        default:
                            throw new Exception("Not implemented Exception");
                    }
        }

        protected override void HandleConnect(Request request)
        {
            request.Reply = "1";
            gameServer.ReplyHandler(request);
        }

        protected override void HandleDie(Request request)
        {
            Guid id = Guid.Parse(request.Parameters.Split(';')[0]);
            var items = model.AllItems.Where(x => x.Id == id);
            if (items.Count() > 0)
            {
                model.DestructItem(id);
                request.Reply = "1";
                request.Broadcast = true;
                gameServer.ReplyHandler(request);
            }
            else
            {
                request.Reply = "0";
                gameServer.ReplyHandler(request);
            }
        }

        protected override void HandleDisconnect(Request request)
        {
            request.Broadcast = true;
            request.Reply = "1";
            gameServer.ReplyHandler(request);
        }

        protected override void HandleHit(Request request)
        {
            
        }

        protected override void HandleMove(Request request)
        {
            //$"Player;{e.Player.Id};{e.Player.Position[0]};{e.Player.Position[1]};{bounds.Width};{bounds.Height};{e.Player.Rotation}";
            string[] zones = request.Parameters.Split(';');
            double[] position = new double[]
            {
                double.Parse(zones[2]) ,
                double.Parse(zones[3])
            };
            double[] dimensions = new double[]
            {
                double.Parse(zones[4]) ,
                double.Parse(zones[5])
            };
            double rotation = double.Parse(zones[6]);
            Guid id = Guid.Parse(zones[0]);
            model.ConstructItem(id, zones[1], new Rect(double.Parse(zones[2]), double.Parse(zones[3]), double.Parse(zones[4]), double.Parse(zones[5])));
            var bullets = model.BulletHit(id);
            request.Reply = "1";
            gameServer.ReplyHandler(request);
            foreach (var bullet in bullets)
            {
                gameServer.ReplyHandler(new Request()
                {
                    Operation = Operation.Hit,
                    Parameters = $"{id};{bullet.Id}",
                    Broadcast = true,
                });
            }
        }

        protected override void HandlePickup(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid playerId = Guid.Parse(zones[0]);
            Guid weaponId = Guid.Parse(zones[1]);
            if (model.Collision(playerId).Where( x => x.Id == weaponId).Count() == 1)
            {
                request.Reply = "1";
                request.Broadcast = true;
            }
            else
            {
                request.Reply = "0";
            }
            gameServer.ReplyHandler(request);
        }

        protected override void HandleReady(Request request)
        {
            string address = request.Sender.Address.ToString();
            gameServer.ReadyClient(address);
            if (gameServer.AllReady)
            {
                request.Reply = "1";
                request.Broadcast = true;
            }
            else
            {
                request.Reply = "0";
            }
            gameServer.ReplyHandler(request);
        }

        protected override void HandleRemove(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid id = Guid.Parse(zones[1]);
            double[] position = new double[] { double.Parse(zones[2]), double.Parse(zones[3]) };
            double rotation = double.Parse(zones[6]);
            var items = model.AllItems.Where(x => x.Id == id);
            if (items.Count() > 0)
            {
                model.DestructItem(id);
                request.Reply = "1";
                request.Broadcast = true;
            }
            else
            {
                request.Reply = "0";
            }
            gameServer.ReplyHandler(request);
        }

        protected override void HandleSendMessage(Request request)
        {
            base.HandleSendMessage(request);
        }

        protected override void HandleSendObject(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            string type = zones[0];
            Guid id = Guid.Parse(zones[1]);
            model.ConstructItem(id, type,new Rect(double.Parse(zones[2]), double.Parse(zones[3]), double.Parse(zones[2]), double.Parse(zones[3])));
            request.Reply = "1";
            request.Broadcast = true;
            gameServer.ReplyHandler(request);
        }

        protected override void HandleShoot(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid player = Guid.Parse(zones[13]);
            if (model.AllItems.Where(x => x.Id == player).Count() == 1)
            {
                string type = zones[0];
                Guid id = Guid.Parse(zones[1]);
                double x = double.Parse(zones[2]);
                double y = double.Parse(zones[3]);
                double width = double.Parse(zones[4]);
                double height = double.Parse(zones[5]);
                double rotation = double.Parse(zones[6]);
                double[] direction = new double[] { double.Parse(zones[7]), double.Parse(zones[8]) };
                double damage = double.Parse(zones[7]);
                double speed = double.Parse(zones[10]);
                double weight = double.Parse(zones[11]);
                double delta = Milis - double.Parse(zones[12]);
                x = x + (int)(direction[0] * delta * speed);
                y = y + (int)(direction[1] * delta * speed);
                model.ConstructItem(id, type, new Rect(x,y,width,height));
                request.Reply = "1";
                request.Broadcast = true;
            }
            else
            {
                request.Reply = "0";
            }
            gameServer.ReplyHandler(request);
        }
    }
}
