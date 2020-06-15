// <copyright file="IPlayer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameModel.Interfaces
{
    using System;
    using System.Windows.Media;

    /// <summary>
    /// Player interface.
    /// </summary>
    public interface IPlayer : IBaseItem
    {
        /// <summary>
        /// Gets the players current weapon.
        /// </summary>
        IWeapon CurrentWeapon { get; }

        /// <summary>
        /// Gets or sets the player death.
        /// </summary>
        EventHandler Die { get; set; }

        /// <summary>
        /// Gets the health of player.
        /// </summary>
        double Health { get; }

        /// <summary>
        /// Gets the amount of stored bullets.
        /// </summary>
        int StoredBullets { get; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Changes the players weapon.
        /// </summary>
        /// <param name="newWeapon">New weapons constructor.</param>
        void ChangeWeapon(IWeapon newWeapon);

        /// <summary>
        /// Players weapons movement.
        /// </summary>
        /// <param name="xMove">X axis movement.</param>
        /// <param name="yMove">Y axis movement.</param>
        /// <param name="deltaTime">Time spend while travelling.</param>
        /// <param name="rotation">Weapon rotation.</param>
        void Move(double xMove, double yMove, double deltaTime, double rotation);

        /// <summary>
        /// Gets the shooted bullets.
        /// </summary>
        /// <returns>Shooted bullets.</returns>
        IBullet[] Shoot();

        /// <summary>
        /// Reloading the weapon.
        /// </summary>
        void Reload();

        /// <summary>
        /// Picking up bullets.
        /// </summary>
        /// <param name="bullets">Number of picked bullets.</param>
        void PickBullet(int bullets);

        /// <summary>
        /// Damaging the enemy.
        /// </summary>
        /// <param name="damage">Amount the damage.</param>
        void Damaged(double damage);
    }
}