// <copyright file="RepoServerSideModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.Model
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using obServer.GameModel;
    using obServer.GameModel.ServerSide;
    using obServer.Model.Interfaces;

    /// <summary>
    /// Serverside model repo class.
    /// </summary>
    public class RepoServerSideModel : IRepoServerSideModel
    {
        /// <summary>
        /// Serverdise model field.
        /// </summary>
        private IServerSideModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoServerSideModel"/> class.
        /// </summary>
        public RepoServerSideModel()
        {
            this.model = new ServerSideModel();
        }

        /// <summary>
        /// Gets All of server items.
        /// </summary>
        public IEnumerable<ServerItem> AllItems
        {
            get { return this.model.AllItems; }
        }

        /// <summary>
        /// Gets all players.
        /// </summary>
        public IEnumerable<ServerItem> Player
        {
            get { return this.model.Players; }
        }

        /// <summary>
        /// Gets all bullets.
        /// </summary>
        public IEnumerable<ServerItem> Bullets
        {
            get { return this.model.Bullets; }
        }

        /// <summary>
        /// Gets all weapons.
        /// </summary>
        public IEnumerable<ServerItem> Weapons
        {
            get { return this.model.Weapons; }
        }

        /// <summary>
        /// Bullet hits method.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <returns>Bullet hit.</returns>
        public ServerItem[] BulletHit(Guid id)
        {
            return this.model.BulletHit(id);
        }

        /// <summary>
        /// Collision detection.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <returns>Collision.</returns>
        public ServerItem[] Collision(Guid id)
        {
            return this.model.Collision(id);
        }

        /// <summary>
        /// Bullet construction.
        /// </summary>
        /// <param name="id">Bullet id.</param>
        /// <param name="type">Bullet type.</param>
        /// <param name="bounds">Bullet bounds.</param>
        /// <param name="impact">Bullet impact.</param>
        /// <param name="direction">Bullet direction.</param>
        /// <param name="weight">Bullet weight.</param>
        /// <param name="speed">Bullet speed.</param>
        public void ConstructBullet(Guid id, string type, Rect bounds, bool impact, Vector direction, double weight, double speed)
        {
            this.model.ConstructBullet(id, type, bounds, impact, direction, weight, speed);
        }

        /// <summary>
        /// Item construction.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <param name="type">Item type.</param>
        /// <param name="bounds">Item bounds.</param>
        /// <param name="impact">Item impact.</param>
        public void ConstructItem(Guid id, string type, Rect bounds, bool impact)
        {
            this.ConstructItem(id, type, bounds, impact);
        }

        /// <summary>
        /// Item destruction.
        /// </summary>
        /// <param name="id">Item id.</param>
        public void DestructItem(Guid id)
        {
            this.DestructItem(id);
        }

        /// <summary>
        /// Map loading method.
        /// </summary>
        public void LoadMap()
        {
            this.model.LoadMap();
        }
    }
}
