// <copyright file="RepoOBServerModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.GameModel
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using obServer.GameModel;
    using obServer.GameModel.Interfaces;

    /// <summary>
    /// Servermodel repo class.
    /// </summary>
    public class RepoOBServerModel : IRepoOBServerModel
    {
        /// <summary>
        /// Model field.
        /// </summary>
        private IOBServerModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoOBServerModel"/> class.
        /// </summary>
        public RepoOBServerModel()
        {
            this.model = new OBServerModel();
        }

        /// <summary>
        /// Gets bullets.
        /// </summary>
        public IEnumerable<IBaseItem> Bullets
        {
            get { return this.model.Bullets; }
        }

        /// <summary>
        /// Gets palyers.
        /// </summary>
        public IEnumerable<IBaseItem> Players
        {
            get { return this.model.Players; }
        }

        /// <summary>
        /// Gets colliders.
        /// </summary>
        public IEnumerable<IBaseItem> Colliders
        {
            get { return this.model.Colliders; }
        }

        /// <summary>
        /// Gets statistics.
        /// </summary>
        public IEnumerable<IBaseItem> Statics
        {
            get { return this.model.Statics; }
        }

        /// <summary>
        /// Gets crates.
        /// </summary>
        public IEnumerable<IBaseItem> Crates
        {
            get { return this.model.Crates; }
        }

        /// <summary>
        /// Gets or sets myplayer.
        /// </summary>
        public IPlayer MyPlayer
        {
            get { return this.model.MyPlayer; }
            set { this.model.MyPlayer = value; }
        }

        /// <summary>
        /// Gets weapons.
        /// </summary>
        public IEnumerable<IBaseItem> Weapons => this.model.Weapons;

        /// <summary>
        /// Gets all items.
        /// </summary>
        public IEnumerable<IBaseItem> AllItems => this.model.AllItems;

        /// <summary>
        /// Item construction method.
        /// </summary>
        /// <param name="item">Item type.</param>
        /// <param name="type">Type type.</param>
        public void ConstructItem(IBaseItem item, Type type)
        {
            this.model.ConstructItem(item, type);
        }

        /// <summary>
        /// Item destruction.
        /// </summary>
        /// <param name="id">Id of guid.</param>
        public void DestructItem(Guid id)
        {
            this.model.DestructItem(id);
        }

        /// <summary>
        /// Username setter method.
        /// </summary>
        /// <param name="name">Name string.</param>
        public void SetUsername(string name)
        {
            this.model.SetUsername(name);
        }

        /// <summary>
        /// Visual removal method.
        /// </summary>
        public void RemoveVisuals()
        {
            this.model.RemoveVisuals();
        }

        /// <summary>
        /// Item loader method.
        /// </summary>
        /// <param name="fileName">Filename string.</param>
        public void LoadItems(string fileName)
        {
            this.model.LoadItems(fileName);
        }

        /// <summary>
        /// Intersection method.
        /// </summary>
        /// <param name="bounds">mounds of rectangle.</param>
        /// <returns>Baseitem list.</returns>
        public IEnumerable<IBaseItem> Intersection(Rect bounds)
        {
            return this.model.Intersection(bounds);
        }

        /// <summary>
        /// Cachke loader method.
        /// </summary>
        public void LoadCache()
        {
            this.model.LoadCache();
        }

        /// <summary>
        /// Item cacher method.
        /// </summary>
        /// <param name="item">Tuple.</param>
        public void CacheItem(Tuple<Type, Guid, double[], double[], double[]> item)
        {
            this.model.CacheItem(item);
        }
    }
}
