using obServer.Model.GameModel;
using obServer.Model.GameModel.Item;
using obServer.Model.Interfaces;
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
            gameClient.StartListening();
            this.model = model;
        }

        public EventHandler UpdateUI;

        public EventHandler Start;

        private IobServerModel model;

        private const int serverPort = 3200;

        private const int clientPort = 4500;

        private IRepoGameClient gameClient;

        protected override void HandleRequests()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Request r = gameClient.GetResponse();
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

        public void OnPickup(object sender, PlayerInputEventArgs e)
        {
            if (e.Pickup)
            {
                var close = model.GetCloseItems(e.Player.Id);
                var pickWeapon = model.Weapons.Where(x => close.Contains(x.Id));
                if (pickWeapon.Count() > 0)
                {
                    var weapon = pickWeapon.First();
                    gameClient.Send(Operation.Pickup, $"{e.Player.Id};{weapon.Id}");
                }
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
            e.Player.Move(e.Movement[0], e.Movement[1], e.deltaTime, e.Angle);
            model.ConstructItem(e.Player);
            var bounds = e.Player.RealPrimitive.Bounds;
            string parameters = $"Player;{e.Player.Id};{e.Player.Position[0]};{e.Player.Position[1]};{bounds.Width};{bounds.Height};{e.Player.Rotation}";
            gameClient.Send(Operation.Move, parameters);
        }

        public void OnShoot(object sender, PlayerInputEventArgs e)
        {
            var bullets = e.Player.Shoot();
            foreach (var bullet in bullets)
            {
                model.ConstructItem(bullet);
                var bounds = bullet.RealPrimitive.Bounds;
                string parameters = $"Bullet;{bullet.Id};{bullet.Position[0]};{bullet.Position[1]};{bounds.Width};{bounds.Height};{bullet.Rotation}";
                gameClient.Send(Operation.Shoot, parameters);
            }
        }

        public void FlyBullets(double deltaTime)
        {
            var bullets = model.Bullets;
            foreach (var bullet in bullets)
            {
                bullet.Fly(deltaTime);
                var ids = model.GetCloseItems(bullet.Id);
                foreach (var id in ids)
                {
                    var it = model.AllItems.Where(x => x.Id == id);
                    if (it.Count() > 0)
                    {
                        var item = it.First();
                        if (item.CollidesWith(bullet.RealPrimitive))
                        {
                            if (item.GetType() == typeof(Player))
                            {
                                bullet.DoDamage((Player)item);
                                gameClient.Send(Operation.Hit, $"{item.Id};{bullet.Id}");
                            }
                            else
                            {
                                model.DestructItem(bullet.Id);
                                gameClient.Send(Operation.Remove, $"{bullet.Id}");
                            }
                        }
                    }
                }
            }
        }

        protected override void HandleConnect(Request request)
        {

        }

        protected override void HandleDie(Request request)
        {

        }

        protected override void HandleDisconnect(Request request) { }

        protected override void HandleHit(Request request)
        {
            //$"{item.Id};{bullet.Id}"
            string[] zones = request.Parameters.Split(';');
            Guid playerId = Guid.Parse(zones[0]);
            Guid bulletId = Guid.Parse(zones[1]);

        }

        protected override void HandleMove(Request request) { }

        protected override void HandlePickup(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid playerId = Guid.Parse(zones[0]);
            Guid weaponId = Guid.Parse(zones[1]);
            if (request.Reply == "1")
            {
                var player = model.Players.Where(x => x.Id == playerId).First();
                var weapon = model.Weapons.Where(x => x.Id == weaponId).First();
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

        protected override void HandleSendMessage(Request request)
        {
            base.HandleSendMessage(request);
        }

        protected override void HandleSendObject(Request request)
        {
            base.HandleSendObject(request);
        }

        protected override void HandleShoot(Request request)
        {
        }

        public void Add(IBaseItem item)
        {
            model.ConstructItem(item);
        }
    }
}
