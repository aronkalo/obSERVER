// <copyright file="RenderCache.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.ViewController.Render.Texture
{
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Rendercache class.
    /// </summary>
    public class RenderCache
    {
        private const int XScale = 5000;
        private const int YScale = 5000;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderCache"/> class.
        /// </summary>
        public RenderCache()
        {
            this.AtmosphereCache = new DrawingGroup();
            this.AtmosphereInstance = new GeometryDrawing();
            this.MapCache = new DrawingGroup();
            this.MapInstance = new GeometryDrawing();
            this.GroundCache = new DrawingGroup();
            this.GroundInstance = new GeometryDrawing();
            this.MiddleCache = new DrawingGroup();
            this.MiddleInstance = new GeometryDrawing();
            this.TopCache = new DrawingGroup();
            this.TopInstance = new GeometryDrawing();
            this.UICache = new DrawingGroup();
        }

        /// <summary>
        /// Gets or sets atmospehere cache.
        /// </summary>
        public DrawingGroup AtmosphereCache { get; set; }

        /// <summary>
        /// Gets or sets atmosphere instance.
        /// </summary>
        public GeometryDrawing AtmosphereInstance { get; set; }

        /// <summary>
        /// Gets or sets mapcache.
        /// </summary>
        public DrawingGroup MapCache { get; set; }

        /// <summary>
        /// Gets or sets map isntance.
        /// </summary>
        public GeometryDrawing MapInstance { get; set; }

        /// <summary>
        /// Gets or sets ground cache.
        /// </summary>
        public DrawingGroup GroundCache { get; set; }

        /// <summary>
        /// Gets or sets ground instance.
        /// </summary>
        public GeometryDrawing GroundInstance { get; set; }

        /// <summary>
        /// Gets or sets middle cache.
        /// </summary>
        public DrawingGroup MiddleCache { get; set; }

        /// <summary>
        /// Gets or sets middle instance.
        /// </summary>
        public GeometryDrawing MiddleInstance { get; set; }

        /// <summary>
        /// Gets or sets top cache.
        /// </summary>
        public DrawingGroup TopCache { get; set; }

        /// <summary>
        /// Gets or sets top isntance.
        /// </summary>
        public GeometryDrawing TopInstance { get; set; }

        /// <summary>
        /// Gets or sets ui cache.
        /// </summary>
        public DrawingGroup UICache { get; set; }

        /// <summary>
        /// Rendermap method.
        /// </summary>
        public void RenderMap()
        {
            DrawingBrush dBrush = new DrawingBrush();
            dBrush.Drawing = this.MapCache;
            this.MapInstance = new GeometryDrawing(dBrush, null, new RectangleGeometry(new Rect(-XScale, -YScale, XScale * 3, YScale * 3)));
        }

        /// <summary>
        /// Renderground method.
        /// </summary>
        public void RenderGround()
        {
            DrawingBrush dBrush = new DrawingBrush();
            dBrush.Drawing = this.GroundCache;
            this.GroundInstance = new GeometryDrawing(dBrush, null, new RectangleGeometry(new Rect(0, 0, XScale, YScale)));
        }

        /// <summary>
        /// Rendermiddle method.
        /// </summary>
        public void RenderMiddle()
        {
            DrawingBrush dBrush = new DrawingBrush();
            dBrush.Drawing = this.MiddleCache;
            this.MiddleInstance = new GeometryDrawing(dBrush, null, new RectangleGeometry(new Rect(-70, -70, XScale + 140, YScale + 140)));
        }

        /// <summary>
        /// Rendertop method.
        /// </summary>
        public void RenderTop()
        {
            DrawingBrush dBrush = new DrawingBrush();
            dBrush.Drawing = this.TopCache;
            this.TopInstance = new GeometryDrawing(dBrush, null, new RectangleGeometry(new Rect(0, 0, XScale, YScale)));
        }

        /// <summary>
        /// RenderAtmosphere method.
        /// </summary>
        public void RenderAtmosphere()
        {
            DrawingBrush dBrush = new DrawingBrush();
            dBrush.Drawing = this.AtmosphereCache;
            dBrush.Opacity = 0.6;
            this.AtmosphereInstance = new GeometryDrawing(dBrush, null, new RectangleGeometry(new Rect(-500, -500, 6000, 6000)));
        }
    }
}
