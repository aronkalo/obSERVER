// <copyright file="RepoCrate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.Model
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using obServer.GameModel.Interfaces;
    using obServer.Model.GameModel.Item;
    using obServer.Model.Interfaces;

    /// <summary>
    /// Crate repo class.
    /// </summary>
    public class RepoCrate : IRepoCrate
    {
        /// <summary>
        /// Crate field.
        /// </summary>
        private ICrate crate;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoCrate"/> class.
        /// </summary>
        /// <param name="geometry">Crate geometry.</param>
        /// <param name="id">Crate id.</param>
        /// <param name="position">Crate position.</param>
        /// <param name="rotation">Crate rotation.</param>
        /// <param name="impact">Crate impact.</param>
        public RepoCrate(Geometry geometry, Guid id, double[] position, double rotation, bool impact)
        {
            this.crate = new Crate(geometry, id, position, rotation, impact);
        }

        /// <summary>
        /// Gets or sets the contained weapons.
        /// </summary>
        public IWeapon ContainedWeapon
        {
            get { return this.crate.ContainedWeapon; } set { this.crate.ContainedWeapon = value; }
        }

        /// <summary>
        /// Gets or sets Crate id.
        /// </summary>
        public Guid Id
        {
            get { return this.crate.Id; } set { this.crate.Id = value; }
        }

        /// <summary>
        /// Gets or sets crate position.
        /// </summary>
        public Vector Position
        {
            get { return this.crate.Position; } set { this.crate.Position = value; }
        }

        /// <summary>
        /// Gets or sets crate rotation.
        /// </summary>
        public double Rotation
        {
            get { return this.crate.Rotation; } set { this.crate.Rotation = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets crate impact.
        /// </summary>
        public bool Impact
        {
            get { return this.crate.Impact; } set { this.crate.Impact = value; }
        }

        /// <summary>
        /// Gets real primitive.
        /// </summary>
        public Geometry RealPrimitive
        {
            get { return this.crate.RealPrimitive; }
        }

        /// <summary>
        /// Collision detection.
        /// </summary>
        /// <param name="geometry">Geometry colliding.</param>
        /// <returns>True or false.</returns>
        public bool CollidesWith(Geometry geometry)
        {
            return this.crate.CollidesWith(geometry);
        }

        /// <summary>
        /// Pickrange detection.
        /// </summary>
        /// <param name="primitive">Rectangle.</param>
        /// <returns>True or false.</returns>
        public bool InPickRange(Rect primitive)
        {
            return this.crate.InPickRange(primitive);
        }

        /// <summary>
        /// Picking up weapong method.
        /// </summary>
        /// <returns>Weapon.</returns>
        public IWeapon PickWeapon()
        {
            return this.crate.PickWeapon();
        }
    }
}
