using obServer.Model.GameModel.ServerSide;
using obServer.Model.Performance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace obServer.Model.GameModel
{
    public class ServerSideModel
    {
        public ServerSideModel(int width, int height)
        {
            Items = new List<ServerItem>();
        }
        private List<ServerItem> Items;

        public IEnumerable<ServerItem> AllItems
        {
            get
            {
                return Items;
            }
        }

        public IEnumerable<ServerItem> Players
        {
            get
            {
                return Items.Where(x => x.Type == "Player");
            }
        }

        public void ConstructItem(Guid id, string type, Rect bounds)
        {
            ServerItem item = new ServerItem() { Bounds = bounds, Type = type, Id = id, };
            if (Items.Contains(item))
            {
                Items.Remove(item);
            }
            Items.Add(item);
        }

        public void DestructItem(Guid id)
        {
            var items = Items.Where(x => x.Id == id);
            if (items.Count() == 1)
            {
                Items.Remove(items.First());
            }
        }

        public ServerItem[] BulletHit(Guid id)
        {
            var items = Items.Where(x => x.Id == id);
            if (items.Count() == 1)
            {
                var item = items.First();
                var bull = Items.Where(x => x.Type == "Bullet" && x.Bounds.Contains(item.Bounds));
                if (bull.Count() > 0)
                {
                    return bull.ToArray();
                }
                return null;
            }
            return null;
        }

        public ServerItem[] Collision(Guid id)
        {
            var items = Items.Where(x => x.Id == id);
            if (items.Count() == 1)
            {
                var item = items.First();
                var bull = Items.Where(x => x.Bounds.Contains(item.Bounds) && x.Type != "RedTree" && x.Type != "RoundTree" && x.Type != "GreenTree");
                if (bull.Count() > 0)
                {
                    return bull.ToArray();
                }
                return null;
            }
            return null;
        }
    }
}
