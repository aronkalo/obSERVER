// <copyright file="ObServerRenderer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.ViewController.Render
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using obServer.GameModel.Interfaces;
    using obServer.Repository.GameModel;
    using obServer.ViewController.Render.Texture;

    /// <summary>
    /// Observer renderer class.
    /// </summary>
    public class ObServerRenderer
    {
        private RenderCache drawingCache;
        private BrushCache textureCache;
        private IRepoOBServerModel model;
        private double xCenter;
        private double yCenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObServerRenderer"/> class.
        /// </summary>
        /// <param name="model">Repo model.</param>
        public ObServerRenderer(IRepoOBServerModel model)
        {
            this.model = model;
            this.drawingCache = new RenderCache();
            this.textureCache = new BrushCache();
            this.DrawStatic();
        }

        /// <summary>
        /// Set offset method.
        /// </summary>
        /// <param name="xCen">X center.</param>
        /// <param name="yCen">Y center.</param>
        public void SetOffsets(double xCen, double yCen)
        {
            this.xCenter = xCen;
            this.yCenter = yCen;
        }

        /// <summary>
        /// Weapon drawing method.
        /// </summary>
        /// <param name="weapon">Weapon type weapon.</param>
        /// <returns>Geometry.</returns>
        public GeometryDrawing WeaponDrawing(IWeapon weapon)
        {
            if (weapon.Owned)
            {
                return new GeometryDrawing(null, this.textureCache.BlackBorder, new EllipseGeometry(new Rect(weapon.Position.X, weapon.Position.Y, 1, 1)));
            }
            else
            {
                switch (weapon.WeaponInfo[0])
                {
                    case 8:
                        return new GeometryDrawing(this.textureCache.ShotgunAloneBrush, null, new EllipseGeometry(new Rect(weapon.Position.X, weapon.Position.Y, 80, 80)));
                    case 20:
                        return new GeometryDrawing(this.textureCache.PistolAloneBrush, null, new EllipseGeometry(new Rect(weapon.Position.X, weapon.Position.Y, 80, 80)));
                    case 30:
                        return new GeometryDrawing(this.textureCache.RifleAloneBrush, null, new EllipseGeometry(new Rect(weapon.Position.X, weapon.Position.Y, 80, 80)));
                    default:
                        throw new Exception("Not implemented weapon texture");
                }
            }
        }

        /// <summary>
        /// Healthbar drawing method.
        /// </summary>
        /// <returns>Geometry.</returns>
        public GeometryDrawing DrawHealthBar()
        {
            double hwidth = 200;
            double hheight = 30;
            return new GeometryDrawing(this.textureCache.DrawHealthBar(this.model.MyPlayer.Health, hwidth, hheight), this.textureCache.BlackBorder, new RectangleGeometry() { Rect = new Rect(this.model.MyPlayer.Position.X - this.xCenter + 20, this.model.MyPlayer.Position.Y + this.yCenter - 50, hwidth, hheight) });
        }

        /// <summary>
        /// Static draw method.
        /// </summary>
        public void DrawStatic()
        {
            foreach (var stat in this.model.Statics)
            {
                switch ((stat as IStaticItem).Type)
                {
                    case "Map":
                        GeometryDrawing mAP = new GeometryDrawing(this.textureCache.MapBrush, null, stat.RealPrimitive);
                        this.drawingCache.MapCache.Children.Add(mAP);
                        break;
                    case "Wall":
                        GeometryDrawing wALL = new GeometryDrawing(this.textureCache.WallBrush, this.textureCache.BlackBorder, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(wALL);
                        break;
                    case "Crate":
                        GeometryDrawing cRATE = new GeometryDrawing(this.textureCache.CrateBrush, this.textureCache.BlackBorder, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(cRATE);
                        break;
                    case "Log":
                        GeometryDrawing lOG = new GeometryDrawing(this.textureCache.LogBrush, this.textureCache.BlackBorder, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(lOG);
                        break;
                    case "Bush":
                        GeometryDrawing bUSH = new GeometryDrawing(this.textureCache.BushBrush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(bUSH);
                        break;
                    case "RedTree":
                        GeometryDrawing rEDTREE = new GeometryDrawing(this.textureCache.RedTreeBrush, null, stat.RealPrimitive);
                        this.drawingCache.TopCache.Children.Add(rEDTREE);
                        break;
                    case "GreenTree":
                        GeometryDrawing gREENTREE = new GeometryDrawing(this.textureCache.GreenTreeBrush, null, stat.RealPrimitive);
                        this.drawingCache.TopCache.Children.Add(gREENTREE);
                        break;
                    case "RoundTree":
                        GeometryDrawing rOUNDTREE = new GeometryDrawing(this.textureCache.RoundTreeBrush, null, stat.RealPrimitive);
                        this.drawingCache.TopCache.Children.Add(rOUNDTREE);
                        break;
                    case "GraveyardArrowSign":
                        GeometryDrawing gRAVEYARDARROWSIGN = new GeometryDrawing(this.textureCache.GraveYardArrowBrush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(gRAVEYARDARROWSIGN);
                        break;
                    case "DirtyRoad":
                        this.textureCache.DirtyRoadBrush.Opacity = 0.8;
                        GeometryDrawing dIRTYROAD = new GeometryDrawing(this.textureCache.DirtyRoadBrush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(dIRTYROAD);
                        break;
                    case "GraveWall1":
                        GeometryDrawing gRAVEWALL1 = new GeometryDrawing(this.textureCache.GraveWall1Brush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(gRAVEWALL1);
                        break;
                    case "GraveWall3":
                        GeometryDrawing gRAVEWALL3 = new GeometryDrawing(this.textureCache.GraveWall3Brush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(gRAVEWALL3);
                        break;
                    case "GraveWall5":
                        GeometryDrawing gRAVEWALL5 = new GeometryDrawing(this.textureCache.GraveWall5Brush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(gRAVEWALL5);
                        break;
                    case "GraveWall7":
                        GeometryDrawing gRAVEWALL7 = new GeometryDrawing(this.textureCache.GraveWall7Brush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(gRAVEWALL7);
                        break;
                    case "GraveFloor":
                        GeometryDrawing gRAVEFLOOR = new GeometryDrawing(this.textureCache.GraveFloorBrush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(gRAVEFLOOR);
                        break;
                    case "TombStone1":
                        GeometryDrawing tOMBSTONE1 = new GeometryDrawing(this.textureCache.GraveTomb1Brush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(tOMBSTONE1);
                        break;
                    case "TombStone2":
                        GeometryDrawing tOMBSTONE2 = new GeometryDrawing(this.textureCache.GraveTomb2Brush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(tOMBSTONE2);
                        break;
                    case "Bone1":
                        GeometryDrawing bONE1 = new GeometryDrawing(this.textureCache.GraveBone1Brush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(bONE1);
                        break;
                    case "Bone2":
                        GeometryDrawing bONE2 = new GeometryDrawing(this.textureCache.GraveBone2Brush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(bONE2);
                        break;
                    case "Skeleton":
                        GeometryDrawing sKELETON = new GeometryDrawing(this.textureCache.GraveSkeletonBrush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(sKELETON);
                        break;
                    case "GraveDirt":
                        GeometryDrawing gRAVEDIRT = new GeometryDrawing(this.textureCache.GraveDirtBrush, null, stat.RealPrimitive);
                        this.drawingCache.MapCache.Children.Add(gRAVEDIRT);
                        break;
                    case "GraveyardSign":
                        GeometryDrawing gRAVESIGN = new GeometryDrawing(this.textureCache.GraveYardSignBrush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(gRAVESIGN);
                        break;
                    case "GraveRoof":
                        GeometryDrawing gRAVEROOF = new GeometryDrawing(this.textureCache.GraveRoofBrush, null, stat.RealPrimitive);
                        this.drawingCache.TopCache.Children.Add(gRAVEROOF);
                        break;
                    case "Tree1":
                        GeometryDrawing tREE1 = new GeometryDrawing(this.textureCache.Tree1Brush, null, stat.RealPrimitive);
                        this.drawingCache.TopCache.Children.Add(tREE1);
                        break;
                    case "Tree2":
                        GeometryDrawing tREE2 = new GeometryDrawing(this.textureCache.Tree2Brush, null, stat.RealPrimitive);
                        this.drawingCache.TopCache.Children.Add(tREE2);
                        break;
                    case "Tree3":
                        GeometryDrawing tREE3 = new GeometryDrawing(this.textureCache.Tree3Brush, null, stat.RealPrimitive);
                        this.drawingCache.TopCache.Children.Add(tREE3);
                        break;
                    case "flower1":
                        GeometryDrawing fLOWER1 = new GeometryDrawing(this.textureCache.Flower1Brush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(fLOWER1);
                        break;
                    case "flower2":
                        GeometryDrawing fLOWER2 = new GeometryDrawing(this.textureCache.Flower2Brush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(fLOWER2);
                        break;
                    case "Well":
                        GeometryDrawing wELL = new GeometryDrawing(this.textureCache.WellBrush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(wELL);
                        break;
                    case "ForestChair":
                        GeometryDrawing fORESTCHAIR = new GeometryDrawing(this.textureCache.ForestChairBrush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(fORESTCHAIR);
                        break;
                    case "ForestChairFlipped":
                        GeometryDrawing fORESTCHAIRFLIPPED = new GeometryDrawing(this.textureCache.ForestChairFlippedBrush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(fORESTCHAIRFLIPPED);
                        break;
                    case "ForestBuilding":
                        GeometryDrawing fORESTBUILDING = new GeometryDrawing(this.textureCache.ForestBuildingBrush, null, stat.RealPrimitive);
                        this.drawingCache.TopCache.Children.Add(fORESTBUILDING);
                        break;
                    case "MazeBush":
                        GeometryDrawing mAZEBUSH = new GeometryDrawing(this.textureCache.MazeBushBrush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(mAZEBUSH);
                        break;
                    case "Pacman":
                        GeometryDrawing pACMAN = new GeometryDrawing(this.textureCache.PacmanBrush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(pACMAN);
                        break;
                    case "PacmanGhost":
                        GeometryDrawing pACMANGHOST = new GeometryDrawing(this.textureCache.PacmanGhostBrush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(pACMANGHOST);
                        break;
                    case "Ferrari":
                        GeometryDrawing fERRARI = new GeometryDrawing(this.textureCache.FerrariBrush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(fERRARI);
                        break;
                    case "MazeChair":
                        GeometryDrawing mAZECHAiR = new GeometryDrawing(this.textureCache.MazeChairBrush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(mAZECHAiR);
                        break;
                    case "MazeTv":
                        GeometryDrawing mAZETV = new GeometryDrawing(this.textureCache.MazeTVBrush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(mAZETV);
                        break;
                    case "MazeArtifact":
                        GeometryDrawing mAZEARTIFACT = new GeometryDrawing(this.textureCache.MazeArtifactBrush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(mAZEARTIFACT);
                        break;
                    case "Desert":
                        GeometryDrawing dESERT = new GeometryDrawing(this.textureCache.DesertBrush, null, stat.RealPrimitive);
                        this.drawingCache.MapCache.Children.Add(dESERT);
                        break;
                    case "Blitz":
                        GeometryDrawing bLITZ = new GeometryDrawing(this.textureCache.BlitzBrush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(bLITZ);
                        break;
                    case "Eagles":
                        GeometryDrawing eAGLES = new GeometryDrawing(this.textureCache.EaglesBrush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(eAGLES);
                        break;
                    case "Cliff1":
                        GeometryDrawing cLIFF1 = new GeometryDrawing(this.textureCache.Cliff1Brush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(cLIFF1);
                        break;
                    case "Cliff2":
                        GeometryDrawing cLIFF2 = new GeometryDrawing(this.textureCache.Cliff2Brush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(cLIFF2);
                        break;
                    case "NFLBall":
                        GeometryDrawing nFLBALL = new GeometryDrawing(this.textureCache.NflBallBrush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(nFLBALL);
                        break;
                    case "Rock1":
                        GeometryDrawing rOCK1 = new GeometryDrawing(this.textureCache.Rock1Brush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(rOCK1);
                        break;
                    case "Rock2":
                        GeometryDrawing rOCK2 = new GeometryDrawing(this.textureCache.Rock2Brush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(rOCK2);
                        break;
                    case "Rock3":
                        GeometryDrawing rOCK3 = new GeometryDrawing(this.textureCache.Rock3Brush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(rOCK3);
                        break;
                    case "Rock4":
                        GeometryDrawing rOCK4 = new GeometryDrawing(this.textureCache.Rock4Brush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(rOCK4);
                        break;
                    case "Stargate":
                        GeometryDrawing sTARGATE = new GeometryDrawing(this.textureCache.StargateBrush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(sTARGATE);
                        break;
                    case "Car1":
                        GeometryDrawing cAR1 = new GeometryDrawing(this.textureCache.Car1Brush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(cAR1);
                        break;
                    case "Car2":
                        GeometryDrawing cAR2 = new GeometryDrawing(this.textureCache.Car2Brush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(cAR2);
                        break;
                    case "Car3":
                        GeometryDrawing cAR3 = new GeometryDrawing(this.textureCache.Car3Brush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(cAR3);
                        break;
                    case "Car4":
                        GeometryDrawing cAR4 = new GeometryDrawing(this.textureCache.Car4Brush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(cAR4);
                        break;
                    case "Car5":
                        GeometryDrawing cAR5 = new GeometryDrawing(this.textureCache.Car5Brush, null, stat.RealPrimitive);
                        this.drawingCache.MiddleCache.Children.Add(cAR5);
                        break;
                    case "Cactus1":
                        GeometryDrawing cACTUS1 = new GeometryDrawing(this.textureCache.Cactus1Brush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(cACTUS1);
                        break;
                    case "Cactus2":
                        GeometryDrawing cACTUS2 = new GeometryDrawing(this.textureCache.Cactus2Brush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(cACTUS2);
                        break;
                    case "Cactus3":
                        GeometryDrawing cACTUS3 = new GeometryDrawing(this.textureCache.Cactus3Brush, null, stat.RealPrimitive);
                        this.drawingCache.GroundCache.Children.Add(cACTUS3);
                        break;
                    case "Line":
                        GeometryDrawing lINE = new GeometryDrawing(Brushes.Transparent, new Pen(Brushes.Black, 1), stat.RealPrimitive);
                        this.drawingCache.MapCache.Children.Add(lINE);
                        break;
                    case "cloudFirst":
                        GeometryDrawing cLOUDONE = new GeometryDrawing(this.textureCache.CloudFirstBrush, null, stat.RealPrimitive);
                        this.drawingCache.AtmosphereCache.Children.Add(cLOUDONE);
                        break;
                    case "cloudSecond":
                        GeometryDrawing cLOUDTWO = new GeometryDrawing(this.textureCache.CloudSecondBrush, null, stat.RealPrimitive);
                        this.drawingCache.AtmosphereCache.Children.Add(cLOUDTWO);
                        break;
                    case "cloudThird":
                        GeometryDrawing cLOUDTHREE = new GeometryDrawing(this.textureCache.CloudFirstBrush, null, stat.RealPrimitive);
                        this.drawingCache.AtmosphereCache.Children.Add(cLOUDTHREE);
                        break;
                    case "Dirt1":
                        GeometryDrawing dIRTONE = new GeometryDrawing(this.textureCache.DirtFirstBrush, null, stat.RealPrimitive);
                        this.drawingCache.MapCache.Children.Add(dIRTONE);
                        break;
                    case "Dirt2":
                        GeometryDrawing dIRTWO = new GeometryDrawing(this.textureCache.DirtSecondBrush, null, stat.RealPrimitive);
                        this.drawingCache.MapCache.Children.Add(dIRTWO);
                        break;
                    default:
                        break;
                }
            }

            foreach (var crate in this.model.Crates)
            {
                GeometryDrawing crateDrawing = new GeometryDrawing(this.textureCache.CrateBrush, null, crate.RealPrimitive);
                this.drawingCache.MiddleCache.Children.Add(crateDrawing);
            }

            this.drawingCache.RenderMap();
            this.drawingCache.RenderGround();
            this.drawingCache.RenderMiddle();
            this.drawingCache.RenderTop();
            this.drawingCache.RenderAtmosphere();
            this.drawingCache.MiddleCache.Opacity = 0.95;
        }

        /// <summary>
        /// Element draw method.
        /// </summary>
        /// <param name="context">Drawing context.</param>
        internal void DrawElements(DrawingContext context)
        {
            DrawingGroup dGroup = new DrawingGroup();

            dGroup.Transform = new TranslateTransform(-(this.model.MyPlayer.Position.X - this.xCenter), -(this.model.MyPlayer.Position.Y - this.yCenter));

            dGroup.Children.Add(this.drawingCache.MapInstance);
            dGroup.Children.Add(this.drawingCache.GroundInstance);
            dGroup.Children.Add(this.drawingCache.MiddleInstance);

            foreach (var weapon in this.model.Weapons)
            {
                dGroup.Children.Add(this.WeaponDrawing((IWeapon)weapon));
            }

            foreach (var bullet in this.model.Bullets)
            {
                GeometryDrawing bullGeom = new GeometryDrawing(this.textureCache.BulletBrush, this.textureCache.BlackBorder, bullet.RealPrimitive);
                dGroup.Children.Add(bullGeom);
            }

            foreach (var player in this.model.Players)
            {
                var iplayer = player as IPlayer;
                ImageBrush texture = this.textureCache.PlayerBrush.Clone();
                if (iplayer.CurrentWeapon != null)
                {
                    switch (iplayer.CurrentWeapon.WeaponInfo[0])
                    {
                        case 8:
                            texture = this.textureCache.PlayerShotgunBrush.Clone();
                            break;
                        case 20:
                            texture = this.textureCache.PlayerPistolBrush.Clone();
                            break;
                        case 30:
                            texture = this.textureCache.PlayerRifleBrush.Clone();
                            break;
                    }
                }

                Geometry primitive = iplayer.RealPrimitive;
                texture.Transform = primitive.Transform;
                GeometryDrawing playerPrimitive = new GeometryDrawing(texture, null, primitive);
                dGroup.Children.Add(playerPrimitive);
                dGroup.Children.Add(this.textureCache.DrawTextUp(primitive.Bounds, this.textureCache.GenerateName(iplayer.Id, iplayer.Name, 18), iplayer.Name, 18));
            }

            dGroup.Children.Add(this.drawingCache.TopInstance);
            dGroup.Children.Add(this.DrawWeaponStat());
            var clouds = this.drawingCache.AtmosphereInstance;
            clouds.Geometry.Transform = new TranslateTransform(-(this.model.MyPlayer.Position.X - this.xCenter) / 5, -(this.model.MyPlayer.Position.Y - this.yCenter) / 5);
            dGroup.Children.Add(clouds);
            dGroup.Children.Add(this.DrawHealthBar());

            context.DrawDrawing(dGroup);
        }

        private GeometryDrawing DrawWeaponStat()
        {
            DrawingGroup dGroup = new DrawingGroup();
            Vector pos = this.model.MyPlayer.Position;
            double pWidth = this.xCenter * 0.25;
            double pHeight = this.yCenter * 0.4;
            Rect panel = new Rect(pos.X + (this.xCenter * 0.75) - 20, pos.Y + (this.yCenter * 0.6) - 20, pWidth, pHeight);
            Rect weaponBulletPanel = new Rect(panel.X + (panel.Width * 0.02), panel.Y + (panel.Height * 0.5), panel.Width * 0.96, panel.Height * 0.2);
            Rect storedBulletPanel = new Rect(panel.X + (panel.Width * 0.02), panel.Y + (panel.Height * 0.8), panel.Width * 0.96, panel.Height * 0.1);
            dGroup.Children.Add(new GeometryDrawing(Brushes.DarkGreen, this.textureCache.BlackBorder, new RectangleGeometry(panel)));
            dGroup.Children.Add(new GeometryDrawing(Brushes.Brown, this.textureCache.BlackBorder, new RectangleGeometry(weaponBulletPanel)));
            dGroup.Children.Add(new GeometryDrawing(Brushes.DarkRed, this.textureCache.BlackBorder, new RectangleGeometry(storedBulletPanel)));

            if (this.model.MyPlayer.CurrentWeapon != null)
            {
                double[] parameters = this.model.MyPlayer.CurrentWeapon.WeaponInfo;
                switch (parameters[0])
                {
                    case 8:
                        dGroup.Children.Add(new GeometryDrawing(this.textureCache.ShotgunSideBrush, null, new RectangleGeometry(new Rect(panel.X, panel.Y, panel.Width, panel.Height * 0.5))));
                        break;
                    case 20:
                        dGroup.Children.Add(new GeometryDrawing(this.textureCache.PistolSideBrush, null, new RectangleGeometry(new Rect(panel.X, panel.Y, panel.Width, panel.Height * 0.5))));
                        break;
                    case 30:
                        dGroup.Children.Add(new GeometryDrawing(this.textureCache.RifleSideBrush, null, new RectangleGeometry(new Rect(panel.X, panel.Y, panel.Width, panel.Height * 0.5))));
                        break;
                }

                dGroup.Children.Add(new GeometryDrawing(this.textureCache.DrawWeaponBullets((int)parameters[5], (int)parameters[0], "Weapon "), null, new RectangleGeometry(weaponBulletPanel)));
            }

            dGroup.Children.Add(new GeometryDrawing(this.textureCache.DrawBullets(this.model.MyPlayer.StoredBullets, "Stored Bullets: "), null, new RectangleGeometry(storedBulletPanel)));
            DrawingBrush dBrush = new DrawingBrush();
            dBrush.Drawing = dGroup;
            dBrush.Opacity = 0.7;
            return new GeometryDrawing(dBrush, null, new RectangleGeometry(panel));
        }
    }
}
