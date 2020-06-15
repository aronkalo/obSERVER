// <copyright file="IBaseItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameModel.Interfaces
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Interface of the Base items.
    /// </summary>
    public interface IBaseItem
    {
        /// <summary>
        /// Gets or sets the id of item.
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the position of item.
        /// </summary>
        Vector Position { get; set; }

        /// <summary>
        /// Gets or sets the rotation of item.
        /// </summary>
        double Rotation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether impact on item.
        /// </summary>
        bool Impact { get; set; }

        /// <summary>
        /// Gets the real primitives.
        /// </summary>
        Geometry RealPrimitive { get; }

        /// <summary>
        /// Checking the collisions.
        /// </summary>
        /// <param name="geometry">Checked geometry.</param>
        /// <returns>Collide or not.</returns>
        bool CollidesWith(Geometry geometry);
    }
}