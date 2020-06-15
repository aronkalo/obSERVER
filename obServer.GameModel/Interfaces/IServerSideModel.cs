// <copyright file="IServerSideModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Model.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using obServer.GameModel.ServerSide;

    /// <summary>
    /// SErverSideModel interface.
    /// </summary>
    public interface IServerSideModel
    {
        /// <summary>
        /// Gets All ServerItems.
        /// </summary>
        IEnumerable<ServerItem> AllItems { get; }

        /// <summary>
        /// Gets all server players.
        /// </summary>
        IEnumerable<ServerItem> Players { get; }

        /// <summary>
        /// Gets all server bullets.
        /// </summary>
        IEnumerable<ServerItem> Bullets { get; }

        /// <summary>
        /// Gets all server weapons.
        /// </summary>
        IEnumerable<ServerItem> Weapons { get; }

        /// <summary>
        /// Item construction method.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <param name="type">Item type.</param>
        /// <param name="bounds">Item bounds.</param>
        /// <param name="impact">Item impact.</param>
        void ConstructItem(Guid id, string type, Rect bounds, bool impact);

        /// <summary>
        /// Bullet construction method.
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
        /// Item destruction method.
        /// </summary>
        /// <param name="id">Item id.</param>
        void DestructItem(Guid id);

        /// <summary>
        /// Bullet hit method.
        /// </summary>
        /// <param name="id">Bullet id.</param>
        /// <returns>Item.</returns>
        ServerItem[] BulletHit(Guid id);

        /// <summary>
        /// Collision method.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Item.</returns>
        ServerItem[] Collision(Guid id);

        /// <summary>
        /// Map loader method.
        /// </summary>
        void LoadMap();
    }
}
