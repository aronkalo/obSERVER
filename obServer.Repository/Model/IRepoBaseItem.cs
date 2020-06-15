// <copyright file="IRepoBaseItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.Model
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// IBaseitem Interface.
    /// </summary>
    public interface IRepoBaseItem
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets Position vector.
        /// </summary>
        Vector Position { get; set; }

        /// <summary>
        /// Gets or sets Rotation.
        /// </summary>
        double Rotation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Impact.
        /// </summary>
        bool Impact { get; set; }

        /// <summary>
        /// Gets a Geometry.
        /// </summary>
        Geometry RealPrimitive { get; }

        /// <summary>
        /// Collision detection.
        /// </summary>
        /// <param name="geometry">Geometry.</param>
        /// <returns>True or false.</returns>
        bool CollidesWith(Geometry geometry);
    }
}
