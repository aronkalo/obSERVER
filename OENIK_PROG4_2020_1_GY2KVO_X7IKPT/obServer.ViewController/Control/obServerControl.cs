using obServer.Logic;
using obServer.Model.GameModel;
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

namespace obServer.ViewController.Control
{
    class obServerControl : FrameworkElement
    {
        public obServerControl()
        {
            Loaded += Window_Loaded;
            om = new obServerModel(1000,1000);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window win = Window.GetWindow(this);
            if (win != null)
            {
                dt = new DispatcherTimer();
                dt.Interval = TimeSpan.FromMilliseconds(10);
                sw = new Stopwatch();
                dt.Tick += Update;
                win.KeyDown += KeyHit;
                win.MouseMove += MouseMovement;
                win.MouseLeftButtonDown += LeftMouseClick;
                playerArgs = new PlayerInputEventArgs() { Player = om.MyPlayer };
                PlayerInput += cl.OnShoot;
                PlayerInput += cl.OnMove;
                PlayerInput += cl.OnReload;
                PlayerInput += cl.OnPickup;
                cl.UpdateUI += (obj, args) => InvalidateVisual();
                InvalidateVisual();
            }
        }

        private void LeftMouseClick(object sender, MouseButtonEventArgs e)
        {
            playerArgs.Shoot = true;
        }

        private void MouseMovement(object sender, MouseEventArgs e)
        {
            Point mp = e.GetPosition(this);
            double angle = Vector.AngleBetween(new Vector(0, -1), new Vector(mp.X - this.ActualWidth, mp.Y - this.ActualHeight));
            playerArgs.Angle = angle;
        }

        private void KeyHit(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    playerArgs.Movement[0] = 1;
                    break;
                case Key.S:
                    playerArgs.Movement[0] = -1;
                    break;
                case Key.A:
                    playerArgs.Movement[1] = -1;
                    break;
                case Key.D:
                    playerArgs.Movement[1] = 1;
                    break;
                case Key.R:
                    playerArgs.Reload = true;
                    break;
                case Key.F:
                    playerArgs.Pickup = true;
                    break;
            }
        }

        private void Update(object sender, EventArgs e)
        {
            double deltaTime = sw.Elapsed.TotalSeconds; sw.Restart();
            ServerUpdate(deltaTime);
            playerArgs.deltaTime = deltaTime;
            PlayerInput?.Invoke(this, playerArgs);
            playerArgs = new PlayerInputEventArgs() { Player = om.MyPlayer };
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
    }
}
