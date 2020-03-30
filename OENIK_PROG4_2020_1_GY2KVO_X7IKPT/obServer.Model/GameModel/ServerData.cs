using obServer.Model.GameModel.ServerSide;
using obServer.Model.Performance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.Model.GameModel
{
    public class ServerData
    {
        public ServerData(int width, int height)
        {
            info = new MapInformation(width, height);
            Items = new List<ServerItem>();
        }
        private MapInformation info;
        private List<ServerItem> Items;

        public IEnumerable<ServerItem> AllItems
        {
            get
            {
                return Items;
            }
        } 

        public void ConstructItem(Guid id, string type, double[] position, double[] dimensions, double rotation)
        {
            ServerItem item = new ServerItem()
            {
                Dimensions = new double[] { dimensions[0], dimensions[1] },
                Position = new double[] { position[0], position[1] },
                Type = type,
                Rotation = rotation,
                Id = id,
            };
            if (Items.Contains(item))
            {
                Items.Remove(item);
                Items.Add(item);
                info.Del(id);
                info.Add(id, (int)position[0], (int)position[1], (int)dimensions[0], (int)dimensions[1]);
            }
            else
            {
                Items.Add(item);
                info.Add(id, (int)position[0], (int)position[1], (int)dimensions[0], (int)dimensions[1]);
            }
        }

        public void DestructItem(Guid id)
        {
            var items = Items.Where(x => x.Id == id);
            if (items.Count() == 1)
            {
                Items.Remove(items.First());
                info.Del(id);
            }
        }

        public ServerItem[] BulletHit(Guid id)
        {
            var items = Items.Where(x => x.Id == id);
            if (items.Count() == 1)
            {
                var it = info.Collision(id);
                var bull = Items.Where(x => it.Contains(x.Id) && x.Type == "Bullet");
                if (bull.Count() > 0)
                {
                    return bull.ToArray();
                }
                return new ServerItem[0];
            }
            return new ServerItem[0];
        }

        public ServerItem[] WeaponCollide(Guid id)
        {
            var items = Items.Where(x => x.Id == id);
            if (items.Count() == 1)
            {
                var it = info.Collision(id);
                var bull = Items.Where(x => it.Contains(x.Id) && x.Type == "Weapon");
                if (bull.Count() > 0)
                {
                    return bull.ToArray();
                }
                return new ServerItem[0];
            }
            return new ServerItem[0];
        }
    }
}
