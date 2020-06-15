// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameModel.Item
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using obServer.GameModel.Interfaces;

    /// <summary>
    /// Player class.
    /// </summary>
    public sealed class Player : BaseItem, IPlayer, IBaseItem
    {
        /// <summary>
        /// Player geometry field.
        /// </summary>
        public static Geometry PlayerGeometry = new EllipseGeometry() { RadiusX = Width, RadiusY = Height };

        private const double MovementSpeed = 200;
        private const double Width = 40;
        private const double Height = 40;
        private double health;
        private Vector weaponCache;
        private int storedBullets;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="geometry">Player geometry.</param>
        /// <param name="id">Player id.</param>
        /// <param name="position">Player position.</param>
        /// <param name="rotation">Player rotation.</param>
        /// <param name="impact">Player impact.</param>
        /// <param name="health">Player health.</param>
        /// <param name="username">Player username.</param>
        public Player(Geometry geometry, Guid id, double[] position, double rotation, bool impact, double health, string username)
            : base(geometry, id, position, rotation, impact)
        {
            this.Name = username;
            this.CurrentWeapon = null;
            this.health = health;
            this.weaponCache = new Vector(0, 41);
        }

        /// <summary>
        /// Gets current weapon field.
        /// </summary>
        public IWeapon CurrentWeapon { get; private set; }

        /// <summary>
        /// Gets or sets name field.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Die field.
        /// </summary>
        public EventHandler Die { get; set; }

        /// <summary>
        /// Gets health field.
        /// </summary>
        public double Health
        {
            get
            {
                return this.health;
            }
        }

        /// <summary>
        /// Gets stored bullets.
        /// </summary>
        public int StoredBullets
        {
            get
            {
                return this.storedBullets;
            }
        }

        /// <summary>
        /// Weapon change method.
        /// </summary>
        /// <param name="newWeapon">Weapon type.</param>
        public void ChangeWeapon(IWeapon newWeapon)
        {
            newWeapon.Owned = true;
            if (this.CurrentWeapon == null)
            {
                this.CurrentWeapon = newWeapon;
                Vector pos = this.WeaponPosition(this.Rotation);
                this.CurrentWeapon.Position = new Vector(this.Position.X + pos.X, this.Position.Y + pos.Y);
            }
            else
            {
                IWeapon helper = this.CurrentWeapon;
                helper.Owned = false;
                this.CurrentWeapon = newWeapon;
                Vector pos = this.WeaponPosition(this.Rotation);
                this.CurrentWeapon.Position = new Vector(this.Position.X + pos.X, this.Position.Y + pos.Y);
            }
        }

        /// <summary>
        /// Bullet pick method.
        /// </summary>
        /// <param name="bullets">Bullet amount.</param>
        public void PickBullet(int bullets)
        {
            this.storedBullets += bullets;
        }

        /// <summary>
        /// Shoot method.
        /// </summary>
        /// <returns>Return bullets.</returns>
        public IBullet[] Shoot()
        {
            if (this.CurrentWeapon != null)
            {
                return this.CurrentWeapon.DoShoot();
            }

            return null;
        }

        /// <summary>
        /// Reload method.
        /// </summary>
        public void Reload()
        {
            this.CurrentWeapon.DoReload(this.storedBullets);
        }

        /// <summary>
        /// Move method.
        /// </summary>
        /// <param name="xMove">X axis movement.</param>
        /// <param name="yMove">Y axis movement.</param>
        /// <param name="deltaTime">Time spend.</param>
        /// <param name="rotation">Player rotation.</param>
        public void Move(double xMove, double yMove, double deltaTime, double rotation)
        {
            double xMovement = xMove * deltaTime * MovementSpeed;
            double yMovement = yMove * deltaTime * MovementSpeed;
            this.ChangePosition(xMovement, yMovement, rotation);
            if (this.CurrentWeapon != null)
            {
                Vector pos = this.WeaponPosition(this.Rotation);
                this.CurrentWeapon.SetPosition(this.Position.X + pos.X, this.Position.Y + pos.Y, this.Rotation);
            }
        }

        /// <summary>
        /// Player damage method.
        /// </summary>
        /// <param name="damage">Damage amount.</param>
        public void Damaged(double damage)
        {
            this.health -= damage;
            if (this.health < 0)
            {
                this.Die?.Invoke(this, null);
            }
        }

        private Vector WeaponPosition(double angle)
        {
            Matrix m = Matrix.Identity;
            m.RotateAt(angle, this.Position.X + (Width / 2), this.Position.Y + (Height / 2));

            // m.Rotate(angle);
            return m.Transform(this.weaponCache);
        }
    }
}
