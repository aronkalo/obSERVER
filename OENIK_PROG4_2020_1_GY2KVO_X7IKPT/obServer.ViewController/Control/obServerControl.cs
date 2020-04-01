using obServer.Logic;
using obServer.Model.GameModel;
using obServer.Model.GameModel.Item;
using obServer.Model.Interfaces;
using obServer.ViewController.Render;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;

namespace obServer.ViewController.Control
{
    class obServerControl : FrameworkElement
    {
        public obServerControl()
        {
            Loaded += Window_Loaded;
            om = new obServerModel(5000, 5000);
            sl = new ServerLogic(5000, 5000);
            cl = new ClientLogic(om);
            or = new obServerRenderer(om);
        }

        private DispatcherTimer dt;
        private ClientLogic cl;
        private ServerLogic sl;
        private obServerRenderer or;
        private IobServerModel om;
        private Stopwatch sw;
        private event EventHandler<PlayerInputEventArgs> PlayerInput;
        private static PlayerInputEventArgs playerArgs;
        private double fpsCache = 50;

        private double xCenter 
        {
            get
            {
                return ActualWidth / 2;
            }
                
        }

        private double yCenter
        {
            get
            {
                return ActualHeight / 2;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window win = Window.GetWindow(this);
            if (win != null)
            {
                dt = new DispatcherTimer();
                dt.Interval = TimeSpan.FromMilliseconds(8);
                sw = new Stopwatch();
                dt.Tick += Update;

                win.SizeChanged += Resized;
                win.KeyDown += KeyHit;
                win.KeyUp += KeyRelease;
                win.MouseMove += MouseMovement;
                win.MouseLeftButtonDown += LeftMouseClick;
                win.MouseRightButtonDown += RightMouseClick;

                playerArgs = new PlayerInputEventArgs() { Player = om.MyPlayer };

                PlayerInput += cl.OnShoot;
                PlayerInput += cl.OnMove;
                PlayerInput += cl.OnReload;
                PlayerInput += cl.OnPickup;
                cl.UpdateUI += (obj, args) => InvalidateVisual();

                or.SetOffsets(xCenter, yCenter);
                InvalidateVisual();

                dt.Start();
            }
        }

        private void RightMouseClick(object sender, MouseButtonEventArgs e)
        {
            var p = e.MouseDevice.GetPosition(this);
            p.X += (om.MyPlayer.Position[0] - xCenter);
            p.Y += (om.MyPlayer.Position[1] - yCenter);
            var colls = om.Statics.Where(x => (x as IStaticItem).Type != "Map" && x.RealPrimitive.Bounds.Contains(p));
            if (colls.Count() > 0)
            {
                om.DestructItem(colls.First().Id);
            }
        }

        private void Resized(object sender, SizeChangedEventArgs e)
        {
            or.SetOffsets(xCenter, yCenter);
        }

        private void LeftMouseClick(object sender, MouseButtonEventArgs e)
        {
            if (playerArgs== null)
            {
                playerArgs = new PlayerInputEventArgs() { Player = om.MyPlayer };
            }
            playerArgs.Shoot = true;
            var mousePoint = e.MouseDevice.GetPosition(this);
            mousePoint.X += (om.MyPlayer.Position[0] - xCenter);
            mousePoint.Y += (om.MyPlayer.Position[1] - yCenter);
            AddItems(mousePoint);
        }

        private void MouseMovement(object sender, MouseEventArgs e)
        {
            if (playerArgs == null)
            {
                playerArgs = new PlayerInputEventArgs() { Player = om.MyPlayer };
            }
            Point mp = e.GetPosition(this);
            double xmouseRelativeToCenter = mp.X - xCenter;
            double ymouseRelativeToCenter = mp.Y - yCenter;
            double angle = Vector.AngleBetween(new Vector(0, -1), new Vector(xmouseRelativeToCenter, ymouseRelativeToCenter));
            playerArgs.Angle = angle;
        }

        private void KeyRelease(object sender, KeyEventArgs e)
        {
            if (playerArgs == null)
            {
                playerArgs = new PlayerInputEventArgs() { Player = om.MyPlayer };
            }
            switch (e.Key)
            {
                case Key.W:
                    playerArgs.Movement[1] = 0;
                    break;
                case Key.S:
                    playerArgs.Movement[1] = 0;
                    break;
                case Key.A:
                    playerArgs.Movement[0] = 0;
                    break;
                case Key.D:
                    playerArgs.Movement[0] = 0;
                    break;
                case Key.R:
                    playerArgs.Reload = false;
                    break;
                case Key.F:
                    playerArgs.Pickup = false;
                    break;
            }
        }

        private void KeyHit(object sender, KeyEventArgs e)
        {
            if (playerArgs == null)
            {
                playerArgs = new PlayerInputEventArgs() { Player = om.MyPlayer };
            }
            switch (e.Key)
            {
                case Key.W:
                    playerArgs.Movement[1] = -1;
                    break;
                case Key.S:
                    playerArgs.Movement[1] = 1;
                    break;
                case Key.A:
                    playerArgs.Movement[0] = -1;
                    break;
                case Key.D:
                    playerArgs.Movement[0] = 1;
                    break;
                case Key.R:
                    //playerArgs.Reload = true;
                    break;
                case Key.F:
                    //playerArgs.Pickup = true;
                    break;
                case Key.Escape:
                    SaveXDoc();
                    break;
            }
        }

        private void Update(object sender, EventArgs e)
        {
            double deltaTime = sw.Elapsed.TotalSeconds; sw.Restart();
            if (deltaTime > 0.0001)
            {
                fpsCache = ((1 / deltaTime) + fpsCache) / 2;
            }
            Debug.WriteLine("AVG FPS: " + fpsCache);
            ServerUpdate(deltaTime);
            playerArgs.deltaTime = deltaTime;
            if (playerArgs != null)
            {
                PlayerInput?.Invoke(this, playerArgs);
                playerArgs = null;
            }
            cl.FlyBullets(deltaTime);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (or != null)
            {
                or.DrawElements(drawingContext);
            }
        }

        private void ServerUpdate(double deltaTime)
        {
            if (sl != null)
            {
            }
        }

        private void SaveXDoc()
        {
            XDocument xDoc = new XDocument(new XElement("items"));
            foreach (var item in om.Statics)
            {
                var stat = (IStaticItem)item;
                var xelem = new XElement("item");
                xelem.Add(new XAttribute("type", stat.Type));
                xelem.Add(new XAttribute("id", stat.Id));
                xelem.Add(new XElement("x", stat.Position[0]));
                xelem.Add(new XElement("y", stat.Position[1]));
                xelem.Add(new XElement("width", stat.Dimensions[0]));
                xelem.Add(new XElement("height", stat.Dimensions[1]));
                xelem.Add(new XElement("angle", stat.Rotation));
                xDoc.Root.Add(xelem);
            }
            xDoc.Save("Map.xml");
        }

        private void AddItems(Point p)
        {
            Random r = new Random();
            int dim = r.Next(250, 350);
            int wall = 70;
            int crate = 50;
            if (Keyboard.IsKeyDown(Key.E))
            {
                om.ConstructItem(new StaticItem(new RectangleGeometry() {Rect = new Rect(0, 0, dim, dim)},Guid.NewGuid(),new double[]{ p.X - (dim / 2),p.Y - (dim / 2)},0,new double[] {dim,dim },false,"RedTree"));
            }
            else if (Keyboard.IsKeyDown(Key.R))
            {
                om.ConstructItem(new StaticItem(new RectangleGeometry(){ Rect = new Rect(0, 0, dim, dim) },Guid.NewGuid(),new double[] { p.X - (dim / 2), p.Y - (dim / 2) },0,new double[] { dim, dim },false,"GreenTree"));
            }
            else if (Keyboard.IsKeyDown(Key.T))
            {
                om.ConstructItem(new StaticItem(new RectangleGeometry(){ Rect = new Rect(0, 0, dim, dim) },Guid.NewGuid(),new double[] { p.X - (dim / 2), p.Y - (dim / 2) },0,new double[] { dim, dim },false,"RoundTree"));
            }
            else if (Keyboard.IsKeyDown(Key.Z))
            {
                om.ConstructItem(new StaticItem(new RectangleGeometry() { Rect = new Rect(0, 0, wall, wall) }, Guid.NewGuid(), new double[] { p.X - (wall / 2), p.Y - (wall / 2) }, 0, new double[] { wall, wall }, true, "Crate"));
            }
            else if (Keyboard.IsKeyDown(Key.U))
            {
                om.ConstructItem(new StaticItem(new RectangleGeometry() { Rect = new Rect(0, 0, crate, crate) }, Guid.NewGuid(), new double[] { p.X - (crate / 2), p.Y - (crate / 2) }, 0, new double[] { crate, crate }, true, "Wall"));
            }
        }
    }
}
