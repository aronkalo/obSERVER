// <copyright file="RepoGameClient.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.Network
{
    using System;
    using obServer.Network.Interface;
    using obServer.Network.NetworkElements;
    using obServer.Network.Structs;

    /// <summary>
    /// GameClient repo class.
    /// </summary>
    public class RepoGameClient : IRepoGameClient
    {
        private IGameClient gameClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoGameClient"/> class.
        /// </summary>
        public RepoGameClient()
        {
            this.gameClient = new GameClient();
            this.gameClient.Receive += this.OnReceive;
        }

        /// <summary>
        /// Gets or sets receive handler.
        /// </summary>
        public EventHandler<ReceivedEventArgs> Receive { get; set; }

        /// <summary>
        /// Gets or sets Request receive.
        /// </summary>
        public EventHandler<IReceivedEventArgs> ReceiveRequest { get; set; }

        /// <summary>
        /// Send method.
        /// </summary>
        /// <param name="operation">Opertion parameter.</param>
        /// <param name="parameters">Parameter string.</param>
        public void Send(Operation operation, string parameters)
        {
            this.gameClient.Send(operation, parameters);
        }

        /// <summary>
        /// Listen starting method.
        /// </summary>
        public void StartListening()
        {
            this.gameClient.StartListening();
        }

        private void OnReceive(object sender, IReceivedEventArgs e)
        {
            this.ReceiveRequest?.Invoke(sender, e);
        }
    }
}
