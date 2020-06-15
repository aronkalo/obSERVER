// <copyright file="ICrate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Model.Interfaces
{
    using System.Windows;
    using obServer.GameModel.Interfaces;

    /// <summary>
    /// The weapon interface.
    /// </summary>
    public interface ICrate : IBaseItem
    {
        /// <summary>
        /// Gets or sets the contained weapon.
        /// </summary>
        IWeapon ContainedWeapon { get; set; }

        /// <summary>
        /// The pick meth.
        /// </summary>
        /// <param name="primitive">The obj.</param>
        /// <returns>True false.</returns>
        bool InPickRange(Rect primitive);

        /// <summary>
        /// The weapon pick method.
        /// </summary>
        /// <returns>Weapon.</returns>
        IWeapon PickWeapon();
    }
}
