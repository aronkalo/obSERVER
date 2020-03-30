using obServer.Logic;
using obServer.Model.GameModel;
using obServer.Model.Interfaces;
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
            om = new obServerModel();
            cl = new ClientLogic(om);
        }
        private DispatcherTimer dt;
        private ClientLogic cl;
        private IobServerModel om;
        private Stopwatch sw;
        private event EventHandler<MovementEventArgs> Movement;
        private event EventHandler<ShootEventArgs> Shoot;
        private event EventHandler<TacticalEventArgs> Tactic;
        private static MovementEventArgs movementArgs;
        private static ShootEventArgs shootArgs;

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
                win.MouseLeftButtonDown += MouseClick;
                movementArgs = new MovementEventArgs();
                Movement += cl.OnMovement;
                Shoot += cl.OnShoot;
                Tactic += cl.OnTactic;
            }
        }

        private void MouseClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MouseMovement(object sender, MouseEventArgs e)
        {
            Point mp = e.GetPosition(this);
            double angle = Vector.AngleBetween(new Vector(0, -1), new Vector(mp.X - this.ActualWidth, mp.Y - this.ActualHeight));
            movementArgs.Angle = angle;
        }

        private void KeyHit(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    movementArgs.Movement[0] = 1;
                    break;
                case Key.S:
                    movementArgs.Movement[0] = -1;
                    break;
                case Key.A:
                    movementArgs.Movement[1] = -1;
                    break;
                case Key.D:
                    movementArgs.Movement[1] = 1;
                    break;
                case Key.R:
                    break;
                case Key.F:
                    break;
            }
        }

        private void Update(object sender, EventArgs e)
        {
            
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            double deltaTime = sw.Elapsed.TotalSeconds;

        }

    }
}
