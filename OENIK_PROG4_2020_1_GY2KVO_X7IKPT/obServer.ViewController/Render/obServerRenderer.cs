using obServer.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace obServer.ViewController.Render
{
    class obServerRenderer
    {
        private static string directory = Directory.GetCurrentDirectory();
        private static ImageBrush playerBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\player.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush crateBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\crate.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush logBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\log.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush weaponBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\log.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush mapBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\hugemap.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush wallBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\wall.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush bushBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\bush.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush redTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\redtree.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush greenTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\greentree.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush roundTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\roundtree.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static Pen blackBorder = new Pen(Brushes.Black, 2);
        private static SolidColorBrush bulletBrush = Brushes.DarkGray;
        private static SolidColorBrush mapEndBrush = Brushes.DarkOliveGreen;

        public obServerRenderer(IobServerModel model)
        {
            this.Model = model;
            DrawStatic();
        }

        private DrawingGroup cache;
        private IobServerModel Model;
        private double width;
        private double height;

        public void SetOffsets(double width, double height)
        {
            this.width = width;
            this.height = height;
        }



        internal void DrawElements(DrawingContext context)
        {
            //DrawStatic();
            DrawingGroup dGroup = new DrawingGroup();
            TransformGroup tg = new TransformGroup();
            tg.Children.Add(new TranslateTransform(-(Model.MyPlayer.Position[0] - width), -(Model.MyPlayer.Position[1] - height)));
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

            dGroup.Children.Add(DrawHealthBar());
            context.DrawDrawing(dGroup);
            GC.Collect();
        }

        public GeometryDrawing DrawHealthBar()
        {
            var healthdrBrush = new DrawingBrush();
            var dg = new DrawingGroup();
            dg.Children.Add(new GeometryDrawing(Brushes.IndianRed,null,new RectangleGeometry(new Rect(0, 0, Model.MyPlayer.Health * 2, 30))));
            dg.Children.Add(new GeometryDrawing(Brushes.Transparent,null,new RectangleGeometry(new Rect(0, 0, 200, 30))));
            dg.Children.Add(new GeometryDrawing(Brushes.White, new Pen(Brushes.White,3), new FormattedText(Model.MyPlayer.Health.ToString(),CultureInfo.CurrentCulture,FlowDirection.LeftToRight, new Typeface("Arial"),20,Brushes.White, 1.25).BuildGeometry(new Point(86, 3))));
            //var healthBrush = new LinearGradientBrush(new GradientStopCollection() { new GradientStop(Colors.Red, 0), new GradientStop(Colors.Red, Model.MyPlayer.Health / 100), new GradientStop(Colors.Transparent, Model.MyPlayer.Health/100)}, new Point(0,0),new Point(1,0));
            healthdrBrush.Drawing = dg;
            healthdrBrush.Opacity = 0.8;
            return new GeometryDrawing(healthdrBrush, blackBorder, new RectangleGeometry() { Rect = new Rect(Model.MyPlayer.Position[0] - width + 20, Model.MyPlayer.Position[1] + height - 50, 200, 30)});
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
