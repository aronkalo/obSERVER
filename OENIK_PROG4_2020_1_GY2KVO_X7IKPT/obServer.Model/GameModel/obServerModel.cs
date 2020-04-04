using System;
using obServer.Model.GameModel.Item;
using System.Collections.Generic;
using System.Linq;
using obServer.Model.Interfaces;
using System.Xml.Linq;
using System.Windows.Media;
using System.Windows;

namespace obServer.Model.GameModel
{
    public sealed class obServerModel :IobServerModel
    {
        public Geometry StaticGeometry(double width, double height)
        {
            return new RectangleGeometry() { Rect = new System.Windows.Rect(0, 0, width, height) };
        }

        public obServerModel(int width, int height)
        {
            Items = new List<IBaseItem>();
            IPlayer player = new Player(Player.PlayerGeometry, Guid.NewGuid(), new double[] { 300, 300 }, 0, true, 100);
            IWeapon weap = new Weapon(new EllipseGeometry() { RadiusX = 1, RadiusY = 1 }, Guid.NewGuid(), new double[] { player.Position.X + 10, player.Position.Y + 35 }, player.Rotation, false, 7000, 7, 1100, 10, 0.001);
            ConstructItem(weap);
            //ConstructItem(player);
            player.ChangeWeapon(weap);
            myPlayer = player;
            LoadItems(width, height);
        }

        private IPlayer myPlayer;

        private List<IBaseItem> Items;

        private IStaticItem[] map;

        private bool itemsChanged;

        private void ItemsChanged()
        {
            staticChanged = true;
            playerChanged = true;
            colliderChanged = true;
            bulletChanged = true;
            weaponChanged = true;
            itemsChanged = true;
        }
        public  IStaticItem[] Map
        {
            get
            {
                return map;
            }
        }

        public IPlayer MyPlayer
        {
            get
            {
                return myPlayer;
            }
        }

        public IEnumerable<IBaseItem> AllItems
        {
            get
            {
                return Items;
            }
        }

        public IEnumerable<IBaseItem> Players
        {
            get
            {
                if (playerChanged)
                {
                    playerCache = Items.Where(x => x.GetType() == typeof(Player));
                    playerChanged = false;
                }
                return playerCache;
            }
        }

        public IEnumerable<IBaseItem> Bullets
        {
            get
            {
                if (bulletChanged)
                {
                    bulletCache = Items.Where(x => x.GetType() == typeof(Bullet));
                    bulletChanged = false;
                }
                return bulletCache;
            }
        }

        public IEnumerable<IBaseItem> Colliders
        {
            get
            {
                if (colliderChanged)
                {
                    colliderCache = Items.Where(x => x.Impact);
                    colliderChanged = false;
                }
                return colliderCache;
            }
        }

        public IEnumerable<IBaseItem> Statics
        {
            get
            {
                if (staticChanged)
                {
                    staticCache = Items.Where(x => x.GetType() == typeof(StaticItem));
                    staticChanged = false;
                }
                return staticCache;
            }
        }

        public IEnumerable<IBaseItem> Weapons
        {
            get
            {
                if (weaponChanged)
                {
                    weaponCache = Items.Where(x => x.GetType() == typeof(Weapon));
                    weaponChanged = false;
                }
                return weaponCache;
            }
        }

        public void ConstructItem(IBaseItem item)
        {
            if (!Items.Contains(item))
            {
                Items.Add(item);
                var bounds = item.RealPrimitive.Bounds;
                int width = (int)bounds.Width;
                int height = (int)bounds.Height;
                int x = (int)bounds.X + width;
                int y = (int)bounds.Y + height;
                ItemsChanged();
            }
        }

        [Obsolete]
        public void UpdateItem(Guid id, double xMove, double yMove, double width, double height, double rotation)
        {
            var items = Items.Where(x => x.Id == id);
            if (items.Count() > 0)
            {
                var item = items.First();
                item.Position = new Vector(xMove, yMove);
            }
        }

        public void DestructItem(Guid id)
        {
            var items = Items.Where(x => x.Id == id);
            if (items.Count() == 1)
            {
                IBaseItem item = items.First();
                Items.Remove(item);
                item = null;
                ItemsChanged();
            }
        }
        
        private IEnumerable<IBaseItem> staticCache;

        private bool staticChanged;

        private IEnumerable<IBaseItem> colliderCache;

        private bool colliderChanged;

        private IEnumerable<IBaseItem> bulletCache;

        private bool bulletChanged;

        private IEnumerable<IBaseItem> playerCache;

        private bool playerChanged;
        private bool weaponChanged;
        private IEnumerable<IBaseItem> weaponCache;

        private void LoadItems()
        {
            XDocument xDoc = XDocument.Load("Map.xml");
            foreach (var node in xDoc.Root.Descendants("item"))
            {
                string type = node.Attribute("type").Value;
                double x = double.Parse(node.Element("x").Value.Replace('.', ','));
                double y = double.Parse(node.Element("y").Value.Replace('.', ','));
                double width = double.Parse(node.Element("width").Value.Replace('.', ','));
                double height = double.Parse(node.Element("height").Value.Replace('.',','));
                double angle = double.Parse(node.Element("angle").Value);
                bool impact = bool.Parse(node.Element("impact").Value);
                Guid id = Guid.Parse(node.Attribute("id").Value);
                ConstructItem(new StaticItem(new RectangleGeometry() { Rect = new System.Windows.Rect(0, 0, width, height) }, id, new double[] { x, y }, angle, new double[] { width, height }, impact, type));
            }
        }
    }
}
