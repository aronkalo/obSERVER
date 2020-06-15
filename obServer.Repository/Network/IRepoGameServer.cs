// <copyright file="IRepoGameServer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.Network
{
    using obServer.Network.Structs;

    /// <summary>
    /// GameServer repo interface.
    /// </summary>
    public interface IRepoGameServer
    {
        /// <summary>
        /// Gets a value indicating whether gets all ready boolean.
        /// </summary>
        bool AllReady { get; }

        /// <summary>
        /// Listening starter method.
        /// </summary>
        void StartListening();

        /// <summary>
        /// Listen stop method.
        /// </summary>
        void StopListening();

        /// <summary>
        /// Handler reply.
        /// </summary>
        /// <param name="request">Request parameter.</param>
        void ReplyHandler(Request request);

        /// <summary>
        /// Client ready method.
        /// </summary>
        /// <param name="address">Address string.</param>
        void ReadyClient(string address);

        /// <summary>
        /// Request getter.
        /// </summary>
        /// <returns>True or false.</returns>
        Request? GetRequest();
    }
}
