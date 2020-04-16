using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using obServer.GameModel;
using obServer.GameModel.Interfaces;

namespace obServer.GameLogic
{
    public class LoginLogic
    {
        public LoginLogic(IObServerModel model, ClientLogic logic)
        {
            gameModel = model;
            clientLogic = logic;
            clientLogic.Start += OnStartGame;
        }

        public EventHandler OpenMain;

        private IObServerModel gameModel;

        private ServerLogic serverLogic;

        private ClientLogic clientLogic;


        public void EraseClient()
        {
            clientLogic = null;
        }

        private void OnStartGame(object sender, EventArgs e)
        {
            OpenMain?.Invoke(sender, e);
        }

        public void UpdateGames(string name)
        {
            clientLogic.SearchServers(name);
        }

        public void UpdateClients()
        {

        }

        public void StartGame()
        {

        }

        public void HostGame()
        {
            serverLogic = new ServerLogic();
        }

        public void ConnectGame()
        {

        }

    }
}
