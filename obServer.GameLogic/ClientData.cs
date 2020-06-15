// <copyright file="ClientData.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameLogic
{
    using System;

    /// <summary>
    /// Client data class.
    /// </summary>
    public class ClientData
    {
        /// <summary>
        /// Gets or sets IPAddress.
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets Username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets identifier.
        /// </summary>
        public Guid Identifier { get; set; }
    }
}