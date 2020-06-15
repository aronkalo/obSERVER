// <copyright file="IRepoCrate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.Model
{
    using System.Windows;
    using obServer.GameModel.Interfaces;

    /// <summary>
    /// Crate repo interface.
    /// </summary>
    public interface IRepoCrate : IRepoBaseItem
    {
        /// <summary>
        /// Gets or sets a contained weapon.
        /// </summary>
        IWeapon ContainedWeapon { get; set; }

        /// <summary>
        /// Pickrange detection.
        /// </summary>
        /// <param name="primitive">Rectangle.</param>
        /// <returns>True or false.</returns>
        bool InPickRange(Rect primitive);

        /// <summary>
        /// Picking a weapon.
        /// </summary>
        /// <returns>Weapon interface.</returns>
        IWeapon PickWeapon();
    }
}
