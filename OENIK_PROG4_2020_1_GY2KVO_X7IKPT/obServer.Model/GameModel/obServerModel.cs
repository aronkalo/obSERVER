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
            IPlayer player = new Player(Player.PlayerGeometry, Guid.NewGuid(), new double[2] { 2500, 2500 }, 0, true, 100);
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
                //info.Add(item.Id, x, y, width, height);
                ItemsChanged();
            }
            else
            {
                //info.Del(item.Id);
                var bounds = item.RealPrimitive.Bounds;
                //info.Add(item.Id, (int)item.Position[0], (int)item.Position[1], (int)bounds.Width, (int)bounds.Height);
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
                //info.Del(item.Id);
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

            //Random r = new Random();
            double d = 70;

            //IBaseItem gravealap = new StaticItem(StaticGeometry(xMax / 2.3, yMax / 2.3), Guid.NewGuid(), new double[] { xMax / 2 + 210, 70}, 0, new double[] { d, d }, true, "GraveDirt");
            //ConstructItem(gravealap);

            //IBaseItem DesertAlap = new StaticItem(StaticGeometry(xMax / 2 - 300, yMax / 2 - 300), Guid.NewGuid(), new double[] { 70, yMax / 2 + 210}, 0, new double[] { d, d }, true, "Desert");
            //ConstructItem(DesertAlap);

            ////függőleges főút
            //for (int y = 0; y < yMax - 70; y += (int)d)
            //{
            //    IBaseItem start = new StaticItem(StaticGeometry(d, d), Guid.NewGuid(), new double[] { xMax / 2 + 10, y }, 0, new double[] { d, d }, true, "DirtyRoad");
            //    ConstructItem(start);
            //}

            ////Vízszintes főút
            //for (int x = 0; x < xMax - 70; x += (int)d)
            //{
            //    IBaseItem start = new StaticItem(StaticGeometry(d, d), Guid.NewGuid(), new double[] { x, yMax / 2 + 20}, 0, new double[] { d, d }, true, "DirtyRoad");
            //    ConstructItem(start);
            //}


            ////függőleges Graveyard falak
            //for (int y = 140; y < yMax - (yMax / 2) - 490; y += (int)d)
            //{
            //    //bal falak
            //    IBaseItem start = new StaticItem(StaticGeometry(d, d), Guid.NewGuid(), new double[] { xMax / 2 + 350, y }, 0, new double[] { d, d }, true, "GraveWall3");
            //    ConstructItem(start);
            //    //jobb falak
            //    IBaseItem end = new StaticItem(StaticGeometry(d, d), Guid.NewGuid(), new double[] { xMax - 350, y }, 0, new double[] { d, d }, true, "GraveWall7");
            //    ConstructItem(end);
            //}

            ////vízszintes Graveyard falak
            //for (int x = xMax / 2 + 350; x < xMax - 280; x += (int)d)
            //{
            //    //alsó falak
            //    IBaseItem start = new StaticItem(StaticGeometry(d, d), Guid.NewGuid(), new double[] { x, yMax / 2 - 490 }, 0, new double[] { d, d }, true, "GraveWall1");
            //    ConstructItem(start);
            //    //felső falak
            //    IBaseItem end = new StaticItem(StaticGeometry(d, d), Guid.NewGuid(), new double[] { x, 0 + 140 }, 0, new double[] { d, d }, true, "GraveWall5");
            //    ConstructItem(end);
            //}

            //for (int y = 210; y < yMax - (yMax / 2); y += (int)d)
            //{
            //    //GraveYard utak függőleges
            //    IBaseItem gravef = new StaticItem(StaticGeometry(d, d), Guid.NewGuid(), new double[] { (xMax / 2) + (xMax / 4) + 10, y }, 0, new double[] { d, d }, true, "GraveFloor");
            //    ConstructItem(gravef);
            //}

            //for (int x = xMax / 2 + 70; x < xMax - 420; x += (int)d)
            //{
            //    //GraveYard utak vízszintes
            //    IBaseItem gravef = new StaticItem(StaticGeometry(d, d), Guid.NewGuid(), new double[] { x, 1400 }, 0, new double[] { d, d }, true, "GraveFloor");
            //    ConstructItem(gravef);
            //}

            //for (int x = xMax / 2 + 420; x < xMax - 420; x += (int)d)
            //{
            //    //GraveYard utak vízszintes
            //    IBaseItem gravef1 = new StaticItem(StaticGeometry(d, d), Guid.NewGuid(), new double[] { x, 560 }, 0, new double[] { d, d }, true, "GraveFloor");
            //    ConstructItem(gravef1);
            //    IBaseItem gravef2 = new StaticItem(StaticGeometry(d, d), Guid.NewGuid(), new double[] { x, 980 }, 0, new double[] { d, d }, true, "GraveFloor");
            //    ConstructItem(gravef2);
            //    IBaseItem gravef3 = new StaticItem(StaticGeometry(d, d), Guid.NewGuid(), new double[] { x, 1820 }, 0, new double[] { d, d }, true, "GraveFloor");
            //    ConstructItem(gravef3);
            //}

            //Maze

            ////Maze függőleges falak
            //for (int i = 0; i < xMax / 2 - 490; i+= 210)
            //{
            //    for (int y = yMax / 2 + 280; y < yMax - 210; y += (int)d)
            //    {                    
            //        IBaseItem mazewall = new StaticItem(StaticGeometry(d, d), Guid.NewGuid(), new double[] { xMax / 2 + 350 + i, y }, 0, new double[] { d, d }, true, "MazeBush");
            //        ConstructItem(mazewall);
            //    }
            //}

            ////Maze vízszintes falak
            //for (int i = 0; i < yMax / 2 - 280; i+= 210)
            //{
            //    int db = 0;
            //    for (int x = xMax / 2 + 350; x < xMax - 140; x += (int)d)
            //    {                    
            //        IBaseItem start = new StaticItem(StaticGeometry(d, d), Guid.NewGuid(), new double[] { x, yMax / 2 + 210 + i }, 0, new double[] { d, d }, true, "MazeBush");
            //        ConstructItem(start);
            //    }            
            //}

            //IBaseItem tree1 = new StaticItem(StaticGeometry(250, 250), Guid.NewGuid(), new double[] { 100, 150 }, 0, new double[] { 250, 250 }, true, "RedTree");
            //ConstructItem(tree1);
            //IBaseItem tree2 = new StaticItem(StaticGeometry(150, 150), Guid.NewGuid(), new double[] { 200, 270 }, 0, new double[] { 150, 150 }, true, "GreenTree");
            //ConstructItem(tree2);
            //IBaseItem tree3 = new StaticItem(StaticGeometry(250, 250), Guid.NewGuid(), new double[] { 350, 170 }, 0, new double[] { 250, 250 }, true, "RedTree");
            //ConstructItem(tree3);
            //IBaseItem tree4 = new StaticItem(StaticGeometry(300, 300), Guid.NewGuid(), new double[] { 400, 340 }, 0, new double[] { 400, 400 }, true, "GreenTree");
            //ConstructItem(tree4);
            //IBaseItem tree5 = new StaticItem(StaticGeometry(250, 250), Guid.NewGuid(), new double[] { 300, 400 }, 0, new double[] { 450, 450 }, true, "GreenTree");
            //ConstructItem(tree5);
        }
    }
}
