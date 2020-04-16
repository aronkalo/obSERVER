using obServer.GameModel;
using obServer.GameModel.Interfaces;
using obServer.Network.Interface;
using obServer.Network.Structs;
using obServer.Repository.Network;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace obServer.GameLogic
{
    public class ServerLogic : BaseLogic
    {
        public ServerLogic()
        {
            gameServer = new RepoGameServer();
            model = new ServerSideModel();
            gameServer.StartListening();
            Serve();
        }

        private IRepoGameServer gameServer;
        private ServerSideModel model;

        public void Serve()
        {
            Task.Factory.StartNew(() => 
            {
                while (true)
                {
                    Request? oldest = gameServer.GetRequest();
                    if (oldest != null)
                    {
                        HandleRequests(oldest.Value);
                    }
                }
            });
        }

        public void Update(double deltaTime)
        {
            var bullets = model.Bullets;
            for (int i = 0; i < bullets.Count(); i++)
            {
                var bullet = bullets.ElementAt(i);
                Vector start = bullet.StartPoint;
                double x = bullet.Bounds.X;
                double y = bullet.Bounds.Y;
                double a = x - start.X;
                double b = y - start.Y;
                double suppression = 1 - (Math.Sqrt((a * a) + (b * b)) * bullet.Weight);
                double movement = bullet.Speed * deltaTime * suppression;
                Vector direction = bullet.Direction;
                bullet.Bounds = new Rect( x + (direction.X * movement), y + (direction.Y * movement), 
                    bullet.Bounds.Width, bullet.Bounds.Height);
                var items = model.Collision(bullet.Id);
                if (items.Count() > 0)
                {
                    model.DestructItem(bullet.Id);
                    var players = items.Where(z => z.Type == "Player");
                    if (players.Count() > 0)
                    {
                        gameServer.ReplyHandler(new Request() { Broadcast = true, Operation = Operation.Hit, Parameters = $"{players.First().Id};{bullet.Id}" });
                    }
                    gameServer.ReplyHandler(new Request() { Broadcast = true, Operation = Operation.Remove, Parameters = $"{bullet.Id}" });
                }
            }
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
                    }
        }

        protected override void HandleConnect(Request request)
        {
            request.Reply = "1";
            request.Broadcast = true;
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
        }

        protected override void HandleDisconnect(Request request)
        {
            request.Broadcast = true;
            request.Reply = "1";
            gameServer.ReplyHandler(request);
        }

        protected override void HandleHit(Request request)
        {
            Guid id = Guid.Parse(request.Parameters.Split(';')[0]);
            Guid bulletid = Guid.Parse(request.Parameters.Split(';')[1]);
            var bullet = model.AllItems.Where(x => x.Id == bulletid);
            if (bullet.Count() > 0)
            {
                model.DestructItem(bulletid);
                request.Broadcast = true;
                request.Reply = "1";
                gameServer.ReplyHandler(request);
            }
        }

        protected override void HandleMove(Request request)
        {
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
            double sendTime = double.Parse(zones[7]);
            Debug.WriteLine($"Server Time: {(Milis - sendTime)/10000}");
            Guid id = Guid.Parse(zones[1]);
            model.ConstructItem(id, zones[0], new Rect(double.Parse(zones[2]), double.Parse(zones[3]), double.Parse(zones[4]), double.Parse(zones[5])), true) ;
            request.Reply = "1";
            request.Broadcast = true;
            gameServer.ReplyHandler(request);
        }

        protected override void HandlePickup(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid playerId = Guid.Parse(zones[0]);
            Guid weaponId = Guid.Parse(zones[1]);
            var weapons = model.Collision(playerId).Where(x => x.Id == weaponId && !x.Owned);
            if (weapons.Count() == 1)
            {
                var weapon = weapons.First();
                weapon.Owned = true;
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
                gameServer.ReplyHandler(request);
            }
        }

        protected override void HandleRemove(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid id = Guid.Parse(zones[0]);
            var items = model.AllItems.Where(x => x.Id == id);
            if (items.Count() > 0)
            {
                model.DestructItem(id);
                request.Reply = "1";
                request.Broadcast = true;
                gameServer.ReplyHandler(request);
            }
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
            model.ConstructItem(id, type,new Rect(double.Parse(zones[2]), double.Parse(zones[3]), double.Parse(zones[2]), double.Parse(zones[3])), true);
            request.Reply = "1";
            request.Broadcast = true;
            gameServer.ReplyHandler(request);
        }

        protected override void HandleShoot(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid player = Guid.Parse(zones[13]);
            if (model.Players.Where(x => x.Id == player).Count() == 1)
            {
                string type = zones[0];
                Guid id = Guid.Parse(zones[1]);
                double x = double.Parse(zones[2]);
                double y = double.Parse(zones[3]);
                double width = double.Parse(zones[4]);
                double height = double.Parse(zones[5]);
                double rotation = double.Parse(zones[6]);
                Vector direction = new Vector( double.Parse(zones[7]), double.Parse(zones[8]) );
                double damage = double.Parse(zones[7]);
                double speed = double.Parse(zones[10]);
                double weight = double.Parse(zones[11]);
                double delta = Milis - double.Parse(zones[12]);
                double xloc = (x + (direction.X * delta * speed));
                double yloc = (y + (direction.Y * delta * speed));
                double a = x - xloc;
                double b = y - yloc;
                double suppression = 1 - (Math.Sqrt((a * a) + (b * b)) * weight);
                xloc = xloc * suppression;
                yloc = yloc * suppression;
                model.ConstructBullet(id, type, new Rect(xloc,yloc,width,height), true, direction, weight, speed);
                request.Reply = "1";
                request.Broadcast = true;
                gameServer.ReplyHandler(request);
            }
        }
    }
}
