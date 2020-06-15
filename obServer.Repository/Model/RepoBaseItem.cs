// <copyright file="RepoBaseItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.Model
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using obServer.GameModel.Interfaces;
    using obServer.GameModel.Item;

    /// <summary>
    /// Baseitem Repo class.
    /// </summary>
    public class RepoBaseItem : IRepoBaseItem
    {
        /// <summary>
        /// Item field.
        /// </summary>
        private IBaseItem item;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoBaseItem"/> class.
        /// </summary>
        /// <param name="geometry">Geometry.</param>
        /// <param name="id">Item id.</param>
        /// <param name="position">Item position.</param>
        /// <param name="rotation">Item rotation.</param>
        /// <param name="impact">Item impact.</param>
        public RepoBaseItem(Geometry geometry, Guid id, double[] position, double rotation, bool impact)
        {
            this.item = new BaseItem(geometry, id, position, rotation, impact);
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id
        {
            get { return this.item.Id; } set { this.item.Id = value; }
        }

        /// <summary>
        /// Gets or sets item position.
        /// </summary>
        public Vector Position
        {
            get { return this.item.Position; } set { this.item.Position = value; }
        }

        /// <summary>
        /// Gets or sets item rotation.
        /// </summary>
        public double Rotation
        {
            get { return this.item.Rotation; } set { this.item.Rotation = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether item impact.
        /// </summary>
        public bool Impact
        {
            get { return this.item.Impact; } set { this.item.Impact = value; }
        }

        /// <summary>
        /// Gets a real primitive geometry.
        /// </summary>
        public Geometry RealPrimitive
        {
            get { return this.item.RealPrimitive; }
        }

        /// <summary>
        /// Collision detection.
        /// </summary>
        /// <param name="geometry">item geometry.</param>
        /// <returns>True or false.</returns>
        public bool CollidesWith(Geometry geometry)
        {
            return this.item.CollidesWith(geometry);
        }
    }
}