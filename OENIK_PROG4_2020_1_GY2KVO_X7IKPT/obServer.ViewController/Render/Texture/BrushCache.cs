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
        public ImageBrush cloudFirstBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\cloud1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush cloudSecondBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\cloud2.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush dirtFirstBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\dirt1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush dirtSecondBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\dirt2.png")) { CacheOption = BitmapCacheOption.OnLoad });
       
        public ImageBrush playerBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\player.png")) { CacheOption = BitmapCacheOption.OnLoad });
        //public ImageBrush playerPistolBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\pistol.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush crateBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\crate.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush logBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\log.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush weaponBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\log.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush mapBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\hugemap.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush wallBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\wall.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush bushBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\bush.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush redTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\redtree.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush greenTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\greentree.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush roundTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\roundtree.png")) { CacheOption = BitmapCacheOption.OnLoad });
        //public static FontFamily karmaFont = new FontFamily(new Uri(directory + "\\karma\\tt\\karma_future.ttf"), "Karma Future");
        //public Typeface karmaTypeface = new Typeface(karmaFont, karmaFont.FamilyTypefaces.First().Style, FontWeights.Normal, karmaFont.FamilyTypefaces.First().Stretch, karmaFont);

        public ImageBrush dirtyRoadBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\dirtyroad.png")) { CacheOption = BitmapCacheOption.OnLoad });

        public ImageBrush graveYardArrowBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveyardArrowSign.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush graveYardSignBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveyardSign.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush graveWall1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveWall1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush graveWall3Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveWall3.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush graveWall5Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveWall5.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush graveWall7Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveWall7.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush graveFloorBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveFloor.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush graveDirtBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveDirt.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush graveBone1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Bone1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush graveBone2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Bone3.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush graveTomb1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\TombStone1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush graveTomb2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\TombStone2.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush graveSkeletonBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Skeleton.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush graveRoofBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveRoof.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Tree1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Tree1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Tree2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Tree2.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Tree3Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Tree3.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Flower1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\flower1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Flower2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\flower2.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush WellBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Well.png")) { CacheOption = BitmapCacheOption.OnLoad });

        public ImageBrush ForestChairBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Chair.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush ForestBuildingBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\ForestBuilding.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush ForestChairFlippedBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\ChairFlipped.png")) { CacheOption = BitmapCacheOption.OnLoad });

        //public ImageBrush Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\ChairFlipped.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush MazeBushBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\MazeBush.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush PacmanBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Pacman.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush PacmanGhostBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\PacmanGhost.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush FerrariBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Ferrari.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush MazeChairBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\MazeFotel.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush MazeTVBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\MazeTv.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush MazeArtifactBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Artifact.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush DesertBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Desert.png")) { CacheOption = BitmapCacheOption.OnLoad });

        public ImageBrush BlitzBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Blitz.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush EaglesBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Eagles.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Cliff1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Cliff1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Cliff2rush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Cliff2.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush NFLBallBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\NFLBall.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Rock1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Rock1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Rock2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Rock2.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Rock3Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Rock3.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Rock4Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Rock4.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush StargateBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Stargate.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Car1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Car1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Car2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Car2.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Car3Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Car3.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Car4Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Car4.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Car5Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Car5.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Cactus1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Cactus01.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Cactus2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Cactus02.png")) { CacheOption = BitmapCacheOption.OnLoad });
        public ImageBrush Cactus3Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Cactus03.png")) { CacheOption = BitmapCacheOption.OnLoad });


        public Typeface officialFont = new Typeface("Colonna MT");
        public Pen blackBorder = new Pen(Brushes.Black, 2);
        public SolidColorBrush bulletBrush = Brushes.DarkGray;
        public SolidColorBrush mapEndBrush = Brushes.DarkOliveGreen;
        public Brush DrawHealthBar(double health, double wid, double hei)
        {
            if (lastHealth != health)
            {
                healthBarBrush = new DrawingBrush();
                var dg = new DrawingGroup();
                if (health > 0)
                {
                    dg.Children.Add(new GeometryDrawing(Brushes.MediumVioletRed, null, new RectangleGeometry(new Rect(0, 0, health * 2, 30))));
                }
                dg.Children.Add(new GeometryDrawing(Brushes.Transparent, null, new RectangleGeometry(new Rect(0, 0, wid, hei))));
                dg.Children.Add(new GeometryDrawing(DrawText(Brushes.Transparent, health.ToString(), 25, new Rect(0, 0, wid, hei),
                    Brushes.WhiteSmoke, officialFont, blackBorder, new Pen(Brushes.WhiteSmoke, 2), 1),null, new RectangleGeometry(new Rect(0, 0, wid, hei))));
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
                //dg.Children.Add(new GeometryDrawing(bulletsBrush, null, new RectangleGeometry(new Rect(0, 0, 50, 50))));
                dg.Children.Add(new GeometryDrawing(Brushes.White, new Pen(Brushes.White, 2), new FormattedText(count.ToString(),
                    CultureInfo.CurrentCulture, FlowDirection.LeftToRight, officialFont, 20, Brushes.White, 1.25).BuildGeometry(new Point(20, 15))));
                storedBulletBrush.Drawing = dg;
                storedBulletBrush.Opacity = 0.8;
            }
            return storedBulletBrush;
        }

        public Brush DrawText(SolidColorBrush bg, string text, int fontSize, Rect panel, SolidColorBrush fontColor, Typeface fontTypeface, Pen panelBorder = null, Pen fontBorder = null, double opacity = 1) 
        {
            var dg = new DrawingGroup();
            int chars = text.Length;
            double yalign = panel.Height/2 - (fontSize / 2);
            double xalign = panel.Width /2 - (chars * fontSize * 0.6 * 0.5);
            dg.Children.Add(new GeometryDrawing(bg, panelBorder, new RectangleGeometry(panel)));
            dg.Children.Add(new GeometryDrawing(fontColor, fontBorder, new FormattedText(text,
                    CultureInfo.CurrentCulture, FlowDirection.LeftToRight, fontTypeface, fontSize, fontColor, 1.25).BuildGeometry(new Point(xalign, yalign))));
            DrawingBrush textBrush = new DrawingBrush();
            textBrush.Drawing = dg;
            textBrush.Opacity = opacity;
            return textBrush;
        }
        private DrawingBrush storedBulletBrush;
        //private ImageBrush bulletsBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\bullets.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private int lastBullets = -1;
    }
}
