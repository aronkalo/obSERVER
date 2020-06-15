// <copyright file="RepoGameServer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.Network
{
    using System;
    using obServer.Network.Interface;
    using obServer.Network.NetworkElements;
    using obServer.Network.Structs;

    /// <summary>
    /// GameServer repo class.
    /// </summary>
    public class RepoGameServer : IRepoGameServer
    {
        private IGameServer gameServer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoGameServer"/> class.
        /// </summary>
        public RepoGameServer()
        {
            this.gameServer = new GameServer();
        }

        /// <summary>
        /// Gets or sets Request receives.
        /// </summary>
        public EventHandler<IReceivedEventArgs> ReceiveRequest { get; set; }

        /// <summary>
        /// Gets a value indicating whether gets all ready field.
        /// </summary>
        public bool AllReady
        {
            get { return this.gameServer.AllReady; }
        }

        /// <summary>
        /// Listen starting method.
        /// </summary>
        public void StartListening()
        {
            this.gameServer.StartListening();
        }

        /// <summary>
        /// Listen stopper method.
        /// </summary>
        public void StopListening()
        {
            this.gameServer.StopListening();
        }

        /// <summary>
        /// Reply handling method.
        /// </summary>
        /// <param name="request">Request parameter.</param>
        public void ReplyHandler(Request request)
        {
            this.gameServer.ReplyHandler(request);
        }

        /// <summary>
        /// Client ready method.
        /// </summary>
        /// <param name="address">Address string.</param>
        public void ReadyClient(string address)
        {
            this.gameServer.ReadyClient(address);
        }

        /// <summary>
        /// Request getter method.
        /// </summary>
        /// <returns>Server request.</returns>
        public Request? GetRequest()
        {
            return this.gameServer.GetRequest();
        }

        private void OnReceive(object sender, IReceivedEventArgs e)
        {
            this.ReceiveRequest?.Invoke(sender, e);
        }
    }
}
