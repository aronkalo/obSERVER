// <copyright file="OBServerModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Xml.Linq;
    using obServer.GameModel.Interfaces;
    using obServer.GameModel.Item;
    using obServer.Model.GameModel.Item;
    using obServer.Model.Interfaces;

    /// <summary>
    /// ObServerModel class.
    /// </summary>
    public sealed class OBServerModel : IOBServerModel
    {
        private static object lockObject = new object();
        private IPlayer myPlayer;
        private List<IBaseItem> items;
        private Queue<Tuple<Type, Guid, double[], double[], double[]>> addCache;
        private IEnumerable<IBaseItem> staticCache;
        private bool staticChanged;
        private IEnumerable<IBaseItem> colliderCache;
        private bool colliderChanged;
        private IEnumerable<IBaseItem> bulletCache;
        private bool bulletChanged;
        private IEnumerable<IBaseItem> playerCache;
        private bool playerChanged;
        private IEnumerable<IBaseItem> weaponCache;
        private bool weaponChanged;
        private IEnumerable<IBaseItem> crateCache;
        private bool crateChanged;
        private bool itemsChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="OBServerModel"/> class.
        /// </summary>
        public OBServerModel()
        {
            this.items = new List<IBaseItem>();
            this.addCache = new Queue<Tuple<Type, Guid, double[], double[], double[]>>();
            this.colliderChanged = true;
            this.staticChanged = true;
            this.weaponChanged = true;
            this.playerChanged = true;
            this.bulletChanged = true;
            this.crateChanged = true;
            this.itemsChanged = true;
        }

        /// <summary>
        /// Gets or sets Player.
        /// </summary>
        public IPlayer MyPlayer
        {
            get
            {
                return this.myPlayer;
            }

            set
            {
                this.myPlayer = value;
            }
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        public IEnumerable<IBaseItem> AllItems
        {
            get
            {
                return this.items;
            }
        }

        /// <summary>
        /// Gets players.
        /// </summary>
        public IEnumerable<IBaseItem> Players
        {
            get
            {
                if (this.playerChanged)
                {
                    lock (lockObject)
                    {
                        this.playerCache = this.items.Where(x => x.GetType() == typeof(Player));
                        this.playerChanged = false;
                    }
                }

                return this.playerCache;
            }
        }

        /// <summary>
        /// Gets bullets.
        /// </summary>
        public IEnumerable<IBaseItem> Bullets
        {
            get
            {
                if (this.bulletChanged)
                {
                    lock (lockObject)
                    {
                        this.bulletCache = this.items.Where(x => x.GetType() == typeof(Bullet));
                        this.bulletChanged = false;
                    }
                }

                return this.bulletCache;
            }
        }

        /// <summary>
        /// Gets colliders.
        /// </summary>
        public IEnumerable<IBaseItem> Colliders
        {
            get
            {
                if (this.colliderChanged)
                {
                    lock (lockObject)
                    {
                        this.colliderCache = this.items.Where(x => x.Impact);
                        this.colliderChanged = false;
                    }
                }

                return this.colliderCache;
            }
        }

        /// <summary>
        /// Gets statics.
        /// </summary>
        public IEnumerable<IBaseItem> Statics
        {
            get
            {
                if (this.staticChanged)
                {
                    lock (lockObject)
                    {
                        this.staticCache = this.items.Where(x => x.GetType() == typeof(StaticItem));
                        this.staticChanged = false;
                    }
                }

                return this.staticCache;
            }
        }

        /// <summary>
        /// Gets weapons.
        /// </summary>
        public IEnumerable<IBaseItem> Weapons
        {
            get
            {
                if (this.weaponChanged)
                {
                    lock (lockObject)
                    {
                        this.weaponCache = this.items.Where(x => x.GetType() == typeof(Weapon));
                        this.weaponChanged = false;
                    }
                }

                return this.weaponCache;
            }
        }

        /// <summary>
        /// Gets crates back.
        /// </summary>
        public IEnumerable<IBaseItem> Crates
        {
            get
            {
                if (this.crateChanged)
                {
                    lock (lockObject)
                    {
                        this.crateCache = this.items.Where(x => x.GetType() == typeof(Crate));
                        this.crateChanged = false;
                    }
                }

                return this.crateCache;
            }
        }

        /// <summary>
        /// Intersection returner method.
        /// </summary>
        /// <param name="bounds">Bounds rectangle.</param>
        /// <returns>Baseitems.</returns>
        public IEnumerable<IBaseItem> Intersection(Rect bounds)
        {
            lock (lockObject)
            {
                return this.Colliders.Where(x => x.RealPrimitive.Bounds.IntersectsWith(bounds) && x.GetType() != typeof(Bullet));
            }
        }

        /// <summary>
        /// ItemCache method.
        /// </summary>
        /// <param name="item">Item parameters.</param>
        public void CacheItem(Tuple<Type, Guid, double[], double[], double[]> item)
        {
            this.addCache.Enqueue(item);
        }

        /// <summary>
        /// Item constructor method.
        /// </summary>
        /// <param name="item">Item type.</param>
        /// <param name="type">Type type.</param>
        public void ConstructItem(IBaseItem item, Type type)
        {
            if (!this.items.Contains(item))
            {
                lock (lockObject)
                {
                    this.items.Add(item);
                }

                this.ItemsChanged(type);
            }
        }

        /// <summary>
        /// Item destruction method.
        /// </summary>
        /// <param name="id">Item id.</param>
        public void DestructItem(Guid id)
        {
            lock (lockObject)
            {
                var items = this.items.Where(x => x.Id == id);
                if (items.Count() == 1)
                {
                    IBaseItem item = items.First();
                    lock (lockObject)
                    {
                        this.items.Remove(item);
                    }

                    Type itemType = item.GetType();
                    item = null;
                    this.ItemsChanged(itemType);
                }
            }
        }

        /// <summary>
        /// Cache load method.
        /// </summary>
        public void LoadCache()
        {
            lock (lockObject)
            {
                for (int i = 0; i < this.addCache.Count; i++)
                {
                    var cacheTuple = this.addCache.Dequeue();
                    this.ConstructItem(new Bullet(Bullet.BulletGeometry, cacheTuple.Item2, cacheTuple.Item3, cacheTuple.Item5[3], true, cacheTuple.Item5[0], cacheTuple.Item5[2], cacheTuple.Item4, cacheTuple.Item5[1]), typeof(Bullet));
                }
            }
        }

        /// <summary>
        /// StaticGeometry method.
        /// </summary>
        /// <param name="width">Width parameter.</param>
        /// <param name="height">Height parameter.</param>
        /// <returns>Geometry returns.</returns>
        public Geometry StaticGeometry(double width, double height)
        {
            return new RectangleGeometry() { Rect = new System.Windows.Rect(0, 0, width, height) };
        }

        /// <summary>
        /// Item loader method.
        /// </summary>
        /// <param name="fileName">Filename string.</param>
        public void LoadItems(string fileName)
        {
            XDocument xDoc = XDocument.Load(fileName);
            foreach (var node in xDoc.Root.Descendants("item"))
            {
                string type = node.Attribute("type").Value;
                double x = double.Parse(node.Element("x").Value.Replace('.', ','));
                double y = double.Parse(node.Element("y").Value.Replace('.', ','));
                double width = double.Parse(node.Element("width").Value.Replace('.', ','));
                double height = double.Parse(node.Element("height").Value.Replace('.', ','));
                double angle = double.Parse(node.Element("angle").Value);
                bool impact = bool.Parse(node.Element("impact").Value);
                Guid id = Guid.Parse(node.Attribute("id").Value);
                if (type == "crate")
                {
                    XElement xWeapon = node.Element("weapon");
                    Guid wId = Guid.Parse(xWeapon.Attribute("id").Value);
                    int capacity = int.Parse(xWeapon.Attribute("capacity").Value.Replace('.', ','));
                    double speed = double.Parse(xWeapon.Attribute("speed").Value.Replace('.', ','));
                    int shoot = int.Parse(xWeapon.Attribute("shoot").Value.Replace('.', ','));
                    double damage = double.Parse(xWeapon.Attribute("damage").Value.Replace('.', ','));
                    double weight = double.Parse(xWeapon.Attribute("weight").Value.Replace('.', ','));
                    IWeapon weapon = new Weapon(new EllipseGeometry() { RadiusX = 1, RadiusY = 1 }, wId, new double[] { x + (width / 2), y + (height / 2) }, 0, false, capacity, shoot, speed, damage, weight);
                    weapon.Owned = true;
                    ICrate crate = new Crate(new RectangleGeometry(new Rect(0, 0, width, height)), id, new double[] { x, y }, angle, impact);
                    crate.ContainedWeapon = weapon;
                    this.ConstructItem(weapon, weapon.GetType());
                    this.ConstructItem(crate, crate.GetType());
                }
                else
                {
                    this.ConstructItem(new StaticItem(new RectangleGeometry() { Rect = new System.Windows.Rect(0, 0, width, height) }, id, new double[] { x, y }, angle, new double[] { width, height }, impact, type), typeof(StaticItem));
                }
            }
        }

        /// <summary>
        /// Visual remover.
        /// </summary>
        public void RemoveVisuals()
        {
            this.items.RemoveAll(x => x.GetType() == typeof(StaticItem) && !x.Impact);
        }

        /// <summary>
        /// Username setter.
        /// </summary>
        /// <param name="name">Name string.</param>
        public void SetUsername(string name)
        {
            IPlayer player = new Player(Player.PlayerGeometry, Guid.NewGuid(), new double[] { 2500, 2500 }, 0, true, 100, name);
            this.myPlayer = player;
        }

        private void ItemsChanged(Type type)
        {
            switch (type.Name.ToString())
            {
                case "BaseItem":
                    this.bulletChanged = true;
                    this.colliderChanged = true;
                    break;
                case "Weapon":
                    this.weaponChanged = true;
                    this.itemsChanged = true;
                    break;
                case "Bullet":
                    this.bulletChanged = true;
                    this.colliderChanged = true;
                    this.itemsChanged = true;
                    break;
                case "StaticItem":
                    this.staticChanged = true;
                    this.colliderChanged = true;
                    this.itemsChanged = true;
                    break;
                case "Player":
                    this.playerChanged = true;
                    this.colliderChanged = true;
                    this.itemsChanged = true;
                    break;
                case "Crate":
                    this.crateChanged = true;
                    this.colliderChanged = true;
                    this.itemsChanged = true;
                    break;
                default:
                    break;
            }
        }
    }
}
