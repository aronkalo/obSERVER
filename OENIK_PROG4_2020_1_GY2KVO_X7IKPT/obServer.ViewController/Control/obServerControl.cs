using obServer.Logic;
using System;
using System.Collections.Generic;
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
        }
        private DispatcherTimer dt;

        private BaseLogic

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window win = Window.GetWindow(this);
            if (win != null)
            {
                dt = new DispatcherTimer();
                dt.Interval = TimeSpan.FromMilliseconds(10);
                dt.Tick += Update;
                win.KeyDown += KeyHit;
                win.MouseMove += MouseMovement;
                win.MouseLeftButtonDown += Shoot;
                
            }
        }

        private void Shoot(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MouseMovement(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void KeyHit(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Update(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {

        }

    }
}
