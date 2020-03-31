using System;
using obServer.Model.GameModel.Item;
using System.Collections.Generic;
using System.Linq;
using obServer.Model.Performance;
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
            IPlayer player = new Player(Player.PlayerGeometry, Guid.NewGuid(), new double[2] { 20, 20 }, 0, true, 100);
            Items = new List<IBaseItem>();
            info = new MapInformation(width, height);
            ConstructItem(player);
            map = new StaticItem(StaticGeometry(width, height), Guid.NewGuid(), new double[] { 0, 0}, 0, new double[] { width, height}, false, "Map");
            myPlayer = player;
            LoadItems(width, height);
        }

        private IPlayer myPlayer;

        private List<IBaseItem> Items;

        private IStaticItem map;

        private MapInformation info;

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
        public  IStaticItem Map
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
                info.Add(item.Id, x, y, width, height);
                ItemsChanged();
            }
            else
            {
                info.Del(item.Id);
                var bounds = item.RealPrimitive.Bounds;
                info.Add(item.Id, (int)item.Position[0], (int)item.Position[1], (int)bounds.Width, (int)bounds.Height);
            }
        }

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
                info.Del(item.Id);
                ItemsChanged();
            }
        }

        public IEnumerable<Guid> GetCloseItems(Guid id)
        {
            return info.Collision(id);
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
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                double dim = r.Next(50, 100);
                double xPos = r.Next(0, xMax - 2000);
                double yPos = r.Next(0, yMax - 2000);
                IBaseItem item = new StaticItem(StaticGeometry(dim,dim), Guid.NewGuid(), new double[] { xPos, yPos }, 0, new double[] { dim, dim }, true, "Crate");
                ConstructItem(item);
            }
            for (int i = 0; i < 10; i++)
            {
                double dim = r.Next(70, 120);
                double xPos = r.Next(0, xMax - 2000);
                double yPos = r.Next(0, yMax - 2000);
                IBaseItem item = new StaticItem(StaticGeometry(dim, dim), Guid.NewGuid(), new double[] { xPos, yPos }, 0, new double[] { dim, dim }, true, "Wall");
                ConstructItem(item);
            }
            for (int i = 0; i < 10; i++)
            {
                double dim = r.Next(100, 200);
                double xPos = r.Next(0, xMax - 2000);
                double yPos = r.Next(0, yMax - 2000);
                IBaseItem item = new StaticItem(StaticGeometry(dim, dim), Guid.NewGuid(), new double[] { xPos, yPos }, 0, new double[] { dim, dim }, true, "Bush");
                ConstructItem(item);
            }
        }
    }
}
