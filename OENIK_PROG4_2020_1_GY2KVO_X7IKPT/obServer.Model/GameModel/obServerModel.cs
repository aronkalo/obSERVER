using System;
using obServer.Model.GameModel.Item;
using System.Collections.Generic;
using System.Linq;

namespace obServer.Model.GameModel
{
    public sealed class obServerModel
    {
        public obServerModel()
        {
            Items = new List<BaseItem>();
        }

        private List<BaseItem> Items;

        private bool itemsChanged
        {
            get { return itemsChanged; }
            set
            {
                staticChanged = value;
                playerChanged = value;
                colliderChanged = value;
                bulletChanged = value;
                itemsChanged = value;
            }
        }

        public IEnumerable<Player> Players
        {
            get
            {
                if (playerChanged)
                {
                    playerCache = (IEnumerable<Player>)Items.Where(x => x.GetType() == typeof(Player));
                    playerChanged = false;
                }
                return playerCache;
            }
        }

        public IEnumerable<Bullet> Bullets
        {
            get
            {
                if (bulletChanged)
                {
                    bulletCache = (IEnumerable<Bullet>)Items.Where(x => x.GetType() == typeof(Player));
                    bulletChanged = false;
                }
                return bulletCache;
            }
        }

        public IEnumerable<BaseItem> Colliders
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

        public IEnumerable<StaticItem> Statics
        {
            get
            {
                if (staticChanged)
                {
                    staticCache = (IEnumerable<StaticItem>)Items.Where(x => x.GetType() == typeof(StaticItem));
                    staticChanged = false;
                }
                return staticCache;
            }
        }

        public void ConstructItem(BaseItem item)
        {
            if (!Items.Contains(item))
            {
                Items.Add(item);
                itemsChanged = true;
            }
        }

        public void DestructItem(Guid id)
        {
            var items = Items.Where(x => x.Id == id);
            if (items.Count() == 1)
            {
                BaseItem item = items.First();
                Items.Remove(item);
                item = null;
                itemsChanged = true;
            }
        }

        private IEnumerable<StaticItem> staticCache;

        private bool staticChanged;

        private IEnumerable<BaseItem> colliderCache;

        private bool colliderChanged;

        private IEnumerable<Bullet> bulletCache;

        private bool bulletChanged;

        private IEnumerable<Player> playerCache;

        private bool playerChanged;
    }
}
