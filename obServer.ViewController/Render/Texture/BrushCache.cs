// <copyright file="BrushCache.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.ViewController.Render.Texture
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// BrushCache class.
    /// </summary>
    public class BrushCache
    {
        private static string directory = System.IO.Directory.GetCurrentDirectory();
        private DrawingBrush storedBulletBrush;
        private DrawingBrush weaponBulletBrush;
        private int lastBullets = -1;
        private Dictionary<Guid, Brush> playerNames;
        private DrawingBrush healthBarBrush;
        private double lastHealth = -1;
        private int capacity = -1;
        private int bulletCount = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrushCache"/> class.
        /// </summary>
        public BrushCache()
        {
            this.playerNames = new Dictionary<Guid, Brush>();
            this.SetupWeaponBrushes();
            this.CloudFirstBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\cloud1.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.CloudSecondBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\cloud2.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.DirtFirstBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\dirt1.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.DirtSecondBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\dirt2.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.PlayerBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\player.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.PlayerPistolBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\pistol.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.PlayerRifleBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\rifle.png")));
            this.PlayerShotgunBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\shotgun.png")));
            this.CrateBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\crate.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.LogBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\log.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.WeaponBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\log.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.MapBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\hugemap.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.WallBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\wall.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.BushBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\bush.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.RedTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\redtree.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.GreenTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\greentree.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.RoundTreeBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\roundtree.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.RifleSideBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\rifleside.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.ShotgunSideBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\shotgunside.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.PistolSideBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\pistolside.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.DirtyRoadBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\dirtyroad.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.GraveYardArrowBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveyardArrowSign.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.GraveYardSignBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveyardSign.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.GraveWall1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveWall1.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.GraveWall3Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveWall3.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.GraveWall5Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveWall5.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.GraveWall7Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveWall7.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.GraveFloorBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveFloor.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.GraveDirtBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveDirt.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.GraveBone1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Bone1.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.GraveBone2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Bone3.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.GraveTomb1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\TombStone1.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.GraveTomb2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\TombStone2.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.GraveSkeletonBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Skeleton.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.GraveRoofBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\GraveRoof.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Tree1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Tree1.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Tree2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Tree2.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Tree3Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Tree3.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Flower1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\flower1.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Flower2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\flower2.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.WellBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Well.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.ForestChairBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Chair.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.ForestBuildingBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\ForestBuilding.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.ForestChairFlippedBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\ChairFlipped.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.MazeBushBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\MazeBush.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.PacmanBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Pacman.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.PacmanGhostBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\PacmanGhost.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.FerrariBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Ferrari.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.MazeChairBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\MazeFotel.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.MazeTVBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\MazeTv.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.MazeArtifactBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Artifact.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.DesertBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Desert.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.BlitzBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Blitz.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.EaglesBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Eagles.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Cliff1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Cliff1.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Cliff2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Cliff2.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.NflBallBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\NFLBall.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Rock1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Rock1.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Rock2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Rock2.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Rock3Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Rock3.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Rock4Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Rock4.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.StargateBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Stargate.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Car1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Car1.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Car2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Car2.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Car3Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Car3.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Car4Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Car4.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Car5Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Car5.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Cactus1Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Cactus01.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Cactus2Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Cactus02.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.Cactus3Brush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\Cactus03.png")) { CacheOption = BitmapCacheOption.OnLoad });
            this.OfficialFont = new Typeface("Segoe UI");
            this.BlackBorder = new Pen(Brushes.Black, 2);
            this.WhiteBorder = new Pen(Brushes.White, 1);
            this.ThinBlackBorder = new Pen(Brushes.Black, 0.3);
            this.BulletBrush = Brushes.DarkGray;
            this.MapEndBrush = Brushes.DarkOliveGreen;
        }

        /// <summary>
        /// Gets or sets shotgun drawbrush.
        /// </summary>
        public DrawingBrush ShotgunAloneBrush { get; set; }

        /// <summary>
        /// Gets or sets rifle brush.
        /// </summary>
        public DrawingBrush RifleAloneBrush { get; set; }

        /// <summary>
        /// Gets or sets pistol drawbrush.
        /// </summary>
        public DrawingBrush PistolAloneBrush { get; set; }

        /// <summary>
        /// Gets or sets first cloud brush.
        /// </summary>
        public ImageBrush CloudFirstBrush { get; set; }

        /// <summary>
        /// Gets or sets second cloud brush.
        /// </summary>
        public ImageBrush CloudSecondBrush { get; set; }

        /// <summary>
        /// Gets or sets first dirtt brush.
        /// </summary>
        public ImageBrush DirtFirstBrush { get; set; }

        /// <summary>
        /// Gets or sets second dirt brush.
        /// </summary>
        public ImageBrush DirtSecondBrush { get; set; }

        /// <summary>
        /// Gets or sets player brush.
        /// </summary>
        public ImageBrush PlayerBrush { get; set; }

        /// <summary>
        /// Gets or sets pistol brush.
        /// </summary>
        public ImageBrush PlayerPistolBrush { get; set; }

        /// <summary>
        /// Gets or sets rifle brush.
        /// </summary>
        public ImageBrush PlayerRifleBrush { get; set; }

        /// <summary>
        /// Gets or sets shotgun brush.
        /// </summary>
        public ImageBrush PlayerShotgunBrush { get; set; }

        /// <summary>
        /// Gets or sets crate brush.
        /// </summary>
        public ImageBrush CrateBrush { get; set; }

        /// <summary>
        /// Gets or sets log brush.
        /// </summary>
        public ImageBrush LogBrush { get; set; }

        /// <summary>
        /// Gets or sets weapon brush.
        /// </summary>
        public ImageBrush WeaponBrush { get; set; }

        /// <summary>
        /// Gets or sets map brush.
        /// </summary>
        public ImageBrush MapBrush { get; set; }

        /// <summary>
        /// Gets or sets wall brush.
        /// </summary>
        public ImageBrush WallBrush { get; set; }

        /// <summary>
        /// Gets or sets bush brush.
        /// </summary>
        public ImageBrush BushBrush { get; set; }

        /// <summary>
        /// Gets or sets red tree brush.
        /// </summary>
        public ImageBrush RedTreeBrush { get; set; }

        /// <summary>
        /// Gets or sets green tree brush.
        /// </summary>
        public ImageBrush GreenTreeBrush { get; set; }

        /// <summary>
        /// Gets or sets round tree brush.
        /// </summary>
        public ImageBrush RoundTreeBrush { get; set; }

        /// <summary>
        /// Gets or sets rifle side brush.
        /// </summary>
        public ImageBrush RifleSideBrush { get; set; }

        /// <summary>
        /// Gets or sets shotgun side brush.
        /// </summary>
        public ImageBrush ShotgunSideBrush { get; set; }

        /// <summary>
        /// Gets or sets pistol side brush.
        /// </summary>
        public ImageBrush PistolSideBrush { get; set; }

        /// <summary>
        /// Gets or sets dirty road brush.
        /// </summary>
        public ImageBrush DirtyRoadBrush { get; set; }

        /// <summary>
        /// Gets or sets graveyard arrow brush.
        /// </summary>
        public ImageBrush GraveYardArrowBrush { get; set; }

        /// <summary>
        /// Gets or sets graveyard sign brush.
        /// </summary>
        public ImageBrush GraveYardSignBrush { get; set; }

        /// <summary>
        /// Gets or sets graveWall 1 brush.
        /// </summary>
        public ImageBrush GraveWall1Brush { get; set; }

        /// <summary>
        /// Gets or sets gravewall 3 brush.
        /// </summary>
        public ImageBrush GraveWall3Brush { get; set; }

        /// <summary>
        /// Gets or sets gravewall 5 brush.
        /// </summary>
        public ImageBrush GraveWall5Brush { get; set; }

        /// <summary>
        /// Gets or sets gravewall 7 brush.
        /// </summary>
        public ImageBrush GraveWall7Brush { get; set; }

        /// <summary>
        /// Gets or sets gravefloor brush.
        /// </summary>
        public ImageBrush GraveFloorBrush { get; set; }

        /// <summary>
        /// Gets or sets gravedirt brush.
        /// </summary>
        public ImageBrush GraveDirtBrush { get; set; }

        /// <summary>
        /// Gets or sets gravebone 1 brush.
        /// </summary>
        public ImageBrush GraveBone1Brush { get; set; }

        /// <summary>
        /// Gets or sets gravebone 2 brush.
        /// </summary>
        public ImageBrush GraveBone2Brush { get; set; }

        /// <summary>
        /// Gets or sets gravetomb1 brush.
        /// </summary>
        public ImageBrush GraveTomb1Brush { get; set; }

        /// <summary>
        /// Gets or sets gravetomb 2 brush.
        /// </summary>
        public ImageBrush GraveTomb2Brush { get; set; }

        /// <summary>
        /// Gets or sets graveskeleton brush.
        /// </summary>
        public ImageBrush GraveSkeletonBrush { get; set; }

        /// <summary>
        /// Gets or sets graveroof brush.
        /// </summary>
        public ImageBrush GraveRoofBrush { get; set; }

        /// <summary>
        /// Gets or sets tree 1 brush.
        /// </summary>
        public ImageBrush Tree1Brush { get; set; }

        /// <summary>
        /// Gets or sets tree 2 brush.
        /// </summary>
        public ImageBrush Tree2Brush { get; set; }

        /// <summary>
        /// Gets or sets tree 3 brush.
        /// </summary>
        public ImageBrush Tree3Brush { get; set; }

        /// <summary>
        /// Gets or sets flower 1 brush.
        /// </summary>
        public ImageBrush Flower1Brush { get; set; }

        /// <summary>
        /// Gets or sets flower 2 brush.
        /// </summary>
        public ImageBrush Flower2Brush { get; set; }

        /// <summary>
        /// Gets or sets well brush.
        /// </summary>
        public ImageBrush WellBrush { get; set; }

        /// <summary>
        /// Gets or sets forest chair brush.
        /// </summary>
        public ImageBrush ForestChairBrush { get; set; }

        /// <summary>
        /// Gets or sets forest building brush.
        /// </summary>
        public ImageBrush ForestBuildingBrush { get; set; }

        /// <summary>
        /// Gets or sets forest chair flipped brush.
        /// </summary>
        public ImageBrush ForestChairFlippedBrush { get; set; }

        /// <summary>
        /// Gets or sets maze bush brush.
        /// </summary>
        public ImageBrush MazeBushBrush { get; set; }

        /// <summary>
        /// Gets or sets pacman brush.
        /// </summary>
        public ImageBrush PacmanBrush { get; set; }

        /// <summary>
        /// Gets or sets pacman ghost brush.
        /// </summary>
        public ImageBrush PacmanGhostBrush { get; set; }

        /// <summary>
        /// Gets or sets ferrari brush.
        /// </summary>
        public ImageBrush FerrariBrush { get; set; }

        /// <summary>
        /// Gets or sets maze chair brush.
        /// </summary>
        public ImageBrush MazeChairBrush { get; set; }

        /// <summary>
        /// Gets or sets maze tv brush.
        /// </summary>
        public ImageBrush MazeTVBrush { get; set; }

        /// <summary>
        /// Gets or sets maze artifact brush.
        /// </summary>
        public ImageBrush MazeArtifactBrush { get; set; }

        /// <summary>
        /// Gets or sets desert brush.
        /// </summary>
        public ImageBrush DesertBrush { get; set; }

        /// <summary>
        /// Gets or sets blitz brush.
        /// </summary>
        public ImageBrush BlitzBrush { get; set; }

        /// <summary>
        /// Gets or sets eagles brush.
        /// </summary>
        public ImageBrush EaglesBrush { get; set; }

        /// <summary>
        /// Gets or sets cliff brush.
        /// </summary>
        public ImageBrush Cliff1Brush { get; set; }

        /// <summary>
        /// Gets or sets cliff brush.
        /// </summary>
        public ImageBrush Cliff2Brush { get; set; }

        /// <summary>
        /// Gets or sets nfl ball brush.
        /// </summary>
        public ImageBrush NflBallBrush { get; set; }

        /// <summary>
        /// Gets or sets rock 1 brush.
        /// </summary>
        public ImageBrush Rock1Brush { get; set; }

        /// <summary>
        /// Gets or sets rock 2 brush.
        /// </summary>
        public ImageBrush Rock2Brush { get; set; }

        /// <summary>
        /// Gets or sets rock 3 brush.
        /// </summary>
        public ImageBrush Rock3Brush { get; set; }

        /// <summary>
        /// Gets or sets rock 4 brush.
        /// </summary>
        public ImageBrush Rock4Brush { get; set; }

        /// <summary>
        /// Gets or sets stargate brush.
        /// </summary>
        public ImageBrush StargateBrush { get; set; }

        /// <summary>
        /// Gets or sets car 1 brush.
        /// </summary>
        public ImageBrush Car1Brush { get; set; }

        /// <summary>
        /// Gets or sets car 2 brush.
        /// </summary>
        public ImageBrush Car2Brush { get; set; }

        /// <summary>
        /// Gets or sets car 3 brush.
        /// </summary>
        public ImageBrush Car3Brush { get; set; }

        /// <summary>
        /// Gets or sets car 4 brush.
        /// </summary>
        public ImageBrush Car4Brush { get; set; }

        /// <summary>
        /// Gets or sets car 5 brush.
        /// </summary>
        public ImageBrush Car5Brush { get; set; }

        /// <summary>
        /// Gets or sets cactus 1 brush.
        /// </summary>
        public ImageBrush Cactus1Brush { get; set; }

        /// <summary>
        /// Gets or sets cactus 2 brush.
        /// </summary>
        public ImageBrush Cactus2Brush { get; set; }

        /// <summary>
        /// Gets or sets cactus 3 brush.
        /// </summary>
        public ImageBrush Cactus3Brush { get; set; }

        /// <summary>
        /// Gets or sets official font typeface.
        /// </summary>
        public Typeface OfficialFont { get; set; }

        /// <summary>
        /// Gets or sets blackborder pen.
        /// </summary>
        public Pen BlackBorder { get; set; }

        /// <summary>
        /// Gets or sets whiteborder pen.
        /// </summary>
        public Pen WhiteBorder { get; set; }

        /// <summary>
        /// Gets or sets thinblackborder pen.
        /// </summary>
        public Pen ThinBlackBorder { get; set; }

        /// <summary>
        /// Gets or sets bullet solidcolor brush.
        /// </summary>
        public SolidColorBrush BulletBrush { get; set; }

        /// <summary>
        /// Gets or sets map end brush.
        /// </summary>
        public SolidColorBrush MapEndBrush { get; set; }

        /// <summary>
        /// Healthbar drawer.
        /// </summary>
        /// <param name="health">Amount of healt.</param>
        /// <param name="wid">Healtbar width.</param>
        /// <param name="hei">Healthbar height.</param>
        /// <returns>Brush.</returns>
        public Brush DrawHealthBar(double health, double wid, double hei)
        {
            if (this.lastHealth != health)
            {
                this.healthBarBrush = new DrawingBrush();
                var dg = new DrawingGroup();
                if (health > 0)
                {
                    dg.Children.Add(new GeometryDrawing(Brushes.MediumVioletRed, null, new RectangleGeometry(new Rect(0, 0, health * 2, 30))));
                }

                dg.Children.Add(new GeometryDrawing(Brushes.Transparent, null, new RectangleGeometry(new Rect(0, 0, wid, hei))));
                dg.Children.Add(new GeometryDrawing(
                    this.DrawText(
                        Brushes.Transparent,
                        health.ToString(),
                        18,
                        new Rect(0, 3, wid, hei),
                        Brushes.WhiteSmoke,
                        this.OfficialFont,
                        this.BlackBorder,
                        new Pen(Brushes.WhiteSmoke, 2),
                        1),
                    null,
                    new RectangleGeometry(new Rect(0, 0, wid, hei))));
                this.healthBarBrush.Drawing = dg;
                this.healthBarBrush.Opacity = 0.8;
                this.lastHealth = health;
            }

            return this.healthBarBrush;
        }

        /// <summary>
        /// DrawBullet method.
        /// </summary>
        /// <param name="count">Bullet count.</param>
        /// <param name="addText">Text.</param>
        /// <returns>Bursh.</returns>
        public Brush DrawBullets(int count, string addText)
        {
            if (this.lastBullets != count)
            {
                this.storedBulletBrush = new DrawingBrush();
                var dg = new DrawingGroup();
                int fontSize = 16;
                string text = addText + count.ToString();
                dg.Children.Add(new GeometryDrawing(this.SimpleText(text, fontSize, Brushes.White, this.OfficialFont, this.WhiteBorder), null, new RectangleGeometry(new Rect(0, 0, text.Length * fontSize * 0.6, fontSize))));
                this.storedBulletBrush.Drawing = dg;
                this.storedBulletBrush.Opacity = 0.8;
            }

            return this.storedBulletBrush;
        }

        /// <summary>
        /// Bullet drawer.
        /// </summary>
        /// <param name="count">Bullet count.</param>
        /// <param name="capacity">Capacity count.</param>
        /// <param name="addText">Text.</param>
        /// <returns>Brush.</returns>
        public Brush DrawWeaponBullets(int count, int capacity, string addText)
        {
            if (this.capacity != capacity || this.bulletCount != count)
            {
                this.weaponBulletBrush = new DrawingBrush();
                var dg = new DrawingGroup();
                int fontSize = 16;
                int fontSizeReal = fontSize - 1;
                string text = addText + count.ToString() + "/" + capacity.ToString();
                dg.Children.Add(new GeometryDrawing(this.SimpleText(text, fontSizeReal, Brushes.White, this.OfficialFont, this.WhiteBorder), null, new RectangleGeometry(new Rect(0, 0, text.Length * fontSize * 0.6, fontSize))));
                this.weaponBulletBrush.Drawing = dg;
                this.weaponBulletBrush.Opacity = 0.8;
            }

            return this.weaponBulletBrush;
        }

        /// <summary>
        /// Text drawer.
        /// </summary>
        /// <param name="bg">Brush color.</param>
        /// <param name="text">Text.</param>
        /// <param name="fontSize">Fontsize.</param>
        /// <param name="panel">Panel type.</param>
        /// <param name="fontColor">Color.</param>
        /// <param name="fontTypeface">Font typeface.</param>
        /// <param name="panelBorder">Panel border.</param>
        /// <param name="fontBorder">Font border.</param>
        /// <param name="opacity">Font opacity.</param>
        /// <returns>Brush.</returns>
        public Brush DrawText(Brush bg, string text, int fontSize, Rect panel, Brush fontColor, Typeface fontTypeface, Pen panelBorder = null, Pen fontBorder = null, double opacity = 1)
        {
            var dg = new DrawingGroup();
            int chars = text.Length;
            double yalign = (panel.Height / 2) - (fontSize / 2);
            double xalign = (panel.Width / 2) - (chars * fontSize * 0.6 * 0.5);
            dg.Children.Add(new GeometryDrawing(bg, panelBorder, new RectangleGeometry(panel)));
            dg.Children.Add(new GeometryDrawing(fontColor, fontBorder, new FormattedText(
                text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                fontTypeface,
                fontSize,
                fontColor,
                1.25).BuildGeometry(new Point(xalign, yalign))));
            DrawingBrush textBrush = new DrawingBrush
            {
                Drawing = dg,
                Opacity = opacity,
            };
            return textBrush;
        }

        /// <summary>
        /// Generating name.
        /// </summary>
        /// <param name="identifier">IDentify id.</param>
        /// <param name="name">Name.</param>
        /// <param name="fontSize">Name size.</param>
        /// <returns>Brush.</returns>
        public Brush GenerateName(Guid identifier, string name, int fontSize)
        {
            if (!this.playerNames.ContainsKey(identifier))
            {
                var textBrush = this.SimpleText(name, fontSize, Brushes.DarkRed, this.OfficialFont, this.ThinBlackBorder);
                this.playerNames.Add(identifier, textBrush);
                return textBrush;
            }

            return this.playerNames.Where(x => x.Key == identifier).First().Value;
        }

        /// <summary>
        /// Text drawer.
        /// </summary>
        /// <param name="relativeTo">Relatice rectangle.</param>
        /// <param name="textBrush">Text brush.</param>
        /// <param name="text">Text string.</param>
        /// <param name="fontSize">Text size.</param>
        /// <returns>Geometry.</returns>
        public GeometryDrawing DrawTextUp(Rect relativeTo, Brush textBrush, string text, int fontSize)
        {
            int letterCount = text.Length;
            double xOffset = relativeTo.X + (relativeTo.Width / 2) - (letterCount * 0.5 * fontSize * 0.6);
            double yOffset = relativeTo.Y - (fontSize * 1.2);
            Rect panel = new Rect(xOffset, yOffset, letterCount * fontSize * 0.6, fontSize);
            return new GeometryDrawing(textBrush, null, new RectangleGeometry(panel));
        }

        private void SetupWeaponBrushes()
        {
            double pWidth = 100 * 4;
            double pHeight = 100 * 3.5;
            double radius = 200;
            Rect imagePanel = new Rect(radius - (pWidth * 0.5), radius - (pHeight * 0.5), pWidth, pHeight);
            DrawingGroup dGroup = new DrawingGroup();
            dGroup.Children.Add(new GeometryDrawing(Brushes.Gray, new Pen(Brushes.Red, 30), new EllipseGeometry(new Point(radius, radius), radius, radius)));
            dGroup.Children.Add(new GeometryDrawing(this.ShotgunSideBrush, null, new RectangleGeometry(imagePanel)));
            dGroup.Opacity = 0.8;
            this.ShotgunAloneBrush = new DrawingBrush
            {
                Drawing = dGroup,
            };
            dGroup = new DrawingGroup();
            dGroup.Children.Add(new GeometryDrawing(Brushes.Gray, new Pen(Brushes.Green, 30), new EllipseGeometry(new Point(radius, radius), radius, radius)));
            dGroup.Children.Add(new GeometryDrawing(this.PistolSideBrush, null, new RectangleGeometry(imagePanel)));
            dGroup.Opacity = 0.8;
            this.PistolAloneBrush = new DrawingBrush
            {
                Drawing = dGroup,
            };
            dGroup = new DrawingGroup();
            dGroup.Children.Add(new GeometryDrawing(Brushes.Gray, new Pen(Brushes.Blue, 30), new EllipseGeometry(new Point(radius, radius), radius, radius)));
            dGroup.Children.Add(new GeometryDrawing(this.RifleSideBrush, null, new RectangleGeometry(imagePanel)));
            dGroup.Opacity = 0.8;
            this.RifleAloneBrush = new DrawingBrush
            {
                Drawing = dGroup,
            };
        }

        private Brush SimpleText(string text, int fontSize, Brush color, Typeface typeface, Pen pen)
        {
            var dg = new DrawingGroup();
            dg.Children.Add(new GeometryDrawing(color, pen, new FormattedText(
                text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                typeface,
                fontSize,
                color,
                1.25).BuildGeometry(new Point(0, 0))));
            DrawingBrush textBrush = new DrawingBrush
            {
                Drawing = dg,
            };
            return textBrush;
        }
    }
}
