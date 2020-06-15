// <copyright file="RepoWeapon.cs" company="PlaceholderCompany">
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
    /// Weapon repo class.
    /// </summary>
    public class RepoWeapon : IRepoWeapon
    {
        /// <summary>
        /// Weapon field.
        /// </summary>
        private IWeapon weapon;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoWeapon"/> class.
        /// </summary>
        /// <param name="geometry">Weapon geaometry.</param>
        /// <param name="id">Weapon id.</param>
        /// <param name="position">Weapon position.</param>
        /// <param name="rotation">Weapon rotation.</param>
        /// <param name="impact">Weapon  impact.</param>
        /// <param name="bulletCapacity">Weapon capacity.</param>
        /// <param name="bulletShoot">Weapon shoot.</param>
        /// <param name="bulletSpeed">Weapon bullet speed.</param>
        /// <param name="damage">Weapon damage.</param>
        /// <param name="bulletWeight">Bullets weight.</param>
        public RepoWeapon(Geometry geometry, Guid id, double[] position, double rotation, bool impact, int bulletCapacity, int bulletShoot, double bulletSpeed, double damage, double bulletWeight)
        {
            this.weapon = new Weapon(geometry, id, position, rotation, impact, bulletCapacity, bulletShoot, bulletSpeed, damage, bulletWeight);
        }

        /// <summary>
        /// Gets weapon info.
        /// </summary>
        public double[] WeaponInfo
        {
            get { return this.weapon.WeaponInfo; }
        }

        /// <summary>
        /// Gets or sets Weapon id.
        /// </summary>
        public Guid Id
        {
            get { return this.weapon.Id; } set { this.weapon.Id = value; }
        }

        /// <summary>
        /// Gets or sets Weapon position.
        /// </summary>
        public Vector Position
        {
            get { return this.weapon.Position; } set { this.weapon.Position = value; }
        }

        /// <summary>
        /// Gets or sets weapon rotation.
        /// </summary>
        public double Rotation
        {
            get { return this.weapon.Rotation; } set { this.weapon.Rotation = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets weapon impact.
        /// </summary>
        public bool Impact
        {
            get { return this.weapon.Impact; } set { this.weapon.Impact = value; }
        }

        /// <summary>
        /// Gets or geometry primitives.
        /// </summary>
        public Geometry RealPrimitive
        {
            get { return this.weapon.RealPrimitive; }
        }

        /// <summary>
        /// Collision detection.
        /// </summary>
        /// <param name="geometry">Item geometry.</param>
        /// <returns>True or false.</returns>
        public bool CollidesWith(Geometry geometry)
        {
            return this.weapon.CollidesWith(geometry);
        }

        /// <summary>
        /// Weapon reload method.
        /// </summary>
        /// <param name="storedBullets">Amount of stored bullets.</param>
        public void DoReload(int storedBullets)
        {
            this.weapon.DoReload(storedBullets);
        }

        /// <summary>
        /// Shooting method.
        /// </summary>
        /// <returns>Weapon shoot.</returns>
        public IBullet[] DoShoot()
        {
            return this.weapon.DoShoot();
        }

        /// <summary>
        /// Position setting method.
        /// </summary>
        /// <param name="xMovement">X axis set.</param>
        /// <param name="yMovement">Y axis set.</param>
        /// <param name="rotation">Rotation amount.</param>
        public void SetPosition(double xMovement, double yMovement, double rotation)
        {
            this.weapon.SetPosition(xMovement, yMovement, rotation);
        }
    }
}
