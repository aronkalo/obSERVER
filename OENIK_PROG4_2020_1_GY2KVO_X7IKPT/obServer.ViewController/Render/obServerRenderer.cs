using obServer.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace obServer.ViewController.Render
{
    class obServerRenderer
    {
        private IobServerModel Model;

        public obServerRenderer(IobServerModel model)
        {
            this.Model = model;
            DrawStatic();
        }

        private double width;
        private double height;
        private double scaleDelta = 1;

        public void SetOffsets(double width, double height)
        {
            this.width = width;
            this.height = height;
        }


        private Stopwatch sw = new Stopwatch();
        private static string directory = Directory.GetCurrentDirectory();
        private static ImageBrush playerBrush = new ImageBrush(new BitmapImage(new Uri(directory  +  "\\textures\\player.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush crateBrush = new ImageBrush(new BitmapImage(new Uri(directory  +  "\\textures\\crate.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush logBrush = new ImageBrush(new BitmapImage(new Uri(directory  +  "\\textures\\log.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush weaponBrush = new ImageBrush(new BitmapImage(new Uri(directory  +  "\\textures\\log.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush mapBrush = new ImageBrush(new BitmapImage(new Uri(directory  +  "\\textures\\hugemap.png")) {  CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush wallBrush = new ImageBrush(new BitmapImage(new Uri(directory  +  "\\textures\\wall.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush bushBrush = new ImageBrush(new BitmapImage(new Uri(directory  +  "\\textures\\bush.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush redTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory  +  "\\textures\\redtree.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush greenTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory  +  "\\textures\\greentree.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush roundTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory  +  "\\textures\\roundtree.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static Pen blackBorder = new Pen(Brushes.Black, 2);
        private static SolidColorBrush bulletBrush = Brushes.DarkGray;
        private static SolidColorBrush mapEndBrush = Brushes.DarkOliveGreen;
        private DrawingGroup cache;


        internal void DrawElements(DrawingContext context)
        {
            sw.Restart();
            DrawStatic();
            DrawingGroup dGroup = new DrawingGroup();
            TransformGroup tg = new TransformGroup();
            tg.Children.Add(new TranslateTransform(-(Model.MyPlayer.Position[0] - width), -(Model.MyPlayer.Position[1] - height)));
            tg.Children.Add(new ScaleTransform(scaleDelta, scaleDelta));
            dGroup.Transform = tg;
            foreach (var item in Model.Map)
            {
                GeometryDrawing map = new GeometryDrawing(mapBrush, null, item.RealPrimitive);
                dGroup.Children.Add(map);
            }
            foreach (var Player in Model.Players) 
            {
                var p = Player.RealPrimitive;
                var t = playerBrush;
                t.Transform = p.Transform;
                GeometryDrawing player = new GeometryDrawing(t, null , p);
                dGroup.Children.Add(player);
            }

            foreach (var Weapon in Model.Weapons)
            {
                
                GeometryDrawing weapon = new GeometryDrawing(weaponBrush, blackBorder, Weapon.RealPrimitive);
                dGroup.Children.Add(weapon);
            }
            foreach (var Bullet in Model.Bullets)
            {
                GeometryDrawing bullet = new GeometryDrawing(bulletBrush, blackBorder, Bullet.RealPrimitive);
                dGroup.Children.Add(bullet);
            }
            foreach (var Static in cache.Children)
            {
                dGroup.Children.Insert(dGroup.Children.Count, Static);
            }
            context.DrawDrawing(dGroup);
            Debug.WriteLine("Render: " + sw.Elapsed.TotalSeconds);
            GC.Collect();
        }

        public void DrawStatic()
        {
            cache = new DrawingGroup();
            foreach (var Static in Model.Statics)
            {
                switch ((Static as IStaticItem).Type)
                {
                    case "Map":
                        GeometryDrawing MAP = new GeometryDrawing(mapBrush, blackBorder, Static.RealPrimitive);
                        cache.Children.Add(MAP);
                        break;
                    case "Wall":
                        GeometryDrawing WALL = new GeometryDrawing(wallBrush, blackBorder, Static.RealPrimitive);
                        cache.Children.Add(WALL);
                        break;
                    case "Crate":
                        GeometryDrawing CRATE = new GeometryDrawing(crateBrush, blackBorder, Static.RealPrimitive);
                        cache.Children.Add(CRATE);
                        break;
                    case "Log":
                        GeometryDrawing LOG = new GeometryDrawing(logBrush, blackBorder, Static.RealPrimitive);
                        cache.Children.Add(LOG);
                        break;
                    case "Bush":
                        GeometryDrawing BUSH = new GeometryDrawing(bushBrush, null, Static.RealPrimitive);
                        cache.Children.Add(BUSH);
                        break;
                    case "RedTree":
                        GeometryDrawing REDTREE = new GeometryDrawing(redTreeBrush, null, Static.RealPrimitive);
                        cache.Children.Add(REDTREE);
                        break;
                    case "GreenTree":
                        GeometryDrawing GREENTREE = new GeometryDrawing(greenTreeBrush, null, Static.RealPrimitive);
                        cache.Children.Add(GREENTREE);
                        break;
                    case "RoundTree":
                        GeometryDrawing ROUNDTREE = new GeometryDrawing(roundTreeBrush, null, Static.RealPrimitive);
                        cache.Children.Add(ROUNDTREE);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
