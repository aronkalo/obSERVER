// <copyright file="Bullet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameModel.Item
{
    using System;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using obServer.GameModel.Interfaces;

    /// <summary>
    /// Bullet class.
    /// </summary>
    public sealed class Bullet : BaseItem, IBullet
    {
        /// <summary>
        /// Gets geometry of bullets.
        /// </summary>
        public static EllipseGeometry BulletGeometry = new EllipseGeometry() { RadiusX = BulletWidth, RadiusY = BulletHeight };
        private const double BulletWidth = 3;
        private const double BulletHeight = 3;
        private double flySpeed;
        private double supressionCache;
        private double realspeedCache;
        private double weight;
        private double damage;
        private double[] direction;
        private double[] startPos;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// </summary>
        /// <param name="geometry">Bullet geometry.</param>
        /// <param name="id">Bullet ID.</param>
        /// <param name="position">Bullet position.</param>
        /// <param name="rotation">Bullet rotation.</param>
        /// <param name="impact">Impact property. True if collide.</param>
        /// <param name="flySpeed">Flyspeed of bullet.</param>
        /// <param name="damage">Damage deal by bullet.</param>
        /// <param name="direction">Fly direction.</param>
        /// <param name="weight">Weight of bullet.</param>
        public Bullet(Geometry geometry, Guid id, double[] position, double rotation, bool impact, double flySpeed, double damage, double[] direction, double weight)
            : base(geometry, id, position, rotation, impact)
        {
            this.flySpeed = flySpeed;
            this.realspeedCache = flySpeed;
            this.damage = damage;
            this.direction = new double[2];
            this.direction[0] = direction[0];
            this.direction[1] = direction[1];
            this.startPos = new double[2];
            this.startPos[0] = position[0];
            this.startPos[1] = position[1];
            this.supressionCache = 1;
            this.weight = weight;
        }

        /// <summary>
        /// Gets the speed of bullet.
        /// </summary>
        public double RealSpeed
        {
            get { return this.realspeedCache; }
        }

        /// <summary>
        /// Gets the bullet damage.
        /// </summary>
        public double BulletDamage
        {
            get
            {
                return this.damage;
            }
        }

        /// <summary>
        /// Gets the bullet direction.
        /// </summary>
        public double[] Direction
        {
            get
            {
                return this.direction;
            }
        }

        /// <summary>
        /// Gets the weight of bullet.
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }
        }

        /// <summary>
        /// Gets the bullet speed.
        /// </summary>
        public double Speed
        {
            get
            {
                return this.flySpeed;
            }
        }

        /// <summary>
        /// Gets the suppressing of bullet by weight.
        /// </summary>
        private double Supression
        {
            get
            {
                double a = this.startPos[0] - this.Position.X;
                double b = this.startPos[1] - this.Position.Y;
                double sup = 1 - (Math.Sqrt((a * a) + (b * b)) * this.weight);
                this.supressionCache = sup > 0 ? sup : 0;
                return this.supressionCache;
            }
        }

        /// <summary>
        /// Bullet flying property.
        /// </summary>
        /// <param name="deltaTime">Fly time.</param>
        public void Fly(double deltaTime)
        {
            this.realspeedCache = this.flySpeed * this.Supression;
            double xMovement = this.direction[0] * this.realspeedCache * deltaTime;
            double yMovement = this.direction[1] * this.realspeedCache * deltaTime;
            this.ChangePosition(xMovement, yMovement, 0);
        }

        /// <summary>
        /// Damaging the enemy player.
        /// </summary>
        /// <param name="player">Enemy player who gets the damage.</param>
        public void DoDamage(IPlayer player)
        {
            player.Damaged(this.damage);
        }
    }
}
