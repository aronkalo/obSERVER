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
        private static string directory = Directory.GetCurrentDirectory();
        private static ImageBrush playerBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\player.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush crateBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\crate.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush logBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\log.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush weaponBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\log.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush mapBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\hugemap.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush wallBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\wall.png")) { CacheOption = BitmapCacheOption.OnLoad });

        private static ImageBrush dirtyRoadBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\dirtyroad.png")) { CacheOption = BitmapCacheOption.OnLoad });
        
        private static ImageBrush graveYardArrowBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveyardArrowSign.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush graveYardSignBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveyardSign.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush graveWall1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveWall1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush graveWall3Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveWall3.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush graveWall5Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveWall5.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush graveWall7Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveWall7.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush graveFloorBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveFloor.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush graveDirtBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveDirt.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush graveBone1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Bone1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush graveBone2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Bone3.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush graveTomb1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\TombStone1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush graveTomb2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\TombStone2.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush graveSkeletonBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Skeleton.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush graveRoofBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveRoof.png")) { CacheOption = BitmapCacheOption.OnLoad });

        private static ImageBrush bushBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\bush.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush redTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\redtree.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush greenTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\greentree.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush roundTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\roundtree.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Tree1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Tree1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Tree2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Tree2.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Tree3Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Tree3.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Flower1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\flower1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Flower2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\flower2.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush WellBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Well.png")) { CacheOption = BitmapCacheOption.OnLoad });

        private static ImageBrush ForestChairBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Chair.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush ForestBuildingBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\ForestBuilding.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush ForestChairFlippedBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\ChairFlipped.png")) { CacheOption = BitmapCacheOption.OnLoad });

        //private static ImageBrush Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\ChairFlipped.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush MazeBushBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\MazeBush.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush PacmanBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Pacman.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush PacmanGhostBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\PacmanGhost.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush FerrariBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Ferrari.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush MazeChairBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\MazeFotel.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush MazeTVBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\MazeTv.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush MazeArtifactBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Artifact.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush DesertBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Desert.png")) { CacheOption = BitmapCacheOption.OnLoad });

        private static ImageBrush BlitzBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Blitz.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush EaglesBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Eagles.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Cliff1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Cliff1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Cliff2rush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Cliff2.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush NFLBallBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\NFLBall.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Rock1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Rock1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Rock2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Rock2.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Rock3Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Rock3.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Rock4Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Rock4.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush StargateBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Stargate.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Car1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Car1.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Car2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Car2.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Car3Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Car3.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Car4Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Car4.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Car5Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Car5.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Cactus1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Cactus01.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Cactus2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Cactus02.png")) { CacheOption = BitmapCacheOption.OnLoad });
        private static ImageBrush Cactus3Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Cactus03.png")) { CacheOption = BitmapCacheOption.OnLoad });


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
            DrawStatic();
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

            context.DrawDrawing(dGroup);
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
                    case "GraveyardArrowSign":
                        GeometryDrawing GRAVEYARDARROWSIGN = new GeometryDrawing(graveYardArrowBrush, null, Static.RealPrimitive);
                        cache.Children.Add(GRAVEYARDARROWSIGN);
                        break;
                    case "DirtyRoad":
                        GeometryDrawing DIRTYROAD = new GeometryDrawing(dirtyRoadBrush, null, Static.RealPrimitive);
                        cache.Children.Add(DIRTYROAD);
                        break;
                    case "GraveWall1":
                        GeometryDrawing GRAVEWALL1 = new GeometryDrawing(graveWall1Brush, null, Static.RealPrimitive);
                        cache.Children.Add(GRAVEWALL1);
                        break;                       
                    case "GraveWall3":
                        GeometryDrawing GRAVEWALL3 = new GeometryDrawing(graveWall3Brush, null, Static.RealPrimitive);
                        cache.Children.Add(GRAVEWALL3);
                        break;
                    case "GraveWall5":
                        GeometryDrawing GRAVEWALL5 = new GeometryDrawing(graveWall5Brush, null, Static.RealPrimitive);
                        cache.Children.Add(GRAVEWALL5);
                        break;
                    case "GraveWall7":
                        GeometryDrawing GRAVEWALL7 = new GeometryDrawing(graveWall7Brush, null, Static.RealPrimitive);
                        cache.Children.Add(GRAVEWALL7);
                        break;
                    case "GraveFloor":
                        GeometryDrawing GRAVEFLOOR = new GeometryDrawing(graveFloorBrush, null, Static.RealPrimitive);
                        cache.Children.Add(GRAVEFLOOR);
                        break;
                    case "TombStone1":
                        GeometryDrawing TOMBSTONE1 = new GeometryDrawing(graveTomb1Brush, null, Static.RealPrimitive);
                        cache.Children.Add(TOMBSTONE1);
                        break;
                    case "TombStone2":
                        GeometryDrawing TOMBSTONE2 = new GeometryDrawing(graveTomb2Brush, null, Static.RealPrimitive);
                        cache.Children.Add(TOMBSTONE2);
                        break;
                    case "Bone1":
                        GeometryDrawing BONE1 = new GeometryDrawing(graveBone1Brush, null, Static.RealPrimitive);
                        cache.Children.Add(BONE1);
                        break;
                    case "Bone2":
                        GeometryDrawing BONE2 = new GeometryDrawing(graveBone2Brush, null, Static.RealPrimitive);
                        cache.Children.Add(BONE2);
                        break;
                    case "Skeleton":
                        GeometryDrawing SKELETON = new GeometryDrawing(graveSkeletonBrush, null, Static.RealPrimitive);
                        cache.Children.Add(SKELETON);
                        break;
                    case "GraveDirt":
                        GeometryDrawing GRAVEDIRT = new GeometryDrawing(graveDirtBrush, null, Static.RealPrimitive);
                        cache.Children.Add(GRAVEDIRT);
                        break;
                    case "GraveyardSign":
                        GeometryDrawing GRAVESIGN = new GeometryDrawing(graveYardSignBrush, null, Static.RealPrimitive);
                        cache.Children.Add(GRAVESIGN);
                        break;
                    case "GraveRoof":
                        GeometryDrawing GRAVEROOF = new GeometryDrawing(graveRoofBrush, null, Static.RealPrimitive);
                        cache.Children.Add(GRAVEROOF);
                        break;
                    case "Tree1":
                        GeometryDrawing TREE1 = new GeometryDrawing(Tree1Brush, null, Static.RealPrimitive);
                        cache.Children.Add(TREE1);
                        break;
                    case "Tree2":
                        GeometryDrawing TREE2 = new GeometryDrawing(Tree2Brush, null, Static.RealPrimitive);
                        cache.Children.Add(TREE2);
                        break;
                    case "Tree3":
                        GeometryDrawing TREE3 = new GeometryDrawing(Tree3Brush, null, Static.RealPrimitive);
                        cache.Children.Add(TREE3);
                        break;
                    case "flower1":
                        GeometryDrawing FLOWER1 = new GeometryDrawing(Flower1Brush, null, Static.RealPrimitive);
                        cache.Children.Add(FLOWER1);
                        break;
                    case "flower2":
                        GeometryDrawing FLOWER2 = new GeometryDrawing(Flower2Brush, null, Static.RealPrimitive);
                        cache.Children.Add(FLOWER2);
                        break;
                    case "Well":
                        GeometryDrawing WELL = new GeometryDrawing(WellBrush, null, Static.RealPrimitive);
                        cache.Children.Add(WELL);
                        break;
                    case "ForestChair":
                        GeometryDrawing FORESTCHAIR = new GeometryDrawing(ForestChairBrush, null, Static.RealPrimitive);
                        cache.Children.Add(FORESTCHAIR);
                        break;
                    case "ForestChairFlipped":
                        GeometryDrawing FORESTCHAIRFLIPPED = new GeometryDrawing(ForestChairFlippedBrush, null, Static.RealPrimitive);
                        cache.Children.Add(FORESTCHAIRFLIPPED);
                        break;
                    case "ForestBuilding":
                        GeometryDrawing FORESTBUILDING = new GeometryDrawing(ForestBuildingBrush, null, Static.RealPrimitive);
                        cache.Children.Add(FORESTBUILDING);
                        break;
                    case "MazeBush":
                        GeometryDrawing MAZEBUSH = new GeometryDrawing(MazeBushBrush, null, Static.RealPrimitive);
                        cache.Children.Add(MAZEBUSH);
                        break;
                    case "Pacman":
                        GeometryDrawing PACMAN = new GeometryDrawing(PacmanBrush, null, Static.RealPrimitive);
                        cache.Children.Add(PACMAN);
                        break;
                    case "PacmanGhost":
                        GeometryDrawing PACMANGHOST = new GeometryDrawing(PacmanGhostBrush, null, Static.RealPrimitive);
                        cache.Children.Add(PACMANGHOST);
                        break;
                    case "Ferrari":
                        GeometryDrawing FERRARI = new GeometryDrawing(FerrariBrush, null, Static.RealPrimitive);
                        cache.Children.Add(FERRARI);
                        break;
                    case "MazeChair":
                        GeometryDrawing MAZECHAiR = new GeometryDrawing(MazeChairBrush, null, Static.RealPrimitive);
                        cache.Children.Add(MAZECHAiR);
                        break;
                    case "MazeTv":
                        GeometryDrawing MAZETV = new GeometryDrawing(MazeTVBrush, null, Static.RealPrimitive);
                        cache.Children.Add(MAZETV);
                        break;
                    case "MazeArtifact":
                        GeometryDrawing MAZEARTIFACT = new GeometryDrawing(MazeArtifactBrush, null, Static.RealPrimitive);
                        cache.Children.Add(MAZEARTIFACT);
                        break;
                    case "Desert":
                        GeometryDrawing DESERT = new GeometryDrawing(DesertBrush, null, Static.RealPrimitive);
                        cache.Children.Add(DESERT);
                        break;
                    case "Blitz":
                        GeometryDrawing BLITZ = new GeometryDrawing(BlitzBrush, null, Static.RealPrimitive);
                        cache.Children.Add(BLITZ);
                        break;
                    case "Eagles":
                        GeometryDrawing EAGLES = new GeometryDrawing(EaglesBrush, null, Static.RealPrimitive);
                        cache.Children.Add(EAGLES);
                        break;
                    case "Cliff1":
                        GeometryDrawing CLIFF1 = new GeometryDrawing(Cliff1Brush, null, Static.RealPrimitive);
                        cache.Children.Add(CLIFF1);
                        break;
                    case "Cliff2":
                        GeometryDrawing CLIFF2 = new GeometryDrawing(Cliff2rush, null, Static.RealPrimitive);
                        cache.Children.Add(CLIFF2);
                        break;
                    case "NFLBall":
                        GeometryDrawing NFLBALL = new GeometryDrawing(NFLBallBrush, null, Static.RealPrimitive);
                        cache.Children.Add(NFLBALL);
                        break;
                    case "Rock1":
                        GeometryDrawing ROCK1 = new GeometryDrawing(Rock1Brush, null, Static.RealPrimitive);
                        cache.Children.Add(ROCK1);
                        break;
                    case "Rock2":
                        GeometryDrawing ROCK2 = new GeometryDrawing(Rock2Brush, null, Static.RealPrimitive);
                        cache.Children.Add(ROCK2);
                        break;
                    case "Rock3":
                        GeometryDrawing ROCK3 = new GeometryDrawing(Rock3Brush, null, Static.RealPrimitive);
                        cache.Children.Add(ROCK3);
                        break;
                    case "Rock4":
                        GeometryDrawing ROCK4 = new GeometryDrawing(Rock4Brush, null, Static.RealPrimitive);
                        cache.Children.Add(ROCK4);
                        break;
                    case "Stargate":
                        GeometryDrawing STARGATE = new GeometryDrawing(StargateBrush, null, Static.RealPrimitive);
                        cache.Children.Add(STARGATE);
                        break;
                    case "Car1":
                        GeometryDrawing CAR1 = new GeometryDrawing(Car1Brush, null, Static.RealPrimitive);
                        cache.Children.Add(CAR1);
                        break;
                    case "Car2":
                        GeometryDrawing CAR2 = new GeometryDrawing(Car2Brush, null, Static.RealPrimitive);
                        cache.Children.Add(CAR2);
                        break;
                    case "Car3":
                        GeometryDrawing CAR3 = new GeometryDrawing(Car3Brush, null, Static.RealPrimitive);
                        cache.Children.Add(CAR3);
                        break;
                    case "Car4":
                        GeometryDrawing CAR4 = new GeometryDrawing(Car4Brush, null, Static.RealPrimitive);
                        cache.Children.Add(CAR4);
                        break;
                    case "Car5":
                        GeometryDrawing CAR5 = new GeometryDrawing(Car5Brush, null, Static.RealPrimitive);
                        cache.Children.Add(CAR5);
                        break;
                    case "Cactus1":
                        GeometryDrawing CACTUS1 = new GeometryDrawing(Cactus1Brush, null, Static.RealPrimitive);
                        cache.Children.Add(CACTUS1);
                        break;
                    case "Cactus2":
                        GeometryDrawing CACTUS2 = new GeometryDrawing(Cactus2Brush, null, Static.RealPrimitive);
                        cache.Children.Add(CACTUS2);
                        break;
                    case "Cactus3":
                        GeometryDrawing CACTUS3 = new GeometryDrawing(Cactus3Brush, null, Static.RealPrimitive);
                        cache.Children.Add(CACTUS3);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
