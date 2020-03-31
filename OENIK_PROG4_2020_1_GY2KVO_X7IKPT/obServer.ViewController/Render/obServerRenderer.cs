using obServer.Model.Interfaces;
using System;
using System.Collections.Generic;
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
        }

        private static ImageBrush playerBrush = new ImageBrush(new BitmapImage(new Uri("C:\\Users\\hp\\Desktop\\textures\\player.png")));
        private static ImageBrush crateBrush = new ImageBrush(new BitmapImage(new Uri("C:\\Users\\hp\\Desktop\\textures\\crate.png")));
        private static ImageBrush logBrush = new ImageBrush(new BitmapImage(new Uri("C:\\Users\\hp\\Desktop\\textures\\log.png")));
        private static ImageBrush weaponBrush = new ImageBrush(new BitmapImage(new Uri("C:\\Users\\hp\\Desktop\\textures\\log.png")));
        private static ImageBrush mapBrush = new ImageBrush(new BitmapImage(new Uri("C:\\Users\\hp\\Desktop\\textures\\hugemap.png")));
        private static ImageBrush wallBrush = new ImageBrush(new BitmapImage(new Uri("C:\\Users\\hp\\Desktop\\textures\\wall.png")));
        private static ImageBrush bushBrush = new ImageBrush(new BitmapImage(new Uri("C:\\Users\\hp\\Desktop\\textures\\bush.png")));
        private static Pen blackBorder = new Pen(Brushes.Black, 2);
        private static SolidColorBrush bulletBrush = Brushes.DarkGray;
        private static SolidColorBrush mapEndBrush = Brushes.DarkOliveGreen;

        internal void DrawElements(System.Windows.Media.DrawingContext context)
        {
            DrawingGroup drawings = new DrawingGroup();

            GeometryDrawing map = new GeometryDrawing(mapBrush, blackBorder, Model.Map.RealPrimitive);
            drawings.Children.Add(map);
            foreach (var Player in Model.Players) 
            {
                GeometryDrawing player = new GeometryDrawing(playerBrush, null , Player.RealPrimitive);
                drawings.Children.Add(player);
            }

            foreach (var Weapon in Model.Weapons)
            {
                
                GeometryDrawing weapon = new GeometryDrawing(weaponBrush, blackBorder, Weapon.RealPrimitive);
                drawings.Children.Add(weapon);
            }
            foreach (var Bullet in Model.Bullets)
            {
                GeometryDrawing bullet = new GeometryDrawing(bulletBrush, blackBorder, Bullet.RealPrimitive);
                drawings.Children.Add(bullet);
            }

            foreach (var Static in Model.Statics)
            {
                switch ((Static as IStaticItem).Type)
                {
                    case "Map":
                        GeometryDrawing MAP = new GeometryDrawing(mapBrush, blackBorder, Static.RealPrimitive);
                        drawings.Children.Add(MAP);
                        break;
                    case "Wall":
                        GeometryDrawing WALL = new GeometryDrawing(wallBrush, blackBorder, Static.RealPrimitive);
                        drawings.Children.Add(WALL);
                        break;
                    case "Crate":
                        GeometryDrawing CRATE = new GeometryDrawing(crateBrush, blackBorder, Static.RealPrimitive);
                        drawings.Children.Add(CRATE);
                        break;
                    case "Log":
                        GeometryDrawing LOG = new GeometryDrawing(logBrush, blackBorder, Static.RealPrimitive);
                        drawings.Children.Add(LOG);
                        break;
                    case "Bush":
                        GeometryDrawing BUSH = new GeometryDrawing(bushBrush, null, Static.RealPrimitive);
                        drawings.Children.Add(BUSH);
                        break;
                    default:
                        break;
                }
            }
            context.DrawDrawing(drawings);

            //GeometryDrawing background = new GeometryDrawing(Config.BgColor,
            //    new Pen(Config.BorderColor, Config.BorderSize),
            //    new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
            //GeometryDrawing ball = new GeometryDrawing(Config.BallBg,
            //    new Pen(Config.BallLine, 1),
            //    new EllipseGeometry(model.Ball.Area));
            //GeometryDrawing pad = new GeometryDrawing(Config.PadBg,
            //    new Pen(Config.PadLine, 1),
            //    new RectangleGeometry(model.Pad.Area));
            //FormattedText formattedText = new FormattedText(model.Errors.ToString(),
            //    System.Globalization.CultureInfo.CurrentCulture,
            //    FlowDirection.LeftToRight,
            //    new Typeface("Arial"),
            //    16,
            //    Brushes.Black);
            //GeometryDrawing text = new GeometryDrawing(null, new Pen(Brushes.Red, 2),
            //    formattedText.BuildGeometry(new Point(5, 5)));

            //dg.Children.Add(background);
            //dg.Children.Add(ball);
            //dg.Children.Add(pad);
            //dg.Children.Add(text);

            //foreach (Star star in model.Stars)
            //{
            //    GeometryDrawing starGeo = new GeometryDrawing(Config.BallBg, new Pen(Config.BallLine, 1),
            //        star.GetGeometry());
            //    dg.Children.Add(starGeo);
            //}
        }
    }
}
