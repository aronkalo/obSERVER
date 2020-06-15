// <copyright file="Crate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace obServer.Model.GameModel.Item
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using obServer.GameModel.Interfaces;
    using obServer.GameModel.Item;
    using obServer.Model.Interfaces;

    /// <summary>
    /// The crate class.
    /// </summary>
    public sealed class Crate : BaseItem, ICrate, IBaseItem
    {
        private IWeapon containedWeapon;

        /// <summary>
        /// Initializes a new instance of the <see cref="Crate"/> class.
        /// </summary>
        /// <param name="geometry">The geom.</param>
        /// <param name="id">The id.</param>
        /// <param name="position">The pos.</param>
        /// <param name="rotation">The rot.</param>
        /// <param name="impact">The impact.</param>
        public Crate(Geometry geometry, Guid id, double[] position, double rotation, bool impact)
            : base(geometry, id, position, rotation, impact)
        {
        }

        /// <summary>
        /// Gets or sets teh weapon.
        /// </summary>
        public IWeapon ContainedWeapon { get => this.containedWeapon; set => this.containedWeapon = value; }

        /// <summary>
        /// Configures the picking method.
        /// </summary>
        /// <param name="primitive">Primitive.</param>
        /// <returns>True false.</returns>
        public bool InPickRange(Rect primitive)
        {
            if (this.ContainedWeapon == null)
            {
                return false;
            }

            Rect obj = this.RealPrimitive.Bounds;
            Rect range = new Rect(obj.X - obj.Width, obj.Y - obj.Height, obj.Width * 4, obj.Height * 4);
            if (primitive.IntersectsWith(range))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The pick weapon mehtod.
        /// </summary>
        /// <returns>The weapon.</returns>
        public IWeapon PickWeapon()
        {
            var helper = this.ContainedWeapon;
            this.ContainedWeapon = null;
            return helper;
        }
    }
}
