using obServer.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace obServer.ViewController.Render
{
    class obServerRenderer
    {
        private IobServerModel Model;

        public obServerRenderer(IobServerModel model)
        {
            this.Model = model;
        }

        internal void DrawElements(System.Windows.Media.DrawingContext ctx)
        {
            DrawingGroup dg = new DrawingGroup();

            //GeometryDrawing background = new GeometryDrawing(Config.BgColor,
            //    new Pen(Config.BorderColor, Config.BorderSize),
            //    new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
            //GeometryDrawing ball = new GeometryDrawing(Config.BallBg,
            //    new Pen(Config.BallLine, 1),
            //    new EllipseGeometry(model.Ball.Area));
            //GeometryDrawing pad = new GeometryDrawing(Config.PadBg,
            //    new Pen(Config.PadLine, 1),
            //    new RectangleGeometry(model.Pad.Area));
            //FormattedText formattedText = new FormattedText(model.Errors.ToString(),
            //    System.Globalization.CultureInfo.CurrentCulture,
            //    FlowDirection.LeftToRight,
            //    new Typeface("Arial"),
            //    16,
            //    Brushes.Black);
            //GeometryDrawing text = new GeometryDrawing(null, new Pen(Brushes.Red, 2),
            //    formattedText.BuildGeometry(new Point(5, 5)));

            //dg.Children.Add(background);
            //dg.Children.Add(ball);
            //dg.Children.Add(pad);
            //dg.Children.Add(text);

            //foreach (Star star in model.Stars)
            //{
            //    GeometryDrawing starGeo = new GeometryDrawing(Config.BallBg, new Pen(Config.BallLine, 1),
            //        star.GetGeometry());
            //    dg.Children.Add(starGeo);
            //}

            ctx.DrawDrawing(dg);
        }
    }
}
