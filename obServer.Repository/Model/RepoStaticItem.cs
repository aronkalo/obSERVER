// <copyright file="RepoStaticItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.Model
{
    using System;
    using System.Windows.Media;
    using obServer.GameModel.Interfaces;
    using obServer.GameModel.Item;

    /// <summary>
    /// StaticItem repo class.
    /// </summary>
    public class RepoStaticItem
    {
        private IStaticItem staticItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoStaticItem"/> class.
        /// </summary>
        /// <param name="geometry">Item geometry.</param>
        /// <param name="id">Item id.</param>
        /// <param name="position">Item position.</param>
        /// <param name="rotation">Item rotation.</param>
        /// <param name="dimensions">Item dimensions.</param>
        /// <param name="impact">Item impact.</param>
        /// <param name="type">Item type.</param>
        public RepoStaticItem(Geometry geometry, Guid id, double[] position, double rotation, double[] dimensions, bool impact, string type)
        {
            this.staticItem = new StaticItem(geometry, id, position, rotation, dimensions, impact, type);
        }

        /// <summary>
        /// Collision detection.
        /// </summary>
        /// <param name="geometry">Item geometry.</param>
        /// <returns>True or false.</returns>
        public bool CollidesWith(Geometry geometry)
        {
            return this.staticItem.CollidesWith(geometry);
        }
    }
}
