using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace obServer.ViewController.Render.Texture
{
    class BrushCache
    {
        public static string directory = Directory.GetCurrentDirectory();
        public ImageBrush playerBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\player.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush playerPistolBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\pistol.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush crateBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\crate.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush logBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\log.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush weaponBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\log.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush mapBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\hugemap.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush wallBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\wall.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush bushBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\bush.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush redTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\redtree.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush greenTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\greentree.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush roundTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\roundtree.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public Pen blackBorder = new Pen(Brushes.Black, 2);
        public SolidColorBrush bulletBrush = Brushes.DarkGray;
        public SolidColorBrush mapEndBrush = Brushes.DarkOliveGreen;
        public Brush DrawHealthBar(double health, double wid, double hei)
        {
            if (lastHealth != health)
            {
                healthBarBrush = new DrawingBrush();
                var dg = new DrawingGroup();
                dg.Children.Add(new GeometryDrawing(Brushes.MediumVioletRed, null, new RectangleGeometry(new Rect(0, 0, health * 2, 30))));
                dg.Children.Add(new GeometryDrawing(Brushes.Transparent, null, new RectangleGeometry(new Rect(0, 0, wid, hei))));
                dg.Children.Add(new GeometryDrawing(Brushes.White, new Pen(Brushes.White, 2), new FormattedText(health.ToString(),
                    CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 20, Brushes.White, 1.25).BuildGeometry(new Point(86, 3))));
                healthBarBrush.Drawing = dg;
                healthBarBrush.Opacity = 0.8;
                lastHealth = health;
            }
            return healthBarBrush;
        }
        private DrawingBrush healthBarBrush;
        private double lastHealth = -1;

        public Brush DrawStoredBullets(int count) 
        {
            if (lastBullets != count)
            {
                storedBulletBrush = new DrawingBrush();
                var dg = new DrawingGroup();
                dg.Children.Add(new GeometryDrawing(Brushes.Gray, new Pen(Brushes.LightGray, 2), new RectangleGeometry(new Rect(0, 0, 200, 120))));
                dg.Children.Add(new GeometryDrawing(bulletsBrush, null, new RectangleGeometry(new Rect(0, 0, 50, 50))));
                dg.Children.Add(new GeometryDrawing(Brushes.White, new Pen(Brushes.White, 2), new FormattedText(count.ToString(),
                    CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 20, Brushes.White, 1.25).BuildGeometry(new Point(20, 15))));
                storedBulletBrush.Drawing = dg;
                storedBulletBrush.Opacity = 0.8;
            }
            return storedBulletBrush;
        }
        private DrawingBrush storedBulletBrush;
        private ImageBrush bulletsBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\bullets.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private int lastBullets = -1;
    }
}
