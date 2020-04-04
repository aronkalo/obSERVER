using obServer.Model.GameModel.ServerSide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace obServer.Model.GameModel
{
    public class ServerSideModel
    {
        public ServerSideModel()
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

        public void ConstructItem(Guid id, string type, Rect bounds, bool impact)
        {
            ServerItem item = new ServerItem() { Bounds = bounds, Type = type, Id = id, Impact = impact };
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

        public void LoadMap()
        {
            XDocument xDoc = System.Xml.Linq.XDocument.Load("Map.xml");
            foreach (var node in xDoc.Root.Descendants("item"))
            {
                string type = node.Attribute("type").Value;
                double x = double.Parse(node.Element("x").Value.Replace('.', ','));
                double y = double.Parse(node.Element("y").Value.Replace('.', ','));
                double width = double.Parse(node.Element("width").Value.Replace('.', ','));
                double height = double.Parse(node.Element("height").Value.Replace('.', ','));
                bool impact = bool.Parse(node.Element("impact").Value);
                Guid id = Guid.Parse(node.Attribute("id").Value);
                ConstructItem(id, type, new Rect(x,y,width, height));
            }
        }
    }
}
