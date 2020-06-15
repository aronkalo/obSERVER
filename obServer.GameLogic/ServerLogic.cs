// <copyright file="ServerLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using obServer.GameModel;
    using obServer.Logic.Interfaces;
    using obServer.Network.Structs;
    using obServer.Repository.Network;

    /// <summary>
    /// ServerLogic class.
    /// </summary>
    public class ServerLogic : BaseLogic, IServerLogic
    {
        private Queue<Request> objectCache;
        private List<ClientData> clients;
        private bool inGame;
        private IRepoGameServer gameServer;
        private ServerSideModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerLogic"/> class.
        /// </summary>
        public ServerLogic()
        {
            this.gameServer = new RepoGameServer();
            this.model = new ServerSideModel();
            this.model.LoadMap();
            this.objectCache = new Queue<Request>();
            this.inGame = false;
            this.gameServer.StartListening();
            this.clients = new List<ClientData>();
            this.Serve();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ServerLogic"/> class.
        /// </summary>
        ~ServerLogic()
        {
            this.gameServer = null;
        }

        /// <summary>
        /// Serve method.
        /// </summary>
        public void Serve()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Request? oldest = this.gameServer.GetRequest();
                    if (oldest != null)
                    {
                        this.HandleRequests(oldest.Value);
                    }
                }
            });
        }

        /// <summary>
        /// Game starting method.
        /// </summary>
        public void SendGameStart()
        {
            while (this.objectCache.Count > 0)
            {
                this.gameServer.ReplyHandler(this.objectCache.Dequeue());
            }

            Thread.Sleep(500);
            this.gameServer.ReplyHandler(new Request() { Broadcast = true, Operation = Operation.StartGame, Reply = "1" });
            this.inGame = true;
        }

        /// <summary>
        /// Update method.
        /// </summary>
        /// <param name="deltaTime">Time spend.</param>
        public void Update(double deltaTime)
        {
            var bullets = this.model.Bullets;
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
                bullet.Bounds = new Rect(
                    x + (direction.X * movement),
                    y + (direction.Y * movement),
                    width: bullet.Bounds.Width,
                    bullet.Bounds.Height);
                var items = this.model.Collision(bullet.Id);
                if (items.Count() > 0)
                {
                    this.model.DestructItem(bullet.Id);
                    var players = items.Where(z => z.Typography == "Player");
                    if (players.Count() > 0)
                    {
                        this.gameServer.ReplyHandler(new Request() { Broadcast = true, Operation = Operation.Hit, Parameters = $"{players.First().Id};{bullet.Id}" });
                    }

                    this.gameServer.ReplyHandler(new Request() { Broadcast = true, Operation = Operation.Remove, Parameters = $"{bullet.Id}" });
                }
            }
        }

        /// <summary>
        /// Request handling.
        /// </summary>
        /// <param name="request">Request type request.</param>
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
            }
        }

        /// <summary>
        /// HandleDrop.
        /// </summary>
        /// <param name="request">Request type request.</param>
        protected override void HandleDrop(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid playerId = Guid.Parse(zones[0]);
            Guid weaponId = Guid.Parse(zones[1]);

            var weapons = this.model.Weapons.Where(x => x.Id == weaponId);

            if (weapons.Count() == 1)
            {
                var weapon = weapons.First();
                if (weapon.Owned)
                {
                    var owner = this.model.AllItems.Where(x => x.Id == weapon.OwnerId).First();
                    if (owner.Id == playerId)
                    {
                        weapon.Owned = false;
                        weapon.OwnerId = Guid.Empty;
                        request.Reply = "1";
                        request.Broadcast = true;
                    }
                    else
                    {
                        request.Reply = "0";
                    }
                }
                else
                {
                    request.Reply = "0";
                }
            }
            else
            {
                request.Reply = "0";
            }

            this.gameServer.ReplyHandler(request);
        }

        /// <summary>
        /// Connection handle method.
        /// </summary>
        /// <param name="request">Request.</param>
        protected override void HandleConnect(Request request)
        {
            request.Reply = "1";
            request.Broadcast = true;
            this.gameServer.ReplyHandler(request);
        }

        /// <summary>
        /// Die handler.
        /// </summary>
        /// <param name="request">Request.</param>
        protected override void HandleDie(Request request)
        {
            Guid id = Guid.Parse(request.Parameters.Split(';')[0]);
            var items = this.model.AllItems.Where(x => x.Id == id);
            if (items.Count() > 0)
            {
                this.model.DestructItem(id);
                request.Reply = "1";
                request.Broadcast = true;
                this.gameServer.ReplyHandler(request);
            }
        }

        /// <summary>
        /// Game starting.
        /// </summary>
        /// <param name="request">Request.</param>
        protected override void HandleStartGame(Request request)
        {
            request.Broadcast = true;
            request.Reply = "1";
            this.gameServer.ReplyHandler(request);
        }

        /// <summary>
        /// Hit handling.
        /// </summary>
        /// <param name="request">Request.</param>
        protected override void HandleHit(Request request)
        {
            Guid bulletid = Guid.Parse(request.Parameters.Split(';')[1]);
            var bullet = this.model.AllItems.Where(x => x.Id == bulletid);
            if (bullet.Count() > 0)
            {
                this.model.DestructItem(bulletid);
                request.Broadcast = true;
                request.Reply = "1";
                this.gameServer.ReplyHandler(request);
            }
        }

        /// <summary>
        /// Move handling.
        /// </summary>
        /// <param name="request">Request.</param>
        protected override void HandleMove(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid id = Guid.Parse(zones[1]);
            this.model.ConstructItem(id, zones[0], new Rect(double.Parse(zones[2]), double.Parse(zones[3]), double.Parse(zones[4]), double.Parse(zones[5])), true);
            request.Reply = "1";
            request.Broadcast = true;
            this.gameServer.ReplyHandler(request);
        }

        /// <summary>
        /// Pickup handle.
        /// </summary>
        /// <param name="request">Request.</param>
        protected override void HandlePickup(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid playerId = Guid.Parse(zones[0]);
            Guid weaponId = Guid.Parse(zones[1]);

            var weapons = this.model.Weapons.Where(x => x.Id == weaponId);

            if (weapons.Count() == 1)
            {
                var weapon = weapons.First();
                if (weapon.Owned)
                {
                    var owner = this.model.AllItems.Where(x => x.Id == weapon.OwnerId).First();
                    if (owner.Typography == "crate")
                    {
                        weapon.Owned = true;
                        weapon.OwnerId = playerId;
                        request.Reply = "1";
                        request.Broadcast = true;
                    }
                    else
                    {
                        request.Reply = "0";
                    }
                }
                else
                {
                    weapon.Owned = true;
                    weapon.OwnerId = playerId;
                    request.Reply = "1";
                    request.Broadcast = true;
                }
            }
            else
            {
                request.Reply = "0";
            }

            this.gameServer.ReplyHandler(request);
        }

        /// <summary>
        /// Ready handle.
        /// </summary>
        /// <param name="request">Request.</param>
        protected override void HandleReady(Request request)
        {
            string address = request.Sender.Address.ToString();
            if (this.clients.Where(x => x.IPAddress == address).Count() != 1)
            {
                this.clients.Add(new ClientData() { IPAddress = address, Username = request.Parameters.Split(';')[0] });
            }

            this.gameServer.ReadyClient(address);
            request.Reply = "1";
            request.Broadcast = true;
            this.gameServer.ReplyHandler(request);
        }

        /// <summary>
        /// Remove handler.
        /// </summary>
        /// <param name="request">Request.</param>
        protected override void HandleRemove(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid id = Guid.Parse(zones[0]);
            var items = this.model.AllItems.Where(x => x.Id == id);
            if (items.Count() > 0)
            {
                this.model.DestructItem(id);
                request.Reply = "1";
                request.Broadcast = true;
                this.gameServer.ReplyHandler(request);
            }
        }

        /// <summary>
        /// SendMessage handling.
        /// </summary>
        /// <param name="request">Request.</param>
        protected override void HandleSendMessage(Request request)
        {
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < this.clients.Count; i++)
                {
                    this.gameServer.ReplyHandler(new Request()
                    {
                        Broadcast = true,
                        Operation = request.Operation,
                        Parameters = $"{this.clients[i].IPAddress};{this.clients[i].Username}",
                    });
                }
            });
        }

        /// <summary>
        /// Send Object handling.
        /// </summary>
        /// <param name="request">Request.</param>
        protected override void HandleSendObject(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            string type = zones[0];
            Guid id = Guid.Parse(zones[1]);
            this.model.ConstructItem(id, type, new Rect(double.Parse(zones[2]), double.Parse(zones[3]), double.Parse(zones[4]), double.Parse(zones[5])), true);
            request.Reply = "1";
            request.Broadcast = true;
            var client = this.clients.Where(x => x.IPAddress == request.Sender.Address.ToString()).First();
            client.Identifier = id;
            request = this.ReorderObjects(request, client.Username);
            if (this.inGame)
            {
                this.gameServer.ReplyHandler(request);
            }
            else
            {
                this.objectCache.Enqueue(request);
            }
        }

        /// <summary>
        /// Shoot handling.
        /// </summary>
        /// <param name="request">Request.</param>
        protected override void HandleShoot(Request request)
        {
            string[] zones = request.Parameters.Split(';');
            Guid player = Guid.Parse(zones[13]);
            if (this.model.Players.Where(x => x.Id == player).Count() == 1)
            {
                Guid id = Guid.Parse(zones[1]);
                double speed = double.Parse(zones[10]);
                double weight = double.Parse(zones[11]);
                Vector direction = new Vector(double.Parse(zones[8]), double.Parse(zones[9]));
                double[] position = new double[] { double.Parse(zones[2]), double.Parse(zones[3]) };
                double deltaMove = speed * ((this.Milis - double.Parse(zones[12])) * 0.0000001);
                position[0] = position[0] + (deltaMove * direction.X);
                position[1] = position[1] + (deltaMove * direction.Y);
                this.model.ConstructBullet(id, zones[0], new Rect(position[0], position[1], 1, 1), true, direction, weight, speed);
                request.Reply = "1";
                request.Broadcast = true;
            }

            this.gameServer.ReplyHandler(request);
        }

        /// <summary>
        /// Object reorders.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <param name="name">Object name.</param>
        /// <returns><see cref="Request"/>.</returns>
        private Request ReorderObjects(Request request, string name)
        {
            string[] zones = request.Parameters.Split(';');
            var players = this.model.Players;
            double xCenter = 2500;
            double radius = 200;
            var player = players.Where(x => x.Id == Guid.Parse(zones[1])).First();
            double[] pos = this.CalulatePosition(xCenter, radius, players.Count());
            double xPos = pos[0];
            double yPos = pos[1];
            player.Bounds = new Rect(xPos, yPos, double.Parse(zones[2]), double.Parse(zones[3]));
            request.Parameters = $"Player;{zones[1]};{xPos};{yPos};{zones[4]};{zones[5]};{zones[6]};{name}";
            return request;
        }

        private double[] CalulatePosition(double center, double radius, int count)
        {
            switch (count)
            {
                case 1:
                    return new double[] { center - radius, center };
                case 2:
                    return new double[] { center + radius, center };
                case 3:
                    return new double[] { center, center - radius };
                case 4:
                    return new double[] { center, center + radius };
                default:
                    return new double[] { -100, -100 };
            }
        }
    }
}
