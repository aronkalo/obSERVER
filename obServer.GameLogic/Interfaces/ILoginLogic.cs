// <copyright file="ILoginLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Logic.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The login logic interface.
    /// </summary>
    public interface ILoginLogic
    {
        /// <summary>
        /// Gets or sets a value indicating whether can starts game.
        /// </summary>
        bool CanStartGame { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is connected.
        /// </summary>
        bool Connected { get; set; }

        /// <summary>
        /// Gets the clients.
        /// </summary>
        Dictionary<string, string> Clients { get; }

        /// <summary>
        /// Dispose the client.
        /// </summary>
        /// <returns>Whether is disposed.</returns>
        bool DisposeClient();

        /// <summary>
        /// Checks the state.
        /// </summary>
        void CheckState();

        /// <summary>
        /// Starts the game.
        /// </summary>
        void StartGame();

        /// <summary>
        /// Hosts a game.
        /// </summary>
        void HostGame();

        /// <summary>
        /// The game connects.
        /// </summary>
        /// <param name="name">name.</param>
        void ConnectGame(string name);

        /// <summary>
        /// Sends a player.
        /// </summary>
        void SendPlayer();

        /// <summary>
        /// Server dispose method.
        /// </summary>
        /// <returns>Wether is true.</returns>
        bool DisposeServer();

        /// <summary>
        /// Sets the username.
        /// </summary>
        /// <param name="name">Nameof.</param>
        void SetUsername(string name);
    }
}
