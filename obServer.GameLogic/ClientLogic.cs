// <copyright file="ClientLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using obServer.GameModel.Interfaces;
    using obServer.GameModel.Item;
    using obServer.Logic.Interfaces;
    using obServer.Model.Interfaces;
    using obServer.Network.Interface;
    using obServer.Network.Structs;
    using obServer.Repository.GameModel;
    using obServer.Repository.Network;

    /// <summary>
    /// The client logic.
    /// </summary>
    public sealed class ClientLogic : BaseLogic, IClientLogic
    {
        private const string FileName = "Map.xml";
        private static readonly object ModelLock = new object();
        private IRepoOBServerModel model;
        private IRepoGameClient gameClient;
        private bool activeGame;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientLogic"/> class.
        /// </summary>
        /// <param name="model"> The model.</param>
        public ClientLogic(IRepoOBServerModel model)
        {
            this.model = model;
            this.model.LoadItems(FileName);

            this.gameClient = new RepoGameClient();
            this.gameClient.ReceiveRequest += this.OnReceive;
            this.gameClient.StartListening();
            this.Clients = new Dictionary<string, string>();

            this.Reload += this.OnReload;
            this.Shoot += this.OnShoot;
            this.Pick += this.OnPickup;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ClientLogic"/> class.
        /// </summary>
        ~ClientLogic()
        {
            this.gameClient = null;
        }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        public Dictionary<string, string> Clients { get; set; }

        /// <summary>
        /// Gets or sets connect event.
        /// </summary>
        public EventHandler Connect { get; set; }

        /// <summary>
        /// Gets or sets death event.
        /// </summary>
        public EventHandler Death { get; set; }

        /// <summary>
        /// Gets or sets render event.
        /// </summary>
        public EventHandler Render { get; set; }

        /// <summary>
        /// Gets or sets start event.
        /// </summary>
        public EventHandler Start { get; set; }

        /// <summary>
        /// Gets or sets reload event.
        /// </summary>
        public EventHandler Reload { get; set; }

        /// <summary>
        /// Gets or sets pick event.
        /// </summary>
        public EventHandler Pick { get; set; }

        /// <summary>
        /// Gets or sets sound event.
        /// </summary>
        public EventHandler SoundActive { get; set; }

        /// <summary>
        /// Gets or sets shoot event.
        /// </summary>
        public EventHandler Shoot { get; set; }

        /// <summary>
        /// Removes the visual objects.
        /// </summary>
        public void RemoveVisuals()
        {
            this.model.RemoveVisuals();
        }

        /// <summary>
        /// Checks the client list.
        /// </summary>
        public void CheckState()
        {
            Task.Factory.StartNew(() => this.gameClient.Send(Operation.GetClientList, "GET"));
        }

        /// <summary>
        /// Appends the player.
        /// </summary>
        public void AppendPlayer()
        {
            IPlayer p = this.model.MyPlayer;
            var bounds = p.RealPrimitive.Bounds;
            this.model.ConstructItem(p, p.GetType());
            string parameters = $"Player;{p.Id};{p.Position.X};{p.Position.Y};{bounds.Width};{bounds.Height};{p.Rotation}";
            this.gameClient.Send(Operation.SendObject, parameters);
            p.Die += this.OnPlayerDie;
        }

        /// <summary>
        /// Pickup weapon.
        /// </summary>
        /// <param name="sender">null.</param>
        /// <param name="e">empty.</param>
        public void OnPickup(object sender, EventArgs e)
        {
            bool got = false;
            var crates = this.model.Crates.Where(x => (x as ICrate).InPickRange(this.model.MyPlayer.RealPrimitive.Bounds));
            if (crates.Count() > 0)
            {
                var weapon = (crates.First() as ICrate).PickWeapon();
                this.gameClient.Send(Operation.Pickup, $"{this.model.MyPlayer.Id};{weapon.Id}");
                this.Render?.Invoke(this, null);
                got = true;
            }

            if (!got)
            {
                var freeWeapons = this.model.Weapons.Where(x => !(x as IWeapon).Owned && new Rect(x.Position.X - 100, x.Position.Y - 100, 300, 300).IntersectsWith(this.model.MyPlayer.RealPrimitive.Bounds));
                if (freeWeapons.Count() > 0)
                {
                    var weapon = freeWeapons.First() as IWeapon;
                    this.gameClient.Send(Operation.Pickup, $"{this.model.MyPlayer.Id};{weapon.Id}");
                    this.Render?.Invoke(this, null);
                }
            }
        }

        /// <summary>
        /// Reload weapon.
        /// </summary>
        /// <param name="sender">null.</param>
        /// <param name="e">empty.</param>
        public void OnReload(object sender, EventArgs e)
        {
            if (this.model.MyPlayer.CurrentWeapon != null)
            {
                this.model.MyPlayer.Reload();
                this.SoundActive?.Invoke(new double[] { 3, 0.5, 0.5 }, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Move player.
        /// </summary>
        /// <param name="sender">player.</param>
        /// <param name="e">empty.</param>
        public void OnMove(object sender, EventArgs e)
        {
            double[] movement = (double[])sender;
            if (movement[0] != 0 || movement[1] != 0 || movement[2] != this.model.MyPlayer.Rotation)
            {
                double angle = this.model.MyPlayer.Rotation;
                if (movement[2] != 0)
                {
                    angle = movement[2];
                }

                this.model.MyPlayer.Move(movement[0], movement[1], movement[3], movement[2]);
                var bounds = this.model.MyPlayer.RealPrimitive.Bounds;
                var closeItems = this.model.Colliders.Where(x => x != this.model.MyPlayer && x.GetType() != typeof(Bullet) && bounds.IntersectsWith(x.RealPrimitive.Bounds));
                var colliders = closeItems.Where(x => x.CollidesWith(this.model.MyPlayer.RealPrimitive));
                if (colliders.Count() > 0)
                {
                    this.model.MyPlayer.Move(-movement[0], -movement[1], movement[3], movement[2]);
                }

                string parameters = $"Player;{this.model.MyPlayer.Id};{this.model.MyPlayer.Position.X};{this.model.MyPlayer.Position.Y};{bounds.Width};{bounds.Height};{this.model.MyPlayer.Rotation};{(double)this.Milis}";
                this.Render?.Invoke(this, null);
                this.gameClient.Send(Operation.Move, parameters);
                this.SoundActive?.Invoke(new double[] { 1, 0.5, 0.5 }, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Shoot weapon.
        /// </summary>
        /// <param name="sender">null.</param>
        /// <param name="e">empty.</param>
        public void OnShoot(object sender, EventArgs e)
        {
            var bullets = this.model.MyPlayer.Shoot();
            if (bullets != null)
            {
                foreach (var bullet in bullets)
                {
                    this.model.ConstructItem(bullet, bullet.GetType());
                    var bounds = bullet.RealPrimitive.Bounds;
                    double[] direction = bullet.Direction;
                    string parameters = $"Bullet;{bullet.Id};{bullet.Position.X};{bullet.Position.Y};{bounds.Width};{bounds.Height};{bullet.Rotation};" +
                    $"{bullet.BulletDamage};{direction[0]};{direction[1]};{bullet.Speed};{bullet.Weight};{this.Milis};{this.model.MyPlayer.Id}";
                    this.gameClient.Send(Operation.Shoot, parameters);
                }

                this.SoundActive?.Invoke(new double[] { 2, 0.5, 0.7 }, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Bullet update method.
        /// </summary>
        /// <param name="deltaTime">deltaTime.</param>
        public void FlyBullets(double deltaTime)
        {
            this.model.LoadCache();
            var bullets = this.model.Bullets;
            for (int i = 0; i < bullets.Count(); i++)
            {
                var ibullet = (IBullet)bullets.ElementAt(i);
                if (ibullet.RealSpeed < 80)
                {
                    this.model.DestructItem(ibullet.Id);
                    this.Render?.Invoke(this, null);
                    continue;
                }

                var bounds = ibullet.RealPrimitive.Bounds;
                ibullet.Fly(deltaTime);
                var items = this.model.Intersection(bounds);
                for (int y = 0; y < items.Count(); y++)
                {
                    if (items.ElementAt(y).CollidesWith(bullets.ElementAt(i).RealPrimitive))
                    {
                        if (items.ElementAt(y).GetType() == typeof(Player))
                        {
                            this.gameClient.Send(Operation.Hit, $"{items.ElementAt(y).Id};{ibullet.Id};{ibullet.BulletDamage};{this.Milis}");
                            this.model.DestructItem(ibullet.Id);
                            break;
                        }
                        else
                        {
                            this.gameClient.Send(Operation.Remove, $"{ibullet.Id}");
                            this.model.DestructItem(ibullet.Id);
                            break;
                        }
                    }
                }
            }

            this.Render?.Invoke(this, null);
        }

        /// <summary>
        /// Ready the client.
        /// </summary>
        /// <param name="name">Username.</param>
        public void Ready(string name)
        {
            Task.Factory.StartNew(() => this.gameClient.Send(Operation.CheckServerAvaliable, name));
        }

        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">the request.</param>
        protected override void HandleRequests(Request request)
        {
            switch (request.Operation)
            {
                case Operation.Connect:
                    this.HandleConnect(request);
                    break;
                case Operation.StartGame:
                    this.HandleStartGame(request);
                    break;
                case Operation.CheckServerAvaliable:
                    this.HandleReady(request);
                    break;
                case Operation.SendObject:
                    this.HandleSendObject(request);
                    break;
                case Operation.Remove:
                    this.HandleRemove(request);
                    break;
                case Operation.Die:
                    this.HandleDie(request);
                    break;
                case Operation.Hit:
                    this.HandleHit(request);
                    break;
                case Operation.GetClientList:
                    this.HandleSendMessage(request);
                    break;
                case Operation.Shoot:
                    this.HandleShoot(request);
                    break;
                case Operation.Move:
                    this.HandleMove(request);
                    break;
                case Operation.Pickup:
                    this.HandlePickup(request);
                    break;
                case Operation.Drop:
                    this.HandleDrop(request);
                    break;
                default:
                    throw new Exception("Not implemented Exception");
            }
        }

        /// <summary>
        /// Connect handler.
        /// </summary>
        /// <param name="request">The request.</param>
        protected override void HandleConnect(Request request)
        {
        }

        /// <summary>
        /// Die handler.
        /// </summary>
        /// <param name="request">The request.</param>
        protected override void HandleDie(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid playerId = Guid.Parse(zones[0]);
            var players = this.model.Players.Where(x => x.Id == playerId);
            if (players.Count() > 0)
            {
                var name = (players.First() as IPlayer).Name;
                this.model.DestructItem(playerId);
                this.Death?.Invoke(new Tuple<Guid, string>(playerId, name), EventArgs.Empty);
            }
        }

        /// <summary>
        /// Drop handler.
        /// </summary>
        /// <param name="request">The request.</param>
        protected override void HandleDrop(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid playerId = Guid.Parse(zones[0]);
            Guid weaponId = Guid.Parse(zones[1]);
            double x = double.Parse(zones[2]);
            double y = double.Parse(zones[3]);
            if (playerId != this.model.MyPlayer.Id)
            {
                var weapons = this.model.Weapons.Where(z => z.Id == weaponId);
                if (weapons.Count() > 0)
                {
                    var weapon = (IWeapon)weapons.First();
                    weapon.SetPosition(x, y, 0);
                }
            }
        }

        /// <summary>
        /// Game start handler.
        /// </summary>
        /// <param name="request">The request.</param>
        protected override void HandleStartGame(Request request)
        {
            if (request.Reply == "1" && this.activeGame == true)
            {
                this.Start?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Ez a metódus az eltalálást kezeli.
        /// </summary>
        /// <param name="request"> a beérkezett kérés.</param>
        protected override void HandleHit(Request request)
        {
            if (request.Reply == "1")
            {
                string[] zones = request.Parameters.Split(';');
                Guid playerId = Guid.Parse(zones[0]);
                Guid bulletId = Guid.Parse(zones[1]);
                double damage = double.Parse(zones[2]);
                var bullets = this.model.Bullets.Where(x => x.Id == bulletId);
                var players = this.model.Players.Where(x => x.Id == playerId);
                lock (ModelLock)
                {
                    if (players.Count() > 0)
                    {
                        IPlayer player = (IPlayer)players.First();
                        if (bullets.Count() > 0)
                        {
                            IBullet bullet = (IBullet)bullets.First();
                            bullet.DoDamage(player);
                            this.model.DestructItem(bullet.Id);
                        }
                        else
                        {
                            player.Damaged(damage);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Movement handler.
        /// </summary>
        /// <param name="request">The request.</param>
        protected override void HandleMove(Request request)
        {
            if (request.Reply == "1")
            {
                string[] zones = request.Parameters.Split(';');
                Guid id = Guid.Parse(zones[1]);
                if (id != this.model.MyPlayer.Id)
                {
                    Vector position = new Vector(double.Parse(zones[2]), double.Parse(zones[3]));
                    double rotation = double.Parse(zones[6]);
                    var players = this.model.Players.Where(x => x.Id == id);
                    if (players.Count() > 0)
                    {
                        var player = players.First();
                        player.Position = position;
                        player.Rotation = rotation;
                        this.model.ConstructItem(player, player.GetType());
                        this.SoundActive?.Invoke(new double[] { 4, 0, 0.3 }, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Pickup handler.
        /// </summary>
        /// <param name="request">The request.</param>
        protected override void HandlePickup(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid playerId = Guid.Parse(zones[0]);
            Guid weaponId = Guid.Parse(zones[1]);
            if (request.Reply == "1")
            {
                var player = (IPlayer)this.model.Players.Where(x => x.Id == playerId).First();
                var weapon = (IWeapon)this.model.Weapons.Where(x => x.Id == weaponId).First();
                if (player.CurrentWeapon != null)
                {
                    player.CurrentWeapon.Owned = false;
                    this.gameClient.Send(Operation.Drop, $"{player.Id};{player.CurrentWeapon.Id};{weapon.Position.X};{weapon.Position.Y}");
                }

                player.ChangeWeapon(weapon);
            }
        }

        /// <summary>
        /// Ready handler.
        /// </summary>
        /// <param name="request">The request.</param>
        protected override void HandleReady(Request request)
        {
            if (request.Reply == "1" && this.activeGame == false)
            {
                this.activeGame = true;
                this.Connect?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Remove handler.
        /// </summary>
        /// <param name="request">The request.</param>
        protected override void HandleRemove(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid id = Guid.Parse(zones[0]);
            this.model.DestructItem(id);
        }

        /// <summary>
        /// The Message handler.
        /// </summary>
        /// <param name="request">The request.</param>
        protected override void HandleSendMessage(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            if (!this.Clients.ContainsKey(zones[0]))
            {
                this.Clients.Add(zones[0], zones[1]);
            }
        }

        /// <summary>
        /// The object sending handler.
        /// </summary>
        /// <param name="request">The request.</param>
        protected override void HandleSendObject(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            lock (ModelLock)
            {
                Guid id = Guid.Parse(zones[1]);
                var items = this.model.AllItems.Where(x => x.Id == id);
                double[] position = new double[] { double.Parse(zones[2]), double.Parse(zones[3]) };
                if (items.Count() == 0)
                {
                    double rotation = double.Parse(zones[6]);
                    IPlayer player = new Player(Player.PlayerGeometry, id, position, rotation, true, 100, zones[7]);
                    player.Die += this.OnPlayerDie;
                    this.model.ConstructItem(player, player.GetType());
                }
                else
                {
                    this.model.MyPlayer.Position = new Vector(position[0], position[1]);
                }
            }
        }

        /// <summary>
        /// The shoot handler.
        /// </summary>
        /// <param name="request">the request.</param>
        protected override void HandleShoot(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid playerId = Guid.Parse(zones[13]);
            if (this.model.MyPlayer.Id != playerId)
            {
                Guid id = Guid.Parse(zones[1]);
                double speed = double.Parse(zones[10]);
                double weight = double.Parse(zones[11]);
                double[] direction = new double[] { double.Parse(zones[8]), double.Parse(zones[9]) };
                double[] position = new double[] { double.Parse(zones[2]), double.Parse(zones[3]) };
                double rotation = double.Parse(zones[6]);
                double bulletDamage = double.Parse(zones[7]);

                lock (ModelLock)
                {
                    Tuple<Type, Guid, double[], double[], double[]> tuple = new Tuple<Type, Guid, double[], double[], double[]>(typeof(Bullet), id, position, direction, new double[] { speed, weight, bulletDamage, rotation });
                    this.model.CacheItem(tuple);
                }

                this.SoundActive?.Invoke(new double[] { 5, 0, 0.6 }, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Player death event.
        /// </summary>
        /// <param name="sender">null.</param>
        /// <param name="e">empty.</param>
        private void OnPlayerDie(object sender, EventArgs e)
        {
            IPlayer player = sender as IPlayer;
            this.gameClient.Send(Operation.Die, $"{player.Id}");
        }

        /// <summary>
        /// Receive method.
        /// </summary>
        /// <param name="sender">null.</param>
        /// <param name="e">Args.</param>
        private void OnReceive(object sender, IReceivedEventArgs e)
        {
            try
            {
                Task.Factory.StartNew(() => this.HandleRequests(e.ReceivedRequest));
            }
            catch (Exception)
            {
            }
        }
    }
}
