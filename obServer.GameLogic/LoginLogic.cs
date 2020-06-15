// <copyright file="LoginLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameLogic
{
    using System;
    using System.Collections.Generic;
    using obServer.Logic.Interfaces;
    using obServer.Repository.GameModel;

    /// <summary>
    /// LoginLogic class.
    /// </summary>
    public class LoginLogic : ILoginLogic
    {
        private IRepoOBServerModel gameModel;

        private IServerLogic serverLogic;

        private IClientLogic clientLogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginLogic"/> class.
        /// </summary>
        /// <param name="model">ObserverRepo.</param>
        /// <param name="logic">Client logic.</param>
        public LoginLogic(IRepoOBServerModel model, IClientLogic logic)
        {
            this.gameModel = model;
            this.clientLogic = logic;
            this.clientLogic.Connect += this.OnConnected;
            this.clientLogic.Start += this.OnStartGame;
        }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets canStartGame.
        /// </summary>
        public bool CanStartGame { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets connected.
        /// </summary>
        public bool Connected { get; set; }

        /// <summary>
        /// Gets clients.
        /// </summary>
        public Dictionary<string, string> Clients
        {
            get
            {
                return this.clientLogic.Clients;
            }
        }

        /// <summary>
        /// Client dispose method.
        /// </summary>
        /// <returns>True or false.</returns>
        public bool DisposeClient()
        {
            if (this.clientLogic != null)
            {
                this.clientLogic = null;
                return true;
            }

            return false;
        }

        /// <summary>
        /// State checking method.
        /// </summary>
        public void CheckState()
        {
            this.clientLogic.CheckState();
        }

        /// <summary>
        /// Game start method.
        /// </summary>
        public void StartGame()
        {
            if (this.serverLogic != null)
            {
                this.serverLogic.SendGameStart();
            }
        }

        /// <summary>
        /// Game hosting method.
        /// </summary>
        public void HostGame()
        {
            this.serverLogic = new ServerLogic();
        }

        /// <summary>
        /// Game connect method.
        /// </summary>
        /// <param name="name">Name string.</param>
        public void ConnectGame(string name)
        {
            this.clientLogic.Ready(name);
        }

        /// <summary>
        /// Player sending method.
        /// </summary>
        public void SendPlayer()
        {
            this.clientLogic.AppendPlayer();
        }

        /// <summary>
        /// Server dispose method.
        /// </summary>
        /// <returns>True or false.</returns>
        public bool DisposeServer()
        {
            if (this.serverLogic != null)
            {
                this.serverLogic = null;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Setting username method.
        /// </summary>
        /// <param name="name">Name string.</param>
        public void SetUsername(string name)
        {
            this.gameModel.SetUsername(name);
        }

        private void OnStartGame(object sender, EventArgs e)
        {
            this.CanStartGame = true;
        }

        private void OnConnected(object sender, EventArgs e)
        {
            this.Connected = true;
        }
    }
}
