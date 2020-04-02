using System;
using obServer.Model.GameModel.Item;
using System.Collections.Generic;
using System.Linq;
using obServer.Model.Interfaces;
using System.Xml.Linq;
using System.Windows.Media;

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
            IPlayer player = new Player(Player.PlayerGeometry, Guid.NewGuid(), new double[2] { 300, 300 }, 0, true, 100);
            Items = new List<IBaseItem>();
            //info = new MapInformation(width, height);
            ConstructItem(player);
            LoadMap(width, height);

            myPlayer = player;
            LoadItems(width, height);
        }

        private void LoadMap(int width,int height)
        {
            map = new IStaticItem[9];
            map[0] = new StaticItem(StaticGeometry(width, height), Guid.NewGuid(), new double[] { width * -1, height * -1 }, 0, new double[] { width, height }, false, "Map");
            map[1] = new StaticItem(StaticGeometry(width, height), Guid.NewGuid(), new double[] { width * -1, height * 0 }, 0, new double[] { width, height }, false, "Map");
            map[2] = new StaticItem(StaticGeometry(width, height), Guid.NewGuid(), new double[] { width * 0, height * -1 }, 0, new double[] { width, height }, false, "Map");
            map[3] = new StaticItem(StaticGeometry(width, height), Guid.NewGuid(), new double[] { width * 0, height * 0 }, 0, new double[] { width, height }, false, "Map");
            map[4] = new StaticItem(StaticGeometry(width, height), Guid.NewGuid(), new double[] { width * 1, height * 0 }, 0, new double[] { width, height }, false, "Map");
            map[5] = new StaticItem(StaticGeometry(width, height), Guid.NewGuid(), new double[] { width * 0, height * 1 }, 0, new double[] { width, height }, false, "Map");
            map[6] = new StaticItem(StaticGeometry(width, height), Guid.NewGuid(), new double[] { width * 1, height * 1 }, 1, new double[] { width, height }, false, "Map");
            map[7] = new StaticItem(StaticGeometry(width, height), Guid.NewGuid(), new double[] { width * -1, height * 1 }, 0, new double[] { width, height }, false, "Map");
            map[8] = new StaticItem(StaticGeometry(width, height), Guid.NewGuid(), new double[] { width * 1, height * -1 }, 0, new double[] { width, height }, false, "Map");
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
                item.Position = new double[] { xMove, yMove };
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

        public IEnumerable<IBaseItem> GetCloseItems(Guid id)
        {
            var items = Items.Where(x => x.Id == id);
            if (items.Count() == 1)
            {
                var item = items.First();
                var bound = item.RealPrimitive.Bounds;
                return Items.Where(x => x.Impact && x.RealPrimitive.Bounds.Contains(bound));
            }
            return null;
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

        private void LoadItems(int xMax, int yMax)
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
                Guid id = Guid.Parse(node.Attribute("id").Value);
                ConstructItem(new StaticItem(new RectangleGeometry() { Rect = new System.Windows.Rect(0, 0, width, height) }, id, new double[] { x, y }, angle, new double[] { width, height }, false, type));
            }
        }
    }
}
