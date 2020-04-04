using obServer.Model.GameModel;
using obServer.Model.GameModel.Item;
using obServer.Model.Interfaces;
using obServer.Network.Interface;
using obServer.Network.Structs;
using obServer.Repository.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;

namespace obServer.Logic
{
    public sealed class ClientLogic : BaseLogic
    {
        public ClientLogic(IobServerModel model)
        {
            gameClient = new RepoGameClient(serverPort, clientPort);
            gameClient.ReceiveRequest += OnReceive;
            gameClient.StartListening();
            this.model = model;
            AddMyPlayer();
        }

        private void OnReceive(object sender, IReceivedEventArgs e)
        {
            Task.Factory.StartNew(() => HandleRequests(e.ReceivedRequest));
        }

        private void AddMyPlayer()
        {
            IPlayer p = model.MyPlayer;
            var bounds = p.RealPrimitive.Bounds;
            model.ConstructItem(p);
            string parameters = $"Player;{p.Id};{p.Position.X};{p.Position.Y};{bounds.Width};{bounds.Height};{p.Rotation}";
            gameClient.Send(Operation.SendObject, parameters);
            p.Die += OnPlayerDie;
        }

        private void OnPlayerDie(object sender, EventArgs e)
        {
            IPlayer player = (sender as IPlayer);
            model.DestructItem(player.Id);
            gameClient.Send(Operation.Die, $"{player.Id}");
        }

        public EventHandler UpdateUI;

        public EventHandler Start;

        private IobServerModel model;

        private const int serverPort = 3200;

        private const int clientPort = 4500;

        private IRepoGameClient gameClient;

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

        public void OnPickup(object sender, PlayerInputEventArgs e)
        {
            if (e.Pickup)
            {
                var bounds = e.Player.RealPrimitive.Bounds;
                var pickWeapon = model.Weapons.Where(x => x.RealPrimitive.Bounds.IntersectsWith(bounds));
                if (pickWeapon.Count() > 0)
                {
                    var weapon = pickWeapon.First();
                    gameClient.Send(Operation.Pickup, $"{e.Player.Id};{weapon.Id}");
                }
                UpdateUI?.Invoke(this, null);
            }
        }

        public void OnReload(object sender, PlayerInputEventArgs e)
        {
            if (e.Reload)
            {
                if (e.Player.CurrentWeapon != null)
                {
                    e.Player.Reload();
                }
            }
        }

        public void OnMove(object sender, PlayerInputEventArgs e)
        {
            if (e.Movement != null)
            {
                double angle = e.Player.Rotation;
                if (e.Angle != 0)
                {
                    angle = e.Angle;
                }
                e.Player.Move(e.Movement[0], e.Movement[1], e.deltaTime, e.Angle);
                var bounds = e.Player.RealPrimitive.Bounds;
                var closeItems = model.Colliders.Where(x => x != e.Player && x.GetType() != typeof(Bullet) && bounds.IntersectsWith(x.RealPrimitive.Bounds));
                var colliders = closeItems.Where(x => x.CollidesWith(e.Player.RealPrimitive));
                foreach (var collide in colliders)
                {
                    e.Player.Move(-e.Movement[0], -e.Movement[1], e.deltaTime, e.Angle);
                    break;
                }
                string parameters = $"Player;{e.Player.Id};{e.Player.Position.X};{e.Player.Position.Y};{bounds.Width};{bounds.Height};{e.Player.Rotation}";
                UpdateUI?.Invoke(this, null);
                gameClient.Send(Operation.Move, parameters);
            }
        }

        public void OnShoot(object sender, PlayerInputEventArgs e)
        {
            if (e.Shoot)
            {
                var bullets = e.Player.Shoot();
                if (bullets != null)
                {
                    foreach (var bullet in bullets)
                    {
                        model.ConstructItem(bullet);
                        var bounds = bullet.RealPrimitive.Bounds;
                        double[] direction = bullet.Direction;
                        string parameters = $"Bullet;{bullet.Id};{bullet.Position.X};{bullet.Position.Y};{bounds.Width};{bounds.Height};{bullet.Rotation};" +
                            $"{bullet.BulletDamage};{direction[0]};{direction[1]};{bullet.Speed};{bullet.Weight};{Milis};{e.Player.Id}";
                        gameClient.Send(Operation.Shoot, parameters);
                    }
                }
            }
        }

