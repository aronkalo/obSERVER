using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using obServer.GameLogic;
using obServer.GameModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace obServer.ViewController.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(IObServerModel model, ClientLogic clientLogic)
        {
            this.model = model;
            this.clientLogic = clientLogic;
            this.logic = new LoginLogic(model, clientLogic);
            string directory = Directory.GetCurrentDirectory();
            bBrush = new ImageBrush(new BitmapImage(new Uri(directory + "\\textures\\startback.png")));
            bBrush.Opacity = 0.6;
            logic.OpenMain += OnStartGame;
            Games = new ObservableCollection<string>()
            {
                "Alma", "Körte", "Banán", "Szilva"
            };
            ConnectCmd = new RelayCommand (() => logic.ConnectGame());
            StartCmd = new RelayCommand(() => logic.OpenMain?.Invoke(this, EventArgs.Empty));
            HostCmd = new RelayCommand(() => logic.HostGame());
            ClosingCmd = new RelayCommand(() => Closing());
            MouseCmd = new RelayCommand<Point>((x) => ChangeBrush(x));
        }

        private void ChangeBrush(Point x)
        {
        }

        public LoginViewModel() :this(ServiceLocator.Current.GetInstance<IObServerModel>(), ServiceLocator.Current.GetInstance<ClientLogic>())
        {

        }

        private void OnStartGame(object sender, EventArgs e)
        {
            var control = new Control.ObServerControl(clientLogic, model);
            MainWindow win = new MainWindow();
            win.Content = control;
            win.ShowDialog();
            logic.EraseClient();
        }
        private void Closing()
        {
            logic.EraseClient();
        }
        private ClientLogic clientLogic;

        private IObServerModel model;
        private LoginLogic logic { get;}
        public ICommand ConnectCmd { get; private set; }
        public ICommand HostCmd { get; private set; }
        public ICommand StartCmd { get; private set; }
        public ICommand ClosingCmd { get; private set; }
        public ICommand MouseCmd { get; private set; }
        public ObservableObject Name { get; set; }
        public ObservableCollection<string> Games { get; private set; }

        private Brush bBrush { get; set; }

        public Brush BackBrush
        {
            get
            {
                return bBrush;
            }
        } 
    }
}
