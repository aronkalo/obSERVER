// <copyright file="IRepoServerSideModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.Model
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using obServer.GameModel.ServerSide;

    /// <summary>
    /// ServerSideModel repo interface.
    /// </summary>
    public interface IRepoServerSideModel
    {
        /// <summary>
        /// Gets All of the items.
        /// </summary>
        IEnumerable<ServerItem> AllItems { get; }

        /// <summary>
        /// Gets all player.
        /// </summary>
        IEnumerable<ServerItem> Player { get; }

        /// <summary>
        /// Gets all bullets.
        /// </summary>
        IEnumerable<ServerItem> Bullets { get; }

        /// <summary>
        /// Gets all weapons.
        /// </summary>
        IEnumerable<ServerItem> Weapons { get; }

        /// <summary>
        /// Constructing an item.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <param name="type">Item type.</param>
        /// <param name="bounds">Item bounds.</param>
        /// <param name="impact">Item impact.</param>
        void ConstructItem(Guid id, string type, Rect bounds, bool impact);

        /// <summary>
        /// Constructing a bullet.
        /// </summary>
        /// <param name="id">Bullet id.</param>
        /// <param name="type">Bullet type.</param>
        /// <param name="bounds">Bullet bounds.</param>
        /// <param name="impact">Bullet impact.</param>
        /// <param name="direction">Bullet direction.</param>
        /// <param name="weight">Bullet weight.</param>
        /// <param name="speed">Bullet speed.</param>
        void ConstructBullet(Guid id, string type, Rect bounds, bool impact, Vector direction, double weight, double speed);

        /// <summary>
        /// Destructing an item.
        /// </summary>
        /// <param name="id">Item id.</param>
        void DestructItem(Guid id);

        /// <summary>
        /// Bullet hits.
        /// </summary>
        /// <param name="id">Hit id.</param>
        /// <returns>Items.</returns>
        ServerItem[] BulletHit(Guid id);

        /// <summary>
        /// Collisions.
        /// </summary>
        /// <param name="id">Collision id.</param>
        /// <returns>Item.</returns>
        ServerItem[] Collision(Guid id);

        /// <summary>
        /// Map loading method.
        /// </summary>
        void LoadMap();
    }
}
