using System;
using obServer.Model.GameModel.Item;
using System.Collections.Generic;
using System.Linq;
using obServer.Model.Performance;
using obServer.Model.Interfaces;

namespace obServer.Model.GameModel
{
    public sealed class obServerModel :IobServerModel
    {
        public obServerModel()
        {
            IPlayer player = new Player(Player.PlayerGeometry, Guid.NewGuid(), new double[2] { 20, 20 }, 0, true, 100);
            Items = new List<IBaseItem>();
            ConstructItem(player);
            myPlayer = player;
        }

        private IPlayer myPlayer;

        private List<IBaseItem> Items;

        private MapInformation info;

        private bool itemsChanged
        {
            get { return itemsChanged; }
            set
            {
                staticChanged = value;
                playerChanged = value;
                colliderChanged = value;
                bulletChanged = value;
                weaponChanged = value;
                itemsChanged = value;
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

        public IEnumerable<IPlayer> Players
        {
            get
            {
                if (playerChanged)
                {
                    playerCache = (IEnumerable<IPlayer>)Items.Where(x => x.GetType() == typeof(Player));
                    playerChanged = false;
                }
                return playerCache;
            }
        }

        public IEnumerable<IBullet> Bullets
        {
            get
            {
                if (bulletChanged)
                {
                    bulletCache = (IEnumerable<IBullet>)Items.Where(x => x.GetType() == typeof(Bullet));
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

        public IEnumerable<IStaticItem> Statics
        {
            get
            {
                if (staticChanged)
                {
                    staticCache = (IEnumerable<IStaticItem>)Items.Where(x => x.GetType() == typeof(StaticItem));
                    staticChanged = false;
                }
                return staticCache;
            }
        }

        public IEnumerable<IWeapon> Weapons
        {
            get
            {
                if (weaponChanged)
                {
                    weaponCache = (IEnumerable<IWeapon>)Items.Where(x => x.GetType() == typeof(Weapon));
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
                info.Add(item.Id, (int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height);
                itemsChanged = true;
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
                itemsChanged = true;
            }
        }

        public IEnumerable<Guid> GetCloseItems(Guid id)
        {
            return info.Collision(id);
        }
        
        private IEnumerable<IStaticItem> staticCache;

        private bool staticChanged;

        private IEnumerable<IBaseItem> colliderCache;

        private bool colliderChanged;

        private IEnumerable<IBullet> bulletCache;

        private bool bulletChanged;

        private IEnumerable<IPlayer> playerCache;

        private bool playerChanged;
        private bool weaponChanged;
        private IEnumerable<IWeapon> weaponCache;
    }
}
