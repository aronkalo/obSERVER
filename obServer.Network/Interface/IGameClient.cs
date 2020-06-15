// <copyright file="IGameClient.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.Interface
{
    using System;
    using obServer.Network.Structs;

    /// <summary>
    /// IGameClient interface.
    /// </summary>
    public interface IGameClient : IGameBase
    {
        /// <summary>
        /// Gets or sets received events.
        /// </summary>
        EventHandler<IReceivedEventArgs> Receive { get; set; }

        /// <summary>
        /// Send operation to the server.
        /// </summary>
        /// <param name="operation">Operation type.</param>
        /// <param name="parameters">Operation parameters.</param>
        void Send(Operation operation, string parameters);

        /// <summary>
        /// Client start to listening for commands.
        /// </summary>
        void StartListening();
    }
}
