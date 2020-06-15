// <copyright file="ServerSideModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Xml.Linq;
    using obServer.GameModel.Interfaces;
    using obServer.GameModel.ServerSide;

    /// <summary>
    /// Model on the server side class.
    /// </summary>
    public class ServerSideModel : IServerSideModel
    {
        private string fileName = "Map.xml";
        private List<ServerItem> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerSideModel"/> class.
        /// </summary>
        public ServerSideModel()
        {
            this.items = new List<ServerItem>();
        }

        /// <summary>
        /// Gets all items on the server.
        /// </summary>
        public IEnumerable<ServerItem> AllItems
        {
            get
            {
                return this.items;
            }
        }

        /// <summary>
        /// Gets Players who connected to the server.
        /// </summary>
        public IEnumerable<ServerItem> Players
        {
            get
            {
                return this.items.Where(x => x.Type == "Player");
            }
        }

        /// <summary>
        /// Gets the bullets on the server.
        /// </summary>
        public IEnumerable<ServerItem> Bullets
        {
            get
            {
                return this.items.Where(x => x.Type == "Bullet");
            }
        }

        /// <summary>
        /// Gets the weapons on the server.
        /// </summary>
        public IEnumerable<ServerItem> Weapons
        {
            get
            {
                return this.items.Where(x => x.Type == "Weapon");
            }
        }

        /// <summary>
        /// Constructing a new item based on the parameters.
        /// </summary>
        /// <param name="id">ID of item.</param>
        /// <param name="type">The type of the new item.</param>
        /// <param name="bounds">Bounds of the new item.</param>
        /// <param name="impact">Impact of the new item.</param>
        public void ConstructItem(Guid id, string type, Rect bounds, bool impact)
        {
            ServerItem updateItem = new ServerItem()
            {
                Bounds = bounds,
                Type = type,
                Id = id,
                Impact = impact,
            };
            var items = this.items.Where(x => x.Id == id);
            if (items.Count() == 1)
            {
                var item = items.First();
                item = updateItem;
            }
            else
            {
                this.items.Add(updateItem);
            }
        }

        /// <summary>
        /// Create a new bullet.
        /// </summary>
        /// <param name="id">Bullet id.</param>
        /// <param name="type">Type of bullet.</param>
        /// <param name="bounds">Bounds of bullet.</param>
        /// <param name="impact">Impact of bullet.</param>
        /// <param name="direction">Bullet direction.</param>
        /// <param name="weight">Bullet weight.</param>
        /// <param name="speed">Bullet speed.</param>
        public void ConstructBullet(Guid id, string type, Rect bounds, bool impact, Vector direction, double weight, double speed)
        {
            ServerItem updateItem = new ServerItem()
            {
                Bounds = bounds,
                Type = type,
                Id = id,
                Impact = impact,
                Direction = direction,
                Weight = weight,
                Speed = speed,
            };
            var items = this.items.Where(x => x.Id == id);
            if (items.Count() == 1)
            {
                var item = items.First();
                item = updateItem;
            }
            else
            {
                this.items.Add(updateItem);
            }
        }

        /// <summary>
        /// Weapon construction method.
        /// </summary>
        /// <param name="id">Weapon id.</param>
        /// <param name="type">Weapon type.</param>
        /// <param name="bounds">Weapon bounds.</param>
        /// <param name="impact">Weapon impact.</param>
        /// <param name="owned">Weapon owned.</param>
        /// <param name="owner">Weapon owner.</param>
        public void ConstructWeapon(Guid id, string type, Rect bounds, bool impact, bool owned, Guid owner)
        {
            ServerItem updateItem = new ServerItem()
            {
                Bounds = bounds,
                Type = type,
                Id = id,
                Impact = impact,
                Owned = owned,
                OwnerId = owner,
            };
            var items = this.items.Where(x => x.Id == id);
            if (items.Count() == 1)
            {
                var item = items.First();
                item = updateItem;
            }
            else
            {
                this.items.Add(updateItem);
            }
        }

        /// <summary>
        /// Destructing item on the server.
        /// </summary>
        /// <param name="id">Id of the destructable item.</param>
        public void DestructItem(Guid id)
        {
            var items = this.items.Where(x => x.Id == id);
            if (items.Count() == 1)
            {
                this.items.Remove(items.First());
            }
        }

        /// <summary>
        /// Collecting the bullet hits.
        /// </summary>
        /// <param name="id">Id of the bullet.</param>
        /// <returns>Returning the remaining server items.</returns>
        public ServerItem[] BulletHit(Guid id)
        {
            var items = this.items.Where(x => x.Id == id);
            if (items.Count() == 1)
            {
                var item = items.First();
                var bull = this.items.Where(x => x.Type == "Bullet" && x.Bounds.Contains(item.Bounds));
                if (bull.Count() > 0)
                {
                    return bull.ToArray();
                }

                return null;
            }

            return null;
        }

        /// <summary>
        /// Collecting the collisions.
        /// </summary>
        /// <param name="id">Id of the colliding.</param>
        /// <returns>Returns the colliding items.</returns>
        public ServerItem[] Collision(Guid id)
        {
            var items = this.items.Where(x => x.Id == id);
            if (items.Count() == 1)
            {
                var item = items.First();
                var bull = this.items.Where(x => x.Impact && x.Bounds.Contains(item.Bounds));
                if (bull.Count() > 0)
                {
                    return bull.ToArray();
                }

                return null;
            }

            return null;
        }

        /// <summary>
        /// Loading map.
        /// </summary>
        public void LoadMap()
        {
            XDocument xDoc = XDocument.Load(this.fileName);
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
                    this.ConstructItem(id, type, new Rect(x, y, width, height), true);
                    this.ConstructWeapon(wId, "Weapon", new Rect(x, y, 1, 1), false, false, id);
                }
                else
                {
                    this.ConstructItem(id, type, new Rect(x, y, width, height), true);
                }
            }
        }
    }
}
