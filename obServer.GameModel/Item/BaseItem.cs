// <copyright file="BaseItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameModel.Item
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using obServer.GameModel.Interfaces;

    /// <summary>
    /// Abstract class for Items.
    /// </summary>
    public class BaseItem : IBaseItem
    {
        private Guid id;
        private Vector position;
        private double rotation;
        private bool impact;
        private Geometry primitve;
        private Geometry cache;
        private bool changed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseItem"/> class.
        /// </summary>
        /// <param name="geometry">Item geometry.</param>
        /// <param name="id">Item name.</param>
        /// <param name="position">Item position.</param>
        /// <param name="rotation">Rotation parameter.</param>
        /// <param name="impact">Item impact.</param>
        public BaseItem(Geometry geometry, Guid id, double[] position, double rotation, bool impact)
        {
            this.primitve = geometry;
            this.Id = id;
            this.Position = new Vector(position[0], position[1]);
            this.SetPosition(position[0], position[1]);
            this.Rotation = rotation;
            this.Impact = impact;
            this.changed = true;
        }

        /// <summary>
        /// Gets or sets id property.
        /// </summary>
        public Guid Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// Gets or sets position.
        /// </summary>
        public Vector Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        /// <summary>
        /// Gets or sets Rotation.
        /// </summary>
        public double Rotation
        {
            get
            {
                return this.rotation;
            }

            set
            {
                this.rotation = value;
                this.changed = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Impact.
        /// </summary>
        public bool Impact
        {
            get
            {
                return this.impact;
            }

            set
            {
                this.impact = value;
                this.changed = true;
            }
        }

        /// <summary>
        /// Gets primitives.
        /// </summary>
        public Geometry RealPrimitive
        {
            get
            {
                if (this.changed)
                {
                    this.primitve.Transform = new TranslateTransform(this.Position.X, this.Position.Y);
                    this.cache = this.primitve.GetFlattenedPathGeometry();
                    this.cache.Transform = new RotateTransform(this.Rotation, this.Position.X, this.Position.Y);
                    this.changed = false;
                }

                return this.cache;
            }
        }

        /// <summary>
        /// Watch for the colliding.
        /// </summary>
        /// <param name="geometry">Geometry what we want to test.</param>
        /// <returns>True if collide, false if not.</returns>
        public bool CollidesWith(Geometry geometry)
        {
            if (this.impact)
            {
                double area = Geometry.Combine(this.RealPrimitive, geometry, GeometryCombineMode.Intersect, null).GetArea();
                if (area > 0)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// Setting the positions.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        protected virtual void SetPosition(double x, double y)
        {
            this.Position = new Vector(x, y);
            this.changed = true;
        }

        /// <summary>
        /// Changing the geometrys position.
        /// </summary>
        /// <param name="x">Move vector X parameter.</param>
        /// <param name="y">Move vector Y parameter.</param>
        /// <param name="angle">Rotation angle.</param>
        protected virtual void ChangePosition(double x, double y, double angle = 0)
        {
            this.Position = new Vector(this.Position.X + x, this.Position.Y + y);
            this.Rotation = angle;
            this.changed = true;
        }
    }
}