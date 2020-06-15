// <copyright file="ObServerControl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.ViewController.Control
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using System.Xml.Linq;
    using obServer.GameLogic;
    using obServer.GameModel;
    using obServer.GameModel.Interfaces;
    using obServer.GameModel.Item;
    using obServer.Logic.Interfaces;
    using obServer.Repository.GameModel;
    using obServer.ViewController.Render;
    using obServer.ViewController.Sound;

    /// <summary>
    /// The control of the game.
    /// </summary>
    public class ObServerControl : FrameworkElement
    {
        private DispatcherTimer beat;
        private IClientLogic cLogic;
        private ObServerRenderer renderer;
        private IRepoOBServerModel model;
        private Stopwatch bulletTimer;
        private Stopwatch moveTimer;
        private double[] movement;
        private string winner;
        private SoundPlayer soundPlayer;
        private DataSaver dataSaver;
        private bool end;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObServerControl"/> class.
        /// </summary>
        /// <param name="logic">The logic.</param>
        /// <param name="model">The model.</param>
        public ObServerControl(IClientLogic logic, IRepoOBServerModel model)
        {
            this.Loaded += this.Window_Loaded;
            this.model = model;
            this.cLogic = logic;
            this.renderer = new ObServerRenderer(this.model);
            this.renderer.SetOffsets(this.XCenter, this.YCenter);
            this.cLogic.RemoveVisuals();
            this.soundPlayer = new SoundPlayer();
            this.dataSaver = new DataSaver();
        }

        private event EventHandler Move;

        private double XCenter
        {
            get
            {
                return this.ActualWidth / 2;
            }
        }

        private double YCenter
        {
            get
            {
                return this.ActualHeight / 2;
            }
        }

        /// <summary>
        /// Render method.
        /// </summary>
        /// <param name="drawingContext">the context of drawing.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            try
            {
                this.renderer.DrawElements(drawingContext);
            }
            catch (Exception)
            {
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window win = Window.GetWindow(this);
            if (win != null)
            {
                this.beat = new DispatcherTimer();
                this.beat.Interval = TimeSpan.FromMilliseconds(30);
                this.moveTimer = new Stopwatch();
                this.bulletTimer = new Stopwatch();

                this.beat.Tick += this.BulletUpdate;
                this.beat.Tick += this.SoundUpdate;
                this.beat.Tick += this.MoveUpdate;
                this.SetupWindowEvents(win);

                this.Move += this.cLogic.OnMove;
                this.movement = new double[4];
                this.renderer.SetOffsets(this.XCenter, this.YCenter);
                this.cLogic.Render += (obj, args) => this.InvalidateVisual();
                this.cLogic.SoundActive += this.OnVoiceActivity;
                this.cLogic.Death += this.OnDie;
                this.dataSaver.NewMatch($"{this.model.MyPlayer.Name}'s Game", DateTime.Now.ToString());
                this.CheckAlone();

                this.InvalidateVisual();
                this.beat.Start();
            }
        }

        /// <summary>
        /// Die handler.
        /// </summary>
        /// <param name="sender">the sender.</param>
        /// <param name="e">the args.</param>
        private void OnDie(object sender, EventArgs e)
        {
            Tuple<Guid, string> obj = (Tuple<Guid, string>)sender;
            int playerNum = this.model.Players.Count();
            this.dataSaver.SavePlayer(obj.Item2, playerNum + 1);
            if (this.model.Players.Count() > 1)
            {
                if (this.model.MyPlayer.Id == obj.Item1)
                {
                    this.Died();
                }
            }
            else
            {
                string name = (this.model.Players.FirstOrDefault() as IPlayer).Name;
                this.dataSaver.SavePlayer(name, 1);
                this.winner = name;
                this.end = true;
            }
        }

        private void CheckAlone()
        {
            if (this.model.Players.Count() < 2)
            {
                this.OnDie(new Tuple<Guid, string>(this.model.MyPlayer.Id, this.model.MyPlayer.Name), EventArgs.Empty);
            }
        }

        private void Died()
        {
            this.model.MyPlayer = (IPlayer)this.model.Players.First();
            var window = Window.GetWindow(this);
            window.KeyDown -= this.OnWindowKeyDown;
            window.MouseMove -= this.OnMouseMove;
            window.MouseLeftButtonDown -= this.OnMouseLeftButtonDown;
        }

        private void OnVoiceActivity(object sender, EventArgs e)
        {
            double[] soundParams = (double[])sender;
            switch (soundParams[0])
            {
                case 1:
                    this.soundPlayer.MoveSound();
                    break;
                case 2:
                    this.soundPlayer.ShootSound();
                    break;
                case 3:
                    this.soundPlayer.ReloadSound();
                    break;
                case 4:
                    this.soundPlayer.OtherMove = true;
                    this.soundPlayer.OtherMoveBalance = soundParams[1];
                    this.soundPlayer.OtherMoveVolume = soundParams[2];
                    break;
                case 5:
                    this.soundPlayer.OtherShoot = true;
                    this.soundPlayer.OtherShootBalance = soundParams[1];
                    this.soundPlayer.OtherShootVolume = soundParams[2];
                    break;
                default:
                    break;
            }
        }

        private void SetupWindowEvents(Window window)
        {
            window.Closing += this.OnClosing;
            window.SizeChanged += this.OnWindowSizeChanged;
            window.KeyDown += this.OnWindowKeyDown;
            window.KeyUp += this.OnWindowKeyUp;
            window.MouseMove += this.OnMouseMove;
            window.MouseLeftButtonDown += this.OnMouseLeftButtonDown;
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            this.beat.Stop();
            this.cLogic = null;
        }

        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.renderer.SetOffsets(this.XCenter, this.YCenter);
        }

        private void OnWindowKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    this.movement[1] = -1;
                    break;
                case Key.S:
                    this.movement[1] = 1;
                    break;
                case Key.A:
                    this.movement[0] = -1;
                    break;
                case Key.D:
                    this.movement[0] = 1;
                    break;
                case Key.R:
                    this.cLogic.Reload?.Invoke(null, EventArgs.Empty);
                    break;
                case Key.F:
                    this.cLogic.Pick?.Invoke(null, EventArgs.Empty);
                    break;
            }
        }

        private void OnWindowKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    this.movement[1] = 0;
                    break;
                case Key.S:
                    this.movement[1] = 0;
                    break;
                case Key.A:
                    this.movement[0] = 0;
                    break;
                case Key.D:
                    this.movement[0] = 0;
                    break;
            }
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.cLogic.Shoot?.Invoke(null, EventArgs.Empty);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            Point mp = e.GetPosition(this);
            this.movement[2] = Vector.AngleBetween(new Vector(0, 1), new Vector(mp.X - this.XCenter, mp.Y - this.YCenter));
        }

        private void BulletUpdate(object sender, EventArgs e)
        {
            this.beat.Stop();
            try
            {
                double deltaTime = this.bulletTimer.Elapsed.TotalSeconds;
                this.bulletTimer.Restart();
                this.movement[3] = deltaTime;
                this.Move?.Invoke(this.movement, EventArgs.Empty);
                this.cLogic.FlyBullets(deltaTime);
            }
            catch (Exception)
            {
            }

            this.beat.Start();
        }

        private void SoundUpdate(object sender, EventArgs e)
        {
            this.soundPlayer.UpdateSounds();
            if (this.end)
            {
                this.beat.Stop();
                var window = Window.GetWindow(this);
                window.KeyDown -= this.OnWindowKeyDown;
                window.MouseMove -= this.OnMouseMove;
                window.MouseLeftButtonDown -= this.OnMouseLeftButtonDown;
                window.Close();
                MessageBox.Show($"Winner player: {this.winner} \nGame end time: {DateTime.Now.ToString()}", "Match Over", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MoveUpdate(object sender, EventArgs e)
        {
            double deltaTime = this.moveTimer.Elapsed.TotalSeconds;
            this.moveTimer.Restart();
            this.movement[3] = deltaTime;
            Debug.WriteLine(1 / deltaTime);
            this.Move?.Invoke(this.movement, EventArgs.Empty);
        }
    }
}