        public void FlyBullets(double deltaTime)
        {
            var bullets = model.Bullets;
            int c = bullets.Count();
            for (int i = 0; i < bullets.Count(); i++)
            {
                var ibullet = (IBullet)bullets.ElementAt(i);
                if ( ibullet.RealSpeed < 80)
                {
                    model.DestructItem(ibullet.Id);
                    //  Send Message
                    UpdateUI?.Invoke(this, null);
                    continue;
                }
                var bounds = ibullet.RealPrimitive.Bounds;
                ibullet.Fly(deltaTime);
                var items = model.Colliders.Where(x => x.RealPrimitive.Bounds.IntersectsWith(bounds) && x.GetType() != typeof(Bullet));
                for (int y = 0; y < items.Count(); y++)
                {
                        if (items.ElementAt(y).CollidesWith(bullets.ElementAt(i).RealPrimitive))
                        {
                            if (items.ElementAt(y).GetType() == typeof(Player))
                            {
                                ibullet.DoDamage((Player)items.ElementAt(y));
                                gameClient.Send(Operation.Hit, $"{items.ElementAt(y).Id};{ibullet.Id}");
                                model.DestructItem(ibullet.Id);
                                //  Send Message
                                break;
                            }
                            else
                            {
                                gameClient.Send(Operation.Remove, $"{ibullet.Id}");
                                model.DestructItem(ibullet.Id);
                                break;
                            }
                        }
                    }
                }
                UpdateUI?.Invoke(this, null);
        }
        protected override void HandleConnect(Request request) { }

        protected override void HandleDie(Request request) { }

        protected override void HandleDisconnect(Request request) { }

        protected override void HandleHit(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid playerId = Guid.Parse(zones[0]);
            Guid bulletId = Guid.Parse(zones[1]);
            var bullets = model.Bullets.Where(x => x.Id == bulletId);
            var players = model.Players.Where(x => x.Id == playerId);
            if (players.Count() > 0)
            {
                IPlayer player = (IPlayer)players.First();
                if (bullets.Count() > 0)
                {
                    IBullet bullet = (IBullet)bullets.First();
                    bullet.DoDamage(player);
                    model.DestructItem(bullet.Id);
                }
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
            Guid id = Guid.Parse(zones[0]);
            model.UpdateItem(id, position[0], position[1], dimensions[0], dimensions[1], rotation);
        }

        protected override void HandlePickup(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid playerId = Guid.Parse(zones[0]);
            Guid weaponId = Guid.Parse(zones[1]);
            if (request.Reply == "1")
            {
                var player = (IPlayer)model.Players.Where(x => x.Id == playerId).First();
                var weapon = (IWeapon)model.Weapons.Where(x => x.Id == weaponId).First();
                player.ChangeWeapon(weapon);
            }
        }

        protected override void HandleReady(Request request)
        {
            if (request.Reply == "1")
            {
                Start?.Invoke(this, null);
            }
        }

        protected override void HandleRemove(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid Id = Guid.Parse(zones[0]);
            model.DestructItem(Id);
        }

        protected override void HandleSendMessage(Request request) { }

        protected override void HandleSendObject(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid id = Guid.Parse(zones[1]);
            double[] position = new double[] { double.Parse(zones[2]), double.Parse(zones[3]) };
            double rotation = double.Parse(zones[6]);
            IPlayer player = new Player(Player.PlayerGeometry, id, position, rotation, true, 100);
            model.ConstructItem(player);
        }

        protected override void HandleShoot(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid id = Guid.Parse(zones[1]);
            if (request.Reply  == "1")
            {
                var bullets = model.Bullets.Where(x => x.Id == id);
                if (bullets.Count()<1)
                {
                    double[] position = new double[] { double.Parse(zones[2]), double.Parse(zones[3]) };
                    double[] direction = new double[] { double.Parse(zones[7]), double.Parse(zones[8]) };
                    double damage = double.Parse(zones[7]);
                    double rotation = double.Parse(zones[6]);
                    double speed = double.Parse(zones[10]);
                    double weight = double.Parse(zones[11]);
                    double sendTime = double.Parse(zones[12]);
                    IBullet bull = new Bullet(Bullet.BulletGeometry, id, position, rotation, true, speed, damage, direction, weight);
                    bull.Fly(Milis - sendTime);
                    model.ConstructItem(bull);
                }
            }
            else
            {
                model.DestructItem(id);
            }
        }

        public void Add(IBaseItem item) { }

    }
}
