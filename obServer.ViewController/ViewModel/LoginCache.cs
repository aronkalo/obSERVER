// <copyright file="LoginCache.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.ViewController.ViewModel
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// LogicCache class.
    /// </summary>
    public class LoginCache
    {
        private static string directory = Directory.GetCurrentDirectory();
        private int playerOneRot;
        private int playerTwoRot;
        private int playerThirdRot;
        private Random rnd;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginCache"/> class.
        /// </summary>
        public LoginCache()
        {
            this.BackBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\startback.png")));
            this.CloudOneBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\cloud1.png")));
            this.CloudTwoBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\cloud2.png")));
            this.PistolBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\pistol.png")));
            this.RifleBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\rifle.png")));
            this.ShotgunBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\shotgun.png")));
            this.LogoBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\logo.png")));
            this.rnd = new Random();
            this.BackBrush.Opacity = 0.8;
            this.PistolBrush.Opacity = 0.8;
            this.CloudOneBrush.Opacity = 0.7;
            this.CloudTwoBrush.Opacity = 0.7;
            this.playerOneRot = 310;
            this.playerTwoRot = 340;
            this.playerThirdRot = 110;
        }

        /// <summary>
        /// Gets or sets backbrush imagebrush.
        /// </summary>
        public ImageBrush BackBrush { get; set; }

        /// <summary>
        /// Gets or sets cloud one brush imagebrush.
        /// </summary>
        public ImageBrush CloudOneBrush { get; set; }

        /// <summary>
        /// Gets or sets cloud two brush imagebrush.
        /// </summary>
        public ImageBrush CloudTwoBrush { get; set; }

        /// <summary>
        /// Gets or sets pistol imagebrush.
        /// </summary>
        public ImageBrush PistolBrush { get; set; }

        /// <summary>
        /// Gets or sets rifle imagebrush.
        /// </summary>
        public ImageBrush RifleBrush { get; set; }

        /// <summary>
        /// Gets or sets shotgun imagebrush.
        /// </summary>
        public ImageBrush ShotgunBrush { get; set; }

        /// <summary>
        /// Gets or sets logo imagebrush.
        /// </summary>
        public ImageBrush LogoBrush { get; set; }

        /// <summary>
        /// Calculate Base method.
        /// </summary>
        /// <returns>Brush.</returns>
        public Brush CalculateBase()
        {
            DrawingBrush dBrush = new DrawingBrush();
            DrawingGroup dGroup = new DrawingGroup();
            dGroup.Children.Add(new GeometryDrawing(this.BackBrush, null, new RectangleGeometry(new Rect(0, 0, 800, 500))));
            dGroup.Children.Add(new GeometryDrawing(this.CloudOneBrush, null, new RectangleGeometry(new Rect(50, 40, 400, 200))));
            dGroup.Children.Add(new GeometryDrawing(this.CloudTwoBrush, null, new RectangleGeometry(new Rect(500, 50, 300, 150))));
            dGroup.Children.Add(new GeometryDrawing(this.CloudOneBrush, null, new RectangleGeometry(new Rect(200, 250, 500, 250))));
            dGroup.Children.Add(new GeometryDrawing(this.LogoBrush, null, new RectangleGeometry(new Rect(320, 70, 400, 100))));
            dBrush.Drawing = dGroup;
            return dBrush;
        }

        /// <summary>
        /// Calculate relative method.
        /// </summary>
        /// <param name="p">Point.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <returns>Brush.</returns>
        public Brush CalculateRelative(Point p, double width, double height)
        {
            double multiplyer = 1.2;
            double[] positions = this.CalculateTransform(p.X, p.Y, width, height);
            DrawingGroup dGroup = new DrawingGroup();
            dGroup.Children.Add(new GeometryDrawing(this.BackBrush, null, new RectangleGeometry(new Rect(0, 0, 800, 500))));
            dGroup.Children.Add(this.CalculatePlayer(300 + (positions[2] * 2), 300 + (positions[3] * 3.5), 80, 80, this.PistolBrush, ref this.playerOneRot));
            dGroup.Children.Add(this.CalculatePlayer(100 + (positions[2] * 1.5), 50 + (positions[3] * -2.1), 80, 80, this.RifleBrush, ref this.playerTwoRot));
            dGroup.Children.Add(this.CalculatePlayer(600 + (positions[2] * -4), 400 + (positions[3] * 6), 80, 80, this.ShotgunBrush, ref this.playerThirdRot));
            dGroup.Children.Add(new GeometryDrawing(this.CloudOneBrush, null, new RectangleGeometry(new Rect(50 + (positions[2] * -1), 40 + (positions[3] * -1), 400, 200))));
            dGroup.Children.Add(new GeometryDrawing(this.CloudOneBrush, null, new RectangleGeometry(new Rect(400 + (positions[2] * -1), 100 + (positions[3] * -1), 300, 150))));
            dGroup.Children.Add(new GeometryDrawing(this.CloudOneBrush, null, new RectangleGeometry(new Rect(200 + (positions[2] * -1), 250 + (positions[3] * -1), 500, 250))));
            dGroup.Children.Add(new GeometryDrawing(this.LogoBrush, null, new RectangleGeometry(new Rect(350 + (positions[2] * -1), 100 + (positions[3] * -1), 350, 90))));
            DrawingBrush rBrush = new DrawingBrush();
            rBrush.Drawing = dGroup;
            rBrush.Transform = new ScaleTransform(multiplyer, multiplyer, positions[0], positions[1]);
            return rBrush;
        }

        private GeometryDrawing CalculatePlayer(double x, double y, double width, double height, Brush origin, ref int rotation)
        {
            Brush cloned = origin.Clone();
            int epoch = this.rnd.Next(-1, 2);
            rotation += epoch;
            cloned.Transform = new RotateTransform(rotation, x + (width / 2), y + (height / 2));
            return new GeometryDrawing(cloned, null, new EllipseGeometry(new Rect(x, y, width, height)));
        }

        private double[] CalculateTransform(double x, double y, double width, double height)
        {
            double xSub = width - x;
            double ySub = height - y;
            double xV = x - xSub;
            double yV = y - ySub;
            return new double[] { (width / 2) + (xV * 0.1), (height / 2) + (yV * 0.1), xV * 0.01, yV * 0.01 };
        }
    }
}
