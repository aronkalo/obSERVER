// <copyright file="Weapon.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameModel.Item
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using obServer.GameModel.Interfaces;

    /// <summary>
    /// Weapons class.
    /// </summary>
    public sealed class Weapon : BaseItem, IWeapon, IBaseItem
    {
        /// <summary>
        /// Pistols starting capacity.
        /// </summary>
        public const int PistolCapacity = 20;

        /// <summary>
        /// Shotgun starting capacity.
        /// </summary>
        public const int ShotgunCapacity = 8;

        /// <summary>
        /// Machine gun starting capacity.
        /// </summary>
        public const int MachinegunCapacity = 30;

        /// <summary>
        /// Pistols damage.
        /// </summary>
        public const int PistolShoot = 1;

        /// <summary>
        /// Shotguns damage.
        /// </summary>
        public const int ShotgunShoot = 6;

        /// <summary>
        /// Machine gun damage.
        /// </summary>
        public const int MachinegunShoot = 3;

        /// <summary>
        /// Pistol bullet speed.
        /// </summary>
        public const int PistolSpeed = 400;

        /// <summary>
        /// Shotgun bullet speed.
        /// </summary>
        public const int ShotgunSpeed = 550;

        /// <summary>
        /// Machine gun bullet speed.
        /// </summary>
        public const int MachinegunSpeed = 450;

        private readonly Random bulletRandomizer;
        private int bulletCapacity;
        private int bulletShoot;
        private double bulletSpeed;
        private double damage;
        private double bulletWeight;
        private int bulletCount;
        private bool owned;

        /// <summary>
        /// Initializes a new instance of the <see cref="Weapon"/> class.
        /// </summary>
        /// <param name="geometry">Weapons geometry.</param>
        /// <param name="id">Weapons ID.</param>
        /// <param name="position">Weapons position.</param>
        /// <param name="rotation">Weapons rotation.</param>
        /// <param name="impact">Impact with something.</param>
        /// <param name="bulletCapacity">Magazine size.</param>
        /// <param name="bulletShoot">Number of shots.</param>
        /// <param name="bulletSpeed">Bullet speed.</param>
        /// <param name="damage">Bullet damage.</param>
        /// <param name="bulletWeight">Weight of bullet.</param>
        public Weapon(Geometry geometry, Guid id, double[] position, double rotation, bool impact, int bulletCapacity, int bulletShoot, double bulletSpeed, double damage, double bulletWeight)
            : base(geometry, id, position, rotation, impact)
        {
            this.bulletRandomizer = new Random();
            this.bulletCapacity = bulletCapacity;
            this.bulletCount = this.bulletRandomizer.Next((int)bulletCapacity / 2, bulletCapacity + 1);
            this.bulletShoot = bulletShoot;
            this.bulletSpeed = bulletSpeed;
            this.damage = damage;
            this.bulletWeight = bulletWeight;
        }

        /// <summary>
        /// Gets weapon information.
        /// </summary>
        public double[] WeaponInfo
        {
            get
            {
                return new double[] { this.bulletCapacity, this.bulletSpeed, this.damage, this.bulletWeight, this.bulletShoot, this.bulletCount };
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is owned.
        /// </summary>
        public bool Owned
        {
            get
            {
                return this.owned;
            }

            set
            {
                this.owned = value;
            }
        }

        /// <summary>
        /// Handling the shooted bullets.
        /// </summary>
        /// <returns>Remaining bullets.</returns>
        public IBullet[] DoShoot()
        {
            if (this.bulletCount > 0)
            {
                this.bulletCount--;
                Bullet[] bullets = new Bullet[this.bulletShoot];
                for (int i = 0; i < bullets.Length; i++)
                {
                    double rot = this.Rotation + (this.bulletRandomizer.Next(-1, 2) * this.bulletRandomizer.NextDouble() * 10 * i);
                    double[] direction = this.BulletDirection(rot);
                    bullets[i] = new Bullet(
                        Bullet.BulletGeometry,
                        Guid.NewGuid(),
                        new double[] { this.Position.X + direction[0], this.Position.Y + direction[1] },
                        this.Rotation,
                        true,
                        this.bulletSpeed,
                        this.damage,
                        direction,
                        this.bulletWeight);
                }

                return bullets;
            }

            return null;
        }

        /// <summary>
        /// Handling the weapong movement and rotation.
        /// </summary>
        /// <param name="xPos">X axis movement.</param>
        /// <param name="yPos">Y axis movement.</param>
        /// <param name="rotation">Weapon rotation.</param>
        public void SetPosition(double xPos, double yPos, double rotation)
        {
            this.SetPosition(xPos, yPos);
            this.Rotation = rotation;
        }

        /// <summary>
        /// Reloading the weapon.
        /// </summary>
        /// <param name="storedBullets">Gets the stored bullets number.</param>
        public void DoReload(int storedBullets)
        {
            if (storedBullets > (this.bulletCapacity - this.bulletCount))
            {
                this.bulletCount = this.bulletCapacity;
            }
            else
            {
                this.bulletCount += storedBullets;
            }
        }

        /// <summary>
        /// Handling the weapong movement and rotation.
        /// </summary>
        /// <param name="x">X axis movement.</param>
        /// <param name="y">Y axis movement.</param>
        protected override void SetPosition(double x, double y)
        {
            base.SetPosition(x, y);
        }

        private double[] BulletDirection(double angle)
        {
            Matrix m = Matrix.Identity;
            m.Rotate(angle);
            Vector trans = m.Transform(new Vector(0, 1));
            trans.Normalize();
            var c = trans.Length;
            return new double[] { trans.X, trans.Y };
        }
    }
}
