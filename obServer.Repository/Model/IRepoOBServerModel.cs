// <copyright file="IRepoOBServerModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.GameModel
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using obServer.GameModel.Interfaces;

    /// <summary>
    /// Observermodel Repository interface.
    /// </summary>
    public interface IRepoOBServerModel
    {
        /// <summary>
        /// Gets the bullets.
        /// </summary>
        IEnumerable<IBaseItem> Bullets { get; }

        /// <summary>
        /// Gets the players.
        /// </summary>
        IEnumerable<IBaseItem> Players { get; }

        /// <summary>
        /// Gets the colliders.
        /// </summary>
        IEnumerable<IBaseItem> Colliders { get; }

        /// <summary>
        /// Gets the statics.
        /// </summary>
        IEnumerable<IBaseItem> Statics { get; }

        /// <summary>
        /// Gets or sets my player.
        /// </summary>
        IPlayer MyPlayer { get; set; }

        /// <summary>
        /// Gets the crates.
        /// </summary>
        IEnumerable<IBaseItem> Crates { get; }

        /// <summary>
        /// Gets the weapons.
        /// </summary>
        IEnumerable<IBaseItem> Weapons { get; }

        /// <summary>
        /// Gets all items.
        /// </summary>
        IEnumerable<IBaseItem> AllItems { get; }

        /// <summary>
        /// Intersection method.
        /// </summary>
        /// <param name="bounds">Rectangle type.</param>
        /// <returns>IBaseItem list.</returns>
        IEnumerable<IBaseItem> Intersection(Rect bounds);

        /// <summary>
        /// Item construction.
        /// </summary>
        /// <param name="item">Ibaseitem interface type.</param>
        /// <param name="type">Type type.</param>
        void ConstructItem(IBaseItem item, Type type);

        /// <summary>
        /// Item destruction.
        /// </summary>
        /// <param name="id">Item id.</param>
        void DestructItem(Guid id);

        /// <summary>
        /// Loading items.
        /// </summary>
        /// <param name="fileName">File url.</param>
        void LoadItems(string fileName);

        /// <summary>
        /// Setting username.
        /// </summary>
        /// <param name="name">Name string.</param>
        void SetUsername(string name);

        /// <summary>
        /// Removing visuals.
        /// </summary>
        void RemoveVisuals();

        /// <summary>
        /// cache.
        /// </summary>
        void LoadCache();

        /// <summary>
        /// ties.
        /// </summary>
        /// <param name="item">item.</param>
        void CacheItem(Tuple<Type, Guid, double[], double[], double[]> item);
    }
}
