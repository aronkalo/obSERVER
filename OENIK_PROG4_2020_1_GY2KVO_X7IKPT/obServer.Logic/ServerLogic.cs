using obServer.Model.GameModel;
using obServer.Model.Interfaces;
using obServer.Network.Structs;
using obServer.Repository.Network;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace obServer.Logic
{
    public class ServerLogic : BaseLogic
    {
        public ServerLogic(int width, int height)
        {
            gameServer = new RepoGameServer();
            gameServer.StartListening();
            model = new ServerData(width, height);
        }

        private IRepoGameServer gameServer;

        private ServerData model;

        protected override void HandleRequests()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Request r = gameServer.GetRequest();
                    switch (r.Operation)
                    {
                        case Operation.Connect:
                            HandleConnect(r);
                            break;
                        case Operation.Disconnect:
                            HandleDisconnect(r);
                            break;
                        case Operation.CheckServerAvaliable:
                            HandleReady(r);
                            break;
                        case Operation.SendObject:
                            HandleSendObject(r);
                            break;
                        case Operation.Remove:
                            HandleRemove(r);
                            break;
                        case Operation.Die:
                            HandleDie(r);
                            break;
                        case Operation.Hit:
                            HandleHit(r);
                            break;
                        case Operation.SendChatMessage:
                            HandleSendMessage(r);
                            break;
                        case Operation.Shoot:
                            HandleShoot(r);
                            break;
                        case Operation.Move:
                            HandleMove(r);
                            break;
                        case Operation.Pickup:
                            HandlePickup(r);
                            break;
                        default:
                            throw new Exception("Not implemented Exception");
                    }
                }
                catch (Exception) { }
            });
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
            model.ConstructItem(id, zones[1], position, dimensions, rotation);
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
            if (model.WeaponCollide(playerId).Where( x => x.Id == weaponId).Count() == 1)
            {
                request.Reply = "1";
                request.Broadcast = true;
                gameServer.ReplyHandler(request);
            }
            else
            {
                request.Reply = "0";
                gameServer.ReplyHandler(request);
            }
            //$"{e.Player.Id};{weapon.Id}"

        }

        private int readyPlayers;

        protected override void HandleReady(Request request)
        {
            string address = request.Sender.Address.ToString();
            gameServer.ReadyClient(address);
            if (gameServer.AllReady)
            {
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

        protected override void HandleRemove(Request request)
        {

        }

        protected override void HandleSendMessage(Request request)
        {
            base.HandleSendMessage(request);
        }

        protected override void HandleSendObject(Request request)
        {
            //$"Bullet;{bullet.Id};{bullet.Position[0]};{bullet.Position[1]};{bounds.Width};{bounds.Height};{bullet.Rotation}"
        }

        protected override void HandleShoot(Request request)
        {
            //$"Bullet;{bullet.Id};{bullet.Position[0]};{bullet.Position[1]};{bounds.Width};{bounds.Height};{bullet.Rotation}"
            string[] zones = request.Parameters.Split(';');
            string type = zones[0];
            Guid id = Guid.Parse(zones[1]);
            double x = double.Parse(zones[2]);
            double y = double.Parse(zones[3]);
            double width = double.Parse(zones[4]);
            double height = double.Parse(zones[5]);
            double rotation = double.Parse(zones[5]);
            model.ConstructItem(id, type, new double[] { x, y }, new double[] { width, height }, rotation);
            request.Reply = "1";
            request.Broadcast = true;
            gameServer.ReplyHandler(request);
        }
    }
}
