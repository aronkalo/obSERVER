// <copyright file="IWeapon.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameModel.Interfaces
{
    using System;
    using System.Windows.Media;

    /// <summary>
    /// Weapon interface.
    /// </summary>
    public interface IWeapon : IBaseItem
    {
        /// <summary>
        /// Gets waepon info.
        /// </summary>
        double[] WeaponInfo { get; }

        /// <summary>
        /// Gets or sets a value indicating whether id owned.
        /// </summary>
        bool Owned { get; set; }

        /// <summary>
        /// Doing a shoot.
        /// </summary>
        /// <returns>Shooted bullets.</returns>
        IBullet[] DoShoot();

        /// <summary>
        /// Moving the weapon.
        /// </summary>
        /// <param name="xMovement">X axis movement.</param>
        /// <param name="yMovement">Y axis movement.</param>
        /// <param name="rotation">Weapon rotation.</param>
        void SetPosition(double xMovement, double yMovement, double rotation);

        /// <summary>
        /// Reloading the weapon.
        /// </summary>
        /// <param name="storedBullets">Number of stored bullets.</param>
        void DoReload(int storedBullets);
    }
}