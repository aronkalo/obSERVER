// <copyright file="IRepoWeapon.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.Model
{
    using obServer.GameModel.Interfaces;

    /// <summary>
    /// Weapon Repo interface.
    /// </summary>
    public interface IRepoWeapon : IRepoBaseItem
    {
        /// <summary>
        /// Gets waepon info.
        /// </summary>
        double[] WeaponInfo { get; }

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
