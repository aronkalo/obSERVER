using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using obServer.GameLogic;
using obServer.GameModel;
using obServer.GameModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace obServer.ViewController
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IObServerModel, ObServerModel>();
            SimpleIoc.Default.Register<ClientLogic, ClientLogic>();
        }
    }
}
