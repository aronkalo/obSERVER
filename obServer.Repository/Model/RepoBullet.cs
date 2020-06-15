// <copyright file="RepoBullet.cs" company="PlaceholderCompany">
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
    /// Bullet repo class.
    /// </summary>
    public class RepoBullet : IRepoBullet
    {
        private IBullet model;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoBullet"/> class.
        /// </summary>
        /// <param name="geometry">Geometry.</param>
        /// <param name="id">Bullet id.</param>
        /// <param name="position">Bullet position.</param>
        /// <param name="rotation">Bullet rotation.</param>
        /// <param name="impact">Bullet impact.</param>
        /// <param name="flySpeed">Bulelt flyspeed.</param>
        /// <param name="damage">Bullet damage.</param>
        /// <param name="direction">Bullet direction.</param>
        /// <param name="weight">Bullet weight.</param>
        public RepoBullet(Geometry geometry, Guid id, double[] position, double rotation, bool impact, double flySpeed, double damage, double[] direction, double weight)
        {
            this.model = new Bullet(geometry, id, position, rotation, impact, flySpeed, damage, direction, weight);
        }

        /// <summary>
        /// Gets bullet damage.
        /// </summary>
        public double BulletDamage
        {
            get { return this.model.BulletDamage; }
        }

        /// <summary>
        /// Gets Bullet direction.
        /// </summary>
        public double[] Direction
        {
            get { return this.model.Direction; }
        }

        /// <summary>
        /// Gets bullet weight.
        /// </summary>
        public double Weight
        {
            get { return this.model.Weight; }
        }

        /// <summary>
        /// Gets bullet speed.
        /// </summary>
        public double Speed
        {
            get { return this.model.Speed; }
        }

        /// <summary>
        /// Gets bullet real speed.
        /// </summary>
        public double RealSpeed
        {
            get { return this.model.RealSpeed; }
        }

        /// <summary>
        /// Gets or sets bullet id.
        /// </summary>
        public Guid Id
        {
            get { return this.model.Id; } set { this.model.Id = value; }
        }

        /// <summary>
        /// Gets or sets bullet position.
        /// </summary>
        public Vector Position
        {
            get { return this.model.Position; } set { this.model.Position = value; }
        }

        /// <summary>
        /// Gets or sets bullet rotation.
        /// </summary>
        public double Rotation
        {
            get { return this.model.Rotation; } set { this.model.Rotation = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether bullet impact.
        /// </summary>
        public bool Impact
        {
            get { return this.model.Impact; } set { this.model.Impact = value; }
        }

        /// <summary>
        /// Gets primitive geometry.
        /// </summary>
        public Geometry RealPrimitive
        {
            get { return this.model.RealPrimitive; }
        }

        /// <summary>
        /// Collision detection.
        /// </summary>
        /// <param name="geometry">Geometry.</param>
        /// <returns>True or false.</returns>
        public bool CollidesWith(Geometry geometry)
        {
            return this.model.CollidesWith(geometry);
        }

        /// <summary>
        /// Damaging method.
        /// </summary>
        /// <param name="player">Player type.</param>
        public void DoDamage(IPlayer player)
        {
            this.model.DoDamage(player);
        }

        /// <summary>
        /// Flying method.
        /// </summary>
        /// <param name="deltaTime">Time spend.</param>
        public void Fly(double deltaTime)
        {
            this.model.Fly(deltaTime);
        }
    }
}
