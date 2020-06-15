// <copyright file="IGameServer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using obServer.Network.Structs;

    /// <summary>
    /// IGameServer interface.
    /// </summary>
    public interface IGameServer : IGameBase
    {
        /// <summary>
        /// Gets a value indicating whether everythink is ready.
        /// </summary>
        bool AllReady { get; }

        /// <summary>
        /// Start to listening the incoming datas.
        /// </summary>
        void StartListening();

        /// <summary>
        /// Processing requests.
        /// </summary>
        /// <returns>True if request is completed.</returns>
        Request? GetRequest();

        /// <summary>
        /// End listening.
        /// </summary>
        void StopListening();

        /// <summary>
        /// Reply to the requests.
        /// </summary>
        /// <param name="request">Reply request.</param>
        void ReplyHandler(Request request);

        /// <summary>
        /// Makes the client ready to operate.
        /// </summary>
        /// <param name="address">Server address.</param>
        void ReadyClient(string address);
    }
}
