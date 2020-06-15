// <copyright file="StaticItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameModel.Item
{
    using System;
    using System.Windows.Media;
    using obServer.GameModel.Interfaces;

    /// <summary>
    /// Static items class.
    /// </summary>
    public sealed class StaticItem : BaseItem, IStaticItem
    {
        private string type;
        private double[] dimensions;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticItem"/> class.
        /// </summary>
        /// <param name="geometry">Item geometry.</param>
        /// <param name="id">Item ID.</param>
        /// <param name="position">Item position.</param>
        /// <param name="rotation">Item rotation.</param>
        /// <param name="dimensions">Item  size.</param>
        /// <param name="impact">Collision detection.</param>
        /// <param name="type">Type of item.</param>
        public StaticItem(Geometry geometry, Guid id, double[] position, double rotation, double[] dimensions, bool impact, string type)
            : base(geometry, id, position, rotation, impact)
        {
            this.Dimensions = dimensions;
            this.Type = type;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// Gets or sets the items size.
        /// </summary>
        public double[] Dimensions
        {
            get { return this.dimensions; }
            set { this.dimensions = value; }
        }
    }
}
