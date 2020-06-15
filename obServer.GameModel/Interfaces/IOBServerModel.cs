// <copyright file="IOBServerModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameModel.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    /// ObserverModel interface.
    /// </summary>
    public interface IOBServerModel
    {
        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        IPlayer MyPlayer { get; set; }

        /// <summary>
        /// Gets all items.
        /// </summary>
        IEnumerable<IBaseItem> AllItems { get; }

        /// <summary>
        /// Gets bullets.
        /// </summary>
        IEnumerable<IBaseItem> Bullets { get; }

        /// <summary>
        /// Gets players.
        /// </summary>
        IEnumerable<IBaseItem> Players { get; }

        /// <summary>
        /// Gets colliders.
        /// </summary>
        IEnumerable<IBaseItem> Colliders { get; }

        /// <summary>
        /// Gets statics.
        /// </summary>
        IEnumerable<IBaseItem> Statics { get; }

        /// <summary>
        /// Gets weapons.
        /// </summary>
        IEnumerable<IBaseItem> Weapons { get; }

        /// <summary>
        /// Gets crates.
        /// </summary>
        IEnumerable<IBaseItem> Crates { get; }

        /// <summary>
        /// Intersection.
        /// </summary>
        /// <param name="bounds">Bounds.</param>
        /// <returns>List of items.</returns>
        IEnumerable<IBaseItem> Intersection(Rect bounds);

        /// <summary>
        /// cache.
        /// </summary>
        void LoadCache();

        /// <summary>
        /// ties.
        /// </summary>
        /// <param name="item">item.</param>
        void CacheItem(Tuple<Type, Guid, double[], double[], double[]> item);

        /// <summary>
        /// Item construction method.
        /// </summary>
        /// <param name="item">Baseitem item.</param>
        /// <param name="type">Type type.</param>
        void ConstructItem(IBaseItem item, Type type);

        /// <summary>
        /// Destruct an item based on the id.
        /// </summary>
        /// <param name="id">item ID.</param>
        void DestructItem(Guid id);

        /// <summary>
        /// Removing visuals from item.
        /// </summary>
        void RemoveVisuals();

        /// <summary>
        /// Sets the username.
        /// </summary>
        /// <param name="name">The username.</param>
        void SetUsername(string name);

        /// <summary>
        /// Loading items.
        /// </summary>
        /// <param name="fileName">Name of the map file.</param>
        void LoadItems(string fileName);
    }
}
