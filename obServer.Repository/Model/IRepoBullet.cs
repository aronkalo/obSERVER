// <copyright file="IRepoBullet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.Model
{
    using obServer.GameModel.Interfaces;

    /// <summary>
    /// Bullet Repo interface.
    /// </summary>
    public interface IRepoBullet : IRepoBaseItem
    {
        /// <summary>
        /// Gets the bullet damage.
        /// </summary>
        double BulletDamage { get; }

        /// <summary>
        /// Gets the Direction.
        /// </summary>
        double[] Direction { get; }

        /// <summary>
        /// Gets the Weight.
        /// </summary>
        double Weight { get; }

        /// <summary>
        /// Gets the speed.
        /// </summary>
        double Speed { get; }

        /// <summary>
        /// Gets the real speed of bullet.
        /// </summary>
        double RealSpeed { get; }

        /// <summary>
        /// Flying time.
        /// </summary>
        /// <param name="deltaTime">Time spend.</param>
        void Fly(double deltaTime);

        /// <summary>
        /// Deal damage.
        /// </summary>
        /// <param name="player">Player who gets the damage.</param>
        void DoDamage(IPlayer player);
    }
}
