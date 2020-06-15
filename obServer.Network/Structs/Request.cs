// <copyright file="Request.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.Structs
{
    using System.Net;
    using obServer.Network.Structs;

    /// <summary>
    /// Request struct.
    /// </summary>
    public struct Request
    {
        private IPEndPoint sender;

        private Operation operation;
        private string parameters;
        private string reply;
        private bool broadcast;

        /// <summary>
        /// Gets or sets the sender ip address.
        /// </summary>
        public IPEndPoint Sender
        {
            get { return this.sender; }
            set { this.sender = value; }
        }

        /// <summary>
        /// Gets or sets the operation parameter.
        /// </summary>
        public Operation Operation
        {
            get { return this.operation; }
            set { this.operation = value; }
        }

        /// <summary>
        /// Gets or sets the request parameters.
        /// </summary>
        public string Parameters
        {
            get { return this.parameters; }
            set { this.parameters = value; }
        }

        /// <summary>
        /// Gets or sets the request reply parameter.
        /// </summary>
        public string Reply
        {
            get { return this.reply; }
            set { this.reply = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether request parameters.
        /// </summary>
        public bool Broadcast
        {
            get { return this.broadcast; }
            set { this.broadcast = value; }
        }
    }
}
