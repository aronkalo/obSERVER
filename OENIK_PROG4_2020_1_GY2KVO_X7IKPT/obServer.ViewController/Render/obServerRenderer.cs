using obServer.Model.Interfaces;
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
        public obServerRenderer(IobServerModel model)
        {
            this.model = model;
            drawingCache = new RenderCache();
            textureCache = new BrushCache();
            DrawStatic();
        }
        private RenderCache drawingCache;
        private BrushCache textureCache;
        private IobServerModel model;
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
            tg.Children.Add(new TranslateTransform(-(model.MyPlayer.Position.X - width), -(model.MyPlayer.Position.Y - height)));
            dGroup.Transform = tg;

            foreach (var Map in drawingCache.MapCache.Children)
            {
                dGroup.Children.Add(Map);
            }

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
            foreach (var Static in drawingCache.MiddleCache.Children)
            {
                dGroup.Children.Add(Static);
            }
            foreach (var Static in drawingCache.TopCache.Children)
            {
                dGroup.Children.Add(Static);
            }
            dGroup.Children.Add(DrawHealthBar());
            //dGroup.Children.Add(DrawBullets());
            context.DrawDrawing(dGroup);
            GC.Collect();
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
                    default:
                        break;
                }
            }
        }
    }
}
