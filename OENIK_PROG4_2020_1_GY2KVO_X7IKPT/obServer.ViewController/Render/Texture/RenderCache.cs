using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace obServer.ViewController.Render.Texture
{
    public class RenderCache
    {
        private const int xScale = 5000;
        private const int yScale = 5000;
        public RenderCache()
        {
            AtmosphereCache = new DrawingGroup();
            AtmosphereInstance = new GeometryDrawing();
            MapCache = new DrawingGroup();
            MapInstance = new GeometryDrawing();
            GroundCache = new DrawingGroup();
            GroundInstance = new GeometryDrawing();
            MiddleCache = new DrawingGroup();
            MiddleInstance = new GeometryDrawing();
            TopCache = new DrawingGroup();
            TopInstance = new GeometryDrawing();
            UICache = new DrawingGroup();
        }

        public void RenderMap()
        {
            DrawingBrush dBrush = new DrawingBrush();
            dBrush.Drawing = MapCache;
            MapInstance = new GeometryDrawing(dBrush, null, new RectangleGeometry(new Rect(-xScale, -yScale, xScale*3, yScale*3)));
        }

        public void RenderGround()
        {
            DrawingBrush dBrush = new DrawingBrush();
            dBrush.Drawing = GroundCache;
            GroundInstance = new GeometryDrawing(dBrush, null, new RectangleGeometry(new Rect(0, 0, xScale, yScale)));
        }

        public void RenderMiddle()
        {
            DrawingBrush dBrush = new DrawingBrush();
            dBrush.Drawing = MiddleCache;
            MiddleInstance = new GeometryDrawing(dBrush, null, new RectangleGeometry(new Rect(-70, -70, xScale + 140, yScale + 140)));
        }

        public void RenderTop()
        {
            DrawingBrush dBrush = new DrawingBrush();
            dBrush.Drawing = TopCache;
            TopInstance = new GeometryDrawing(dBrush, null, new RectangleGeometry(new Rect(0, 0, xScale, yScale)));
        }

        public void RenderAtmosphere()
        {
            DrawingBrush dBrush = new DrawingBrush();
            dBrush.Drawing = AtmosphereCache;
            dBrush.Opacity = 0.5;
            AtmosphereInstance = new GeometryDrawing(dBrush, null, new RectangleGeometry(new Rect(-500,-500,6000,6000)));
        }

        public DrawingGroup AtmosphereCache { get; set; }

        public GeometryDrawing AtmosphereInstance { get; set; }

        public DrawingGroup MapCache { get; set; } 

        public GeometryDrawing MapInstance { get; set; }

        public DrawingGroup GroundCache { get; set; }

        public GeometryDrawing GroundInstance { get; set; }

        public DrawingGroup MiddleCache { get; set; }

        public GeometryDrawing MiddleInstance { get; set; }

        public DrawingGroup TopCache { get; set; }

        public GeometryDrawing TopInstance { get; set; }

        public DrawingGroup UICache { get; set; }

    }
}
