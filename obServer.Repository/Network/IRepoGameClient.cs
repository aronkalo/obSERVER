// <copyright file="IRepoGameClient.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.Network
{
    using System;
    using obServer.Network.Interface;
    using obServer.Network.Structs;

    /// <summary>
    /// GameClient repo interface.
    /// </summary>
    public interface IRepoGameClient
    {
        /// <summary>
        /// Gets or sets Receives.
        /// </summary>
        EventHandler<IReceivedEventArgs> ReceiveRequest { get; set; }

        /// <summary>
        /// Send method.
        /// </summary>
        /// <param name="operation">Operation parameter.</param>
        /// <param name="parameters">parameter string.</param>
        void Send(Operation operation, string parameters);

        /// <summary>
        /// Listening method.
        /// </summary>
        void StartListening();
    }
}
