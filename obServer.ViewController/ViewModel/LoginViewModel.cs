// <copyright file="LoginViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.ViewController.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using obServer.GameLogic;
    using obServer.Logic.Interfaces;
    using obServer.Repository.GameModel;
    using obServer.ViewController.Sound;

    /// <summary>
    /// LoginViewModel class.
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        private bool hostVis;
        private bool connectVis;
        private bool startVis;
        private bool historyVis;
        private string status;
        private string name;
        private Window win;
        private Brush backBrush;
        private DispatcherTimer timer;
        private LoginCache cache;
        private IClientLogic clientLogic;
        private IRepoOBServerModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        /// <param name="model">RepoObserverModel.</param>
        /// <param name="clientLogic">ClientLogic.</param>
        public LoginViewModel(IRepoOBServerModel model, IClientLogic clientLogic)
        {
            this.model = model;
            this.clientLogic = clientLogic;
            this.Logic = new LoginLogic(model, clientLogic);
            this.cache = new LoginCache();
            this.timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000),
            };
            this.timer.Tick += this.Update;
            MusicPlayer.PlaySound(@"\sounds\loginmusic.mp3");
            this.backBrush = this.cache.CalculateBase();

            this.ConnectCmd = new RelayCommand(() => this.ConnectGame());
            this.HistoryCmd = new RelayCommand(() => this.HistoryDialog());
            this.StartCmd = new RelayCommand<Window>((o) => this.StartGame());
            this.HostCmd = new RelayCommand(() => this.HostGame());
            this.ClosingCmd = new RelayCommand(() => this.Closing());
            this.MouseCmd = new RelayCommand<Window>((x) => this.MouseMove(x));

            this.Games = new ObservableCollection<ClientInfo>();
            this.ConnectVis = true;
            this.HostVis = true;
            this.StartVis = false;
            this.HistoryVis = true;
            this.Status = "Not Connected";

            this.timer.Start();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        public LoginViewModel()
            : this(ServiceLocator.Current.GetInstance<IRepoOBServerModel>(), ServiceLocator.Current.GetInstance<IClientLogic>())
        {
        }

        /// <summary>
        /// Gets connect command.
        /// </summary>
        public ICommand ConnectCmd { get; private set; }

        /// <summary>
        /// Gets history command.
        /// </summary>
        public ICommand HistoryCmd { get; private set; }

        /// <summary>
        /// Gets host command.
        /// </summary>
        public ICommand HostCmd { get; private set; }

        /// <summary>
        /// Gets start command.
        /// </summary>
        public ICommand StartCmd { get; private set; }

        /// <summary>
        /// Gets closing command.
        /// </summary>
        public ICommand ClosingCmd { get; private set; }

        /// <summary>
        /// Gets mouse command.
        /// </summary>
        public ICommand MouseCmd { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether ConnectVis true or false.
        /// </summary>
        public bool ConnectVis
        {
            get
            {
                return this.connectVis;
            }

            set
            {
                this.connectVis = value;
                this.RaisePropertyChanged(() => this.ConnectVis);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether HostVis true or false.
        /// </summary>
        public bool HostVis
         {
            get
            {
                return this.hostVis;
            }

            set
            {
                this.hostVis = value;
                this.RaisePropertyChanged(() => this.HostVis);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether StartVis true or false.
        /// </summary>
        public bool StartVis
        {
            get
            {
                return this.startVis;
            }

            set
            {
                this.startVis = value;
                this.RaisePropertyChanged(() => this.StartVis);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether HistoryVis true or false.
        /// </summary>
        public bool HistoryVis
        {
            get
            {
                return this.historyVis;
            }

            set
            {
                this.historyVis = value;
                this.RaisePropertyChanged(() => this.HistoryVis);
            }
        }

        /// <summary>
        /// Gets or sets a value Status.
        /// </summary>
        public string Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
                this.RaisePropertyChanged(() => this.Status);
            }
        }

        /// <summary>
        /// Gets games Collection.
        /// </summary>
        public ObservableCollection<ClientInfo> Games { get; private set; }

        /// <summary>
        /// Gets or sets Backbrush bush.
        /// </summary>
        public Brush BackBrush
        {
            get
            {
                return this.backBrush;
            }

            set
            {
                this.backBrush = value;
                this.RaisePropertyChanged(() => this.BackBrush);
            }
        }

        /// <summary>
        /// Gets or sets name string.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.RaisePropertyChanged(() => this.Name);
            }
        }

        private ILoginLogic Logic { get; set; }

        private void HistoryDialog()
        {
            HistoryWindow win = new HistoryWindow();
            win.ShowDialog();
        }

        private void Update(object sender, EventArgs e)
        {
            this.timer.Stop();
            if (this.Logic.Connected)
            {
                this.Logic.CheckState();
                this.FillClients(this.Logic.Clients);
            }

            MusicPlayer.Restart();
            this.timer.Start();

            if (this.Logic.CanStartGame)
            {
                MusicPlayer.Stop();
                this.OpenWindow();
            }
        }

        private void MouseMove(Window o)
        {
            Point p = Mouse.GetPosition(o);
            this.BackBrush = this.cache.CalculateRelative(p, o.Width, o.Height);
            this.win = o;
        }

        private void FillClients(Dictionary<string, string> clients)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (this.Games.Where(x => x.IPAddress == clients.ElementAt(i).Key).Count() < 1)
                {
                    this.Games.Add(new ClientInfo(clients.ElementAt(i).Value, clients.ElementAt(i).Key));
                }
            }
        }

        private void HostGame()
        {
            if (this.Name != string.Empty && this.Name != string.Empty && this.Name != null)
            {
                this.Logic.HostGame();
                this.ConnectGame();
                this.Status = "Hosting";
                this.HostVis = false;
                this.ConnectVis = false;
                this.StartVis = true;
                this.HistoryVis = false;
            }
            else
            {
                MessageBox.Show("Please add username before hosting game", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ConnectGame()
        {
            if (this.Name != string.Empty && this.Name != string.Empty && this.Name != null)
            {
                this.Logic.ConnectGame(this.Name);
                Thread.Sleep(500);
                if (this.Logic.Connected)
                {
                    this.Logic.SetUsername(this.Name);
                    this.Logic.SendPlayer();
                    this.Status = "Connected";
                    this.ConnectVis = false;
                    this.HostVis = false;
                    this.HistoryVis = false;
                }
                else
                {
                    MessageBox.Show("No avaliable servers retry later.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Please add username before connecting game", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void StartGame()
        {
            this.Logic.StartGame();
        }

        private void OpenWindow()
        {
            this.timer.Stop();
            MusicPlayer.CanPlay = false;
            var control = new Control.ObServerControl(this.clientLogic, this.model);
            MainWindow win = new MainWindow
            {
                Content = control,
            };
            win.ShowDialog();
            this.Closing();
            this.win.Close();
        }

        private void Closing()
        {
            this.Logic.DisposeClient();
            this.Logic.DisposeServer();
        }
    }
}
