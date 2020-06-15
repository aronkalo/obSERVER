// <copyright file="GameClient.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.NetworkElements
{
    using System;
    using System.Threading.Tasks;
    using obServer.Network.Interface;
    using obServer.Network.NetworkController;
    using obServer.Network.Structs;

    /// <summary>
    /// Gameclient Class.
    /// </summary>
    public sealed class GameClient : GameBase, IGameClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameClient"/> class.
        /// </summary>
        public GameClient()
            : base()
        {
            this.Network = new UdpUser();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="GameClient"/> class.
        /// </summary>
        ~GameClient()
        {
            this.Network.Dispose();
        }

        /// <summary>
        /// Gets or sets the received events.
        /// </summary>
        public EventHandler<IReceivedEventArgs> Receive { get; set; }

        private bool ActiveSession { get; set; }

        private UdpUser Network { get; set; }

        /// <summary>
        /// Sending the commands to the server.
        /// </summary>
        /// <param name="operation">operation name.</param>
        /// <param name="parameters">operation parameters.</param>
        public void Send(Operation operation, string parameters)
        {
            Task.Factory.StartNew(() => this.Network.Send(operation, parameters));
        }

        /// <summary>
        /// Client start listening to the server.
        /// </summary>
        public void StartListening()
        {
            this.ActiveSession = true;
            Task.Factory.StartNew(async () =>
            {
                while (this.ActiveSession)
                {
                    var received = await this.Network.Receive();
                    this.Receive?.Invoke(this, new ReceivedEventArgs(received));
                }
            });
        }
    }
}
