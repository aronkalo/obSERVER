// <copyright file="GameClientInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.Structs
{
    using System;
    using System.Net;

    /// <summary>
    /// Client info class.
    /// </summary>
    public class GameClientInfo
    {
        private IPEndPoint endpoint;

        private Guid id;

        private bool ready;

        /// <summary>
        /// Gets or sets the client endpoints.
        /// </summary>
        public IPEndPoint EndPoint
        {
            get { return this.endpoint; }
            set { this.endpoint = value; }
        }

        /// <summary>
        /// Gets or sets the cliend Id.
        /// </summary>
        public Guid Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether client is ready.
        /// </summary>
        public bool Ready
        {
            get { return this.ready; }
            set { this.ready = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether client setting to ready.
        /// </summary>
        /// <param name="ipAddress">Endpoint ip address.</param>
        /// <returns>Ready or not ready.</returns>
        public bool SetReady(string ipAddress)
        {
            if (ipAddress == this.endpoint.Address.ToString())
            {
                this.Ready = true;
                return true;
            }

            return false;
        }
    }
}