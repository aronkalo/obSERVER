// <copyright file="RepoPlayer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.Model
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using obServer.GameModel.Interfaces;
    using obServer.GameModel.Item;

    /// <summary>
    /// Player repo class.
    /// </summary>
    public class RepoPlayer : IRepoPlayer
    {
        /// <summary>
        /// Player field.
        /// </summary>
        private IPlayer player;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoPlayer"/> class.
        /// </summary>
        /// <param name="geometry">Player geometry.</param>
        /// <param name="id">Player id.</param>
        /// <param name="position">Player position.</param>
        /// <param name="rotation">Player rotation.</param>
        /// <param name="impact">Player impact.</param>
        /// <param name="health">Player health.</param>
        /// <param name="username">Player username.</param>
        public RepoPlayer(Geometry geometry, Guid id, double[] position, double rotation, bool impact, double health, string username)
        {
            this.player = new Player(geometry, id, position, rotation, impact, health, username);
        }

        /// <summary>
        /// Gets the current weapon.
        /// </summary>
        public IWeapon CurrentWeapon
        {
            get { return this.player.CurrentWeapon; }
        }

        /// <summary>
        /// Gets or sets player death.
        /// </summary>
        public EventHandler Die
        {
            get { return this.player.Die; }
            set { this.player.Die = value; }
        }

        /// <summary>
        /// Gets the player health.
        /// </summary>
        public double Health
        {
            get { return this.player.Health; }
        }

        /// <summary>
        /// Gets the stored bullets.
        /// </summary>
        public int StoredBullets
        {
            get { return this.player.StoredBullets; }
        }

        /// <summary>
        /// Gets or sets player name.
        /// </summary>
        public string Name
        {
            get { return this.player.Name; }
            set { this.player.Name = value; }
        }

        /// <summary>
        /// Gets or sets player id.
        /// </summary>
        public Guid Id
        {
            get { return this.player.Id; }
            set { this.player.Id = value; }
        }

        /// <summary>
        /// Gets or sets Player positions.
        /// </summary>
        public Vector Position
        {
            get { return this.player.Position; }
            set { this.player.Position = value; }
        }

        /// <summary>
        /// Gets or sets player rotation.
        /// </summary>
        public double Rotation
        {
            get { return this.player.Rotation; }
            set { this.player.Rotation = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets player impact.
        /// </summary>
        public bool Impact
        {
            get { return this.player.Impact; }
            set { this.player.Impact = value; }
        }

        /// <summary>
        /// Gets the player primitive.
        /// </summary>
        public Geometry RealPrimitive
        {
            get { return this.player.RealPrimitive; }
        }

        /// <summary>
        /// Weapon change method.
        /// </summary>
        /// <param name="newWeapon">Weapon type.</param>
        public void ChangeWeapon(IWeapon newWeapon)
        {
            this.player.ChangeWeapon(newWeapon);
        }

        /// <summary>
        /// Collide detection.
        /// </summary>
        /// <param name="geometry">Geometry.</param>
        /// <returns>True or false.</returns>
        public bool CollidesWith(Geometry geometry)
        {
            return this.player.CollidesWith(geometry);
        }

        /// <summary>
        /// Damaging method.
        /// </summary>
        /// <param name="damage">Damage amount.</param>
        public void Damaged(double damage)
        {
            this.player.Damaged(damage);
        }

        /// <summary>
        /// Moving method.
        /// </summary>
        /// <param name="xMove">X axis movement.</param>
        /// <param name="yMove">Y axis movement.</param>
        /// <param name="deltaTime">Time spent.</param>
        /// <param name="rotation">Rotation amunt.</param>
        public void Move(double xMove, double yMove, double deltaTime, double rotation)
        {
            this.player.Move(xMove, yMove, deltaTime, rotation);
        }

        /// <summary>
        /// Bullet pickup method.
        /// </summary>
        /// <param name="bullets">Number of bullets.</param>
        public void PickBullet(int bullets)
        {
            this.player.PickBullet(bullets);
        }

        /// <summary>
        /// Reloading method.
        /// </summary>
        public void Reload()
        {
            this.player.Reload();
        }

        /// <summary>
        /// Shooting method.
        /// </summary>
        /// <returns>Bullets.</returns>
        public IBullet[] Shoot()
        {
            return this.player.Shoot();
        }
    }
}
