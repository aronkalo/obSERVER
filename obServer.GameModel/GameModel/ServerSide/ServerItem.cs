// <copyright file="ServerItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameModel.ServerSide
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Server item structure.
    /// </summary>
    public class ServerItem
    {
        private string typography;
        private Rect bounds;
        private Guid id;
        private bool impact;
        private Vector direction;
        private Vector startPoint;
        private string name;
        private double speed;
        private double weight;
        private bool owned;
        private Guid containedId;

        /// <summary>
        /// Gets or sets the Bounds property.
        /// </summary>
        public Rect Bounds
        {
            get { return this.bounds; }
            set { this.bounds = value; }
        }

        /// <summary>
        /// Gets or sets the type property.
        /// </summary>
        public string Typography
        {
            get { return this.typography; }
            set { this.typography = value; }
        }

        /// <summary>
        /// Gets or sets the ID property.
        /// </summary>
        public Guid Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether impacting with something.
        /// </summary>
        public bool Impact
        {
            get { return this.impact; }
            set { this.impact = value; }
        }

        /// <summary>
        /// Gets or sets the direction vector.
        /// </summary>
        public Vector Direction
        {
            get { return this.direction; }
            set { this.direction = value; }
        }

        /// <summary>
        /// Gets or sets the startpoint vector.
        /// </summary>
        public Vector StartPoint
        {
            get { return this.startPoint; }
            set { this.startPoint = value; }
        }

        /// <summary>
        /// Gets or sets the name property in the server.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets or sets the speed property.
        /// </summary>
        public double Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }

        /// <summary>
        /// Gets or sets the weight property.
        /// </summary>
        public double Weight
        {
            get { return this.weight; }
            set { this.weight = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether owned by somebody.
        /// </summary>
        public bool Owned
        {
            get { return this.owned; }
            set { this.owned = value; }
        }

        /// <summary>
        /// Gets or sets the weapon id.
        /// </summary>
        public Guid OwnerId
        {
            get
            {
                return this.containedId;
            }

            set
            {
                this.containedId = value;
            }
        }
    }
}
