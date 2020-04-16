using obServer.GameModel.Interfaces;
using obServer.ViewController.Render.Texture;
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
        public obServerRenderer(IObServerModel model)
        {
            this.model = model;
            drawingCache = new RenderCache();
            textureCache = new BrushCache();
            DrawStatic();
        }
        private Stopwatch sw = new Stopwatch();
        private RenderCache drawingCache;
        private BrushCache textureCache;
        private IObServerModel model;
        private double width;
        private double height;

        public void SetOffsets(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        internal GeometryDrawing DrawStartMenu()
        {
            DrawingGroup dGroup = new DrawingGroup();
            double realWidth = width * 2;
            double realHeight = height * 2;

            double controlXSize = realWidth - (realWidth * 0.6);
            double controlYSize = realHeight - (realHeight * 0.8);
            Rect r = new Rect(0, 0, 300, 200);
            return new GeometryDrawing(textureCache.DrawText(Brushes.Blue, "welcome", 120, r, Brushes.WhiteSmoke, textureCache.officialFont),
                null, new RectangleGeometry(r));
        }
        internal void DrawElements(DrawingContext context)
        {
            sw.Restart();
            DrawingGroup dGroup = new DrawingGroup();
            dGroup.Transform = new TranslateTransform(-(model.MyPlayer.Position.X - width), -(model.MyPlayer.Position.Y - height));
            dGroup.Children.Add(drawingCache.MapInstance);
            dGroup.Children.Add(drawingCache.GroundInstance);
            foreach (var Player in model.Players) 
            {
                var p = Player.RealPrimitive;
                var t = textureCache.playerBrush;
                if ((Player as IPlayer).CurrentWeapon != null)
                {
                    t = textureCache.playerPistolBrush;
                }
                t.Transform = p.Transform;
                GeometryDrawing player = new GeometryDrawing(t, null , p);
                dGroup.Children.Add(player);
            }
            foreach (var Weapon in model.Weapons)
            {
                GeometryDrawing weapon = new GeometryDrawing(null, textureCache.blackBorder, Weapon.RealPrimitive);
                dGroup.Children.Add(weapon);
            }
            foreach (var Bullet in model.Bullets)
            {
                GeometryDrawing bullet = new GeometryDrawing(textureCache.bulletBrush, textureCache.blackBorder, Bullet.RealPrimitive);
                dGroup.Children.Add(bullet);
            }
            drawingCache.MiddleCache.Opacity = 0.95;
            dGroup.Children.Add(drawingCache.MiddleInstance);
            dGroup.Children.Add(drawingCache.TopInstance);
            var clouds = drawingCache.AtmosphereInstance;
            clouds.Geometry.Transform = new TranslateTransform(-(model.MyPlayer.Position.X - width)/5, -(model.MyPlayer.Position.Y - height)/5);
            dGroup.Children.Add(clouds);
            dGroup.Children.Add(DrawHealthBar());
            context.DrawDrawing(dGroup);
            Debug.WriteLine(sw.Elapsed.TotalMilliseconds);
        }

        public GeometryDrawing DrawHealthBar()
        {
            double hwidth = 200;
            double hheight = 30;
            return new GeometryDrawing(textureCache.DrawHealthBar(model.MyPlayer.Health, hwidth, hheight), textureCache.blackBorder, new RectangleGeometry() { Rect = new Rect(model.MyPlayer.Position.X - width + 20, model.MyPlayer.Position.Y + height - 50, hwidth, hheight)});
        }

        public GeometryDrawing DrawBullets()
        {
            double hwidth = 200;
            double hheight = 120;
            return new GeometryDrawing(textureCache.DrawStoredBullets(model.MyPlayer.StoredBullets), null, new RectangleGeometry() { Rect = new Rect(model.MyPlayer.Position.X + width - 100 - hwidth, model.MyPlayer.Position.Y + height - 100, hwidth, hheight) });
        }

        public void DrawStatic()
        {
            foreach (var Static in model.Statics)
            {
                switch ((Static as IStaticItem).Type)
                {
                    case "Map":
                        GeometryDrawing MAP = new GeometryDrawing(textureCache.mapBrush, null, Static.RealPrimitive);
                        drawingCache.MapCache.Children.Add(MAP);
                        break;
                    case "Wall":
                        GeometryDrawing WALL = new GeometryDrawing(textureCache.wallBrush, textureCache.blackBorder, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(WALL);
                        break;
                    case "Crate":
                        GeometryDrawing CRATE = new GeometryDrawing(textureCache.crateBrush, textureCache.blackBorder, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(CRATE);
                        break;
                    case "Log":
                        GeometryDrawing LOG = new GeometryDrawing(textureCache.logBrush, textureCache.blackBorder, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(LOG);
                        break;
                    case "Bush":
                        GeometryDrawing BUSH = new GeometryDrawing(textureCache.bushBrush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(BUSH);
                        break;
                    case "RedTree":
                        GeometryDrawing REDTREE = new GeometryDrawing(textureCache.redTreeBrush, null, Static.RealPrimitive);
                        drawingCache.TopCache.Children.Add(REDTREE);
                        break;
                    case "GreenTree":
                        GeometryDrawing GREENTREE = new GeometryDrawing(textureCache.greenTreeBrush, null, Static.RealPrimitive);
                        drawingCache.TopCache.Children.Add(GREENTREE);
                        break;
                    case "RoundTree":
                        GeometryDrawing ROUNDTREE = new GeometryDrawing(textureCache.roundTreeBrush, null, Static.RealPrimitive);
                        drawingCache.TopCache.Children.Add(ROUNDTREE);
                        break;
                    case "GraveyardArrowSign":
                        GeometryDrawing GRAVEYARDARROWSIGN = new GeometryDrawing(textureCache.graveYardArrowBrush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(GRAVEYARDARROWSIGN);
                        break;
                    case "DirtyRoad":
                        textureCache.dirtyRoadBrush.Opacity = 0.8;
                        GeometryDrawing DIRTYROAD = new GeometryDrawing(textureCache.dirtyRoadBrush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(DIRTYROAD);
                        break;
                    case "GraveWall1":
                        GeometryDrawing GRAVEWALL1 = new GeometryDrawing(textureCache.graveWall1Brush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(GRAVEWALL1);
                        break;
                    case "GraveWall3":
                        GeometryDrawing GRAVEWALL3 = new GeometryDrawing(textureCache.graveWall3Brush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(GRAVEWALL3);
                        break;
                    case "GraveWall5":
                        GeometryDrawing GRAVEWALL5 = new GeometryDrawing(textureCache.graveWall5Brush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(GRAVEWALL5);
                        break;
                    case "GraveWall7":
                        GeometryDrawing GRAVEWALL7 = new GeometryDrawing(textureCache.graveWall7Brush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(GRAVEWALL7);
                        break;
                    case "GraveFloor":
                        GeometryDrawing GRAVEFLOOR = new GeometryDrawing(textureCache.graveFloorBrush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(GRAVEFLOOR);
                        break;
                    case "TombStone1":
                        GeometryDrawing TOMBSTONE1 = new GeometryDrawing(textureCache.graveTomb1Brush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(TOMBSTONE1);
                        break;
                    case "TombStone2":
                        GeometryDrawing TOMBSTONE2 = new GeometryDrawing(textureCache.graveTomb2Brush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(TOMBSTONE2);
                        break;
                    case "Bone1":
                        GeometryDrawing BONE1 = new GeometryDrawing(textureCache.graveBone1Brush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(BONE1);
                        break;
                    case "Bone2":
                        GeometryDrawing BONE2 = new GeometryDrawing(textureCache.graveBone2Brush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(BONE2);
                        break;
                    case "Skeleton":
                        GeometryDrawing SKELETON = new GeometryDrawing(textureCache.graveSkeletonBrush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(SKELETON);
                        break;
                    case "GraveDirt":
                        GeometryDrawing GRAVEDIRT = new GeometryDrawing(textureCache.graveDirtBrush, null, Static.RealPrimitive);
                        drawingCache.MapCache.Children.Add(GRAVEDIRT);
                        break;
                    case "GraveyardSign":
                        GeometryDrawing GRAVESIGN = new GeometryDrawing(textureCache.graveYardSignBrush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(GRAVESIGN);
                        break;
                    case "GraveRoof":
                        GeometryDrawing GRAVEROOF = new GeometryDrawing(textureCache.graveRoofBrush, null, Static.RealPrimitive);
                        drawingCache.TopCache.Children.Add(GRAVEROOF);
                        break;
                    case "Tree1":
                        GeometryDrawing TREE1 = new GeometryDrawing(textureCache.Tree1Brush, null, Static.RealPrimitive);
                        drawingCache.TopCache.Children.Add(TREE1);
                        break;
                    case "Tree2":
                        GeometryDrawing TREE2 = new GeometryDrawing(textureCache.Tree2Brush, null, Static.RealPrimitive);
                        drawingCache.TopCache.Children.Add(TREE2);
                        break;
                    case "Tree3":
                        GeometryDrawing TREE3 = new GeometryDrawing(textureCache.Tree3Brush, null, Static.RealPrimitive);
                        drawingCache.TopCache.Children.Add(TREE3);
                        break;
                    case "flower1":
                        GeometryDrawing FLOWER1 = new GeometryDrawing(textureCache.Flower1Brush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(FLOWER1);
                        break;
                    case "flower2":
                        GeometryDrawing FLOWER2 = new GeometryDrawing(textureCache.Flower2Brush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(FLOWER2);
                        break;
                    case "Well":
                        GeometryDrawing WELL = new GeometryDrawing(textureCache.WellBrush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(WELL);
                        break;
                    case "ForestChair":
                        GeometryDrawing FORESTCHAIR = new GeometryDrawing(textureCache.ForestChairBrush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(FORESTCHAIR);
                        break;
                    case "ForestChairFlipped":
                        GeometryDrawing FORESTCHAIRFLIPPED = new GeometryDrawing(textureCache.ForestChairFlippedBrush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(FORESTCHAIRFLIPPED);
                        break;
                    case "ForestBuilding":
                        GeometryDrawing FORESTBUILDING = new GeometryDrawing(textureCache.ForestBuildingBrush, null, Static.RealPrimitive);
                        drawingCache.TopCache.Children.Add(FORESTBUILDING);
                        break;
                    case "MazeBush":
                        GeometryDrawing MAZEBUSH = new GeometryDrawing(textureCache.MazeBushBrush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(MAZEBUSH);
                        break;
                    case "Pacman":
                        GeometryDrawing PACMAN = new GeometryDrawing(textureCache.PacmanBrush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(PACMAN);
                        break;
                    case "PacmanGhost":
                        GeometryDrawing PACMANGHOST = new GeometryDrawing(textureCache.PacmanGhostBrush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(PACMANGHOST);
                        break;
                    case "Ferrari":
                        GeometryDrawing FERRARI = new GeometryDrawing(textureCache.FerrariBrush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(FERRARI);
                        break;
                    case "MazeChair":
                        GeometryDrawing MAZECHAiR = new GeometryDrawing(textureCache.MazeChairBrush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(MAZECHAiR);
                        break;
                    case "MazeTv":
                        GeometryDrawing MAZETV = new GeometryDrawing(textureCache.MazeTVBrush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(MAZETV);
                        break;
                    case "MazeArtifact":
                        GeometryDrawing MAZEARTIFACT = new GeometryDrawing(textureCache.MazeArtifactBrush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(MAZEARTIFACT);
                        break;
                    case "Desert":
                        GeometryDrawing DESERT = new GeometryDrawing(textureCache.DesertBrush, null, Static.RealPrimitive);
                        drawingCache.MapCache.Children.Add(DESERT);
                        break;
                    case "Blitz":
                        GeometryDrawing BLITZ = new GeometryDrawing(textureCache.BlitzBrush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(BLITZ);
                        break;
                    case "Eagles":
                        GeometryDrawing EAGLES = new GeometryDrawing(textureCache.EaglesBrush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(EAGLES);
                        break;
                    case "Cliff1":
                        GeometryDrawing CLIFF1 = new GeometryDrawing(textureCache.Cliff1Brush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(CLIFF1);
                        break;
                    case "Cliff2":
                        GeometryDrawing CLIFF2 = new GeometryDrawing(textureCache.Cliff2rush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(CLIFF2);
                        break;
                    case "NFLBall":
                        GeometryDrawing NFLBALL = new GeometryDrawing(textureCache.NFLBallBrush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(NFLBALL);
                        break;
                    case "Rock1":
                        GeometryDrawing ROCK1 = new GeometryDrawing(textureCache.Rock1Brush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(ROCK1);
                        break;
                    case "Rock2":
                        GeometryDrawing ROCK2 = new GeometryDrawing(textureCache.Rock2Brush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(ROCK2);
                        break;
                    case "Rock3":
                        GeometryDrawing ROCK3 = new GeometryDrawing(textureCache.Rock3Brush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(ROCK3);
                        break;
                    case "Rock4":
                        GeometryDrawing ROCK4 = new GeometryDrawing(textureCache.Rock4Brush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(ROCK4);
                        break;
                    case "Stargate":
                        GeometryDrawing STARGATE = new GeometryDrawing(textureCache.StargateBrush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(STARGATE);
                        break;
                    case "Car1":
                        GeometryDrawing CAR1 = new GeometryDrawing(textureCache.Car1Brush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(CAR1);
                        break;
                    case "Car2":
                        GeometryDrawing CAR2 = new GeometryDrawing(textureCache.Car2Brush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(CAR2);
                        break;
                    case "Car3":
                        GeometryDrawing CAR3 = new GeometryDrawing(textureCache.Car3Brush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(CAR3);
                        break;
                    case "Car4":
                        GeometryDrawing CAR4 = new GeometryDrawing(textureCache.Car4Brush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(CAR4);
                        break;
                    case "Car5":
                        GeometryDrawing CAR5 = new GeometryDrawing(textureCache.Car5Brush, null, Static.RealPrimitive);
                        drawingCache.MiddleCache.Children.Add(CAR5);
                        break;
                    case "Cactus1":
                        GeometryDrawing CACTUS1 = new GeometryDrawing(textureCache.Cactus1Brush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(CACTUS1);
                        break;
                    case "Cactus2":
                        GeometryDrawing CACTUS2 = new GeometryDrawing(textureCache.Cactus2Brush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(CACTUS2);
                        break;
                    case "Cactus3":
                        GeometryDrawing CACTUS3 = new GeometryDrawing(textureCache.Cactus3Brush, null, Static.RealPrimitive);
                        drawingCache.GroundCache.Children.Add(CACTUS3);
                        break;
                    case "Line":
                        GeometryDrawing LINE = new GeometryDrawing(Brushes.Transparent, new Pen(Brushes.Black,1), Static.RealPrimitive);
                        drawingCache.MapCache.Children.Add(LINE);
                        break;
                    case "cloudFirst":
                        GeometryDrawing CLOUDONE = new GeometryDrawing(textureCache.cloudFirstBrush, null, Static.RealPrimitive);
                        drawingCache.AtmosphereCache.Children.Add(CLOUDONE);
                        break;
                    case "cloudSecond":
                        GeometryDrawing CLOUDTWO = new GeometryDrawing(textureCache.cloudSecondBrush, null, Static.RealPrimitive);
                        drawingCache.AtmosphereCache.Children.Add(CLOUDTWO);
                        break;
                    case "cloudThird":
                        GeometryDrawing CLOUDTHREE = new GeometryDrawing(textureCache.cloudFirstBrush, null, Static.RealPrimitive);
                        drawingCache.AtmosphereCache.Children.Add(CLOUDTHREE);
                        break;
                    case "Dirt1":
                        GeometryDrawing DIRTONE = new GeometryDrawing(textureCache.dirtFirstBrush, null, Static.RealPrimitive);
                        drawingCache.MapCache.Children.Add(DIRTONE);
                        break;
                    case "Dirt2":
                        GeometryDrawing DIRTWO = new GeometryDrawing(textureCache.dirtSecondBrush, null, Static.RealPrimitive);
                        drawingCache.MapCache.Children.Add(DIRTWO);
                        break;
                    default:
                        break;
                }
                drawingCache.RenderMap();
                drawingCache.RenderGround();
                drawingCache.RenderMiddle();
                drawingCache.RenderTop();
                drawingCache.RenderAtmosphere();
            }
        }
    }
}
