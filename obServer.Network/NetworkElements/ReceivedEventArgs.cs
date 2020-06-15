// <copyright file="ReceivedEventArgs.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.NetworkElements
{
    using System;
    using obServer.Network.Interface;
    using obServer.Network.Structs;

    /// <summary>
    /// Received Event args class.
    /// </summary>
    public class ReceivedEventArgs : EventArgs, IReceivedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="request">Request parameter.</param>
        public ReceivedEventArgs(Request request)
        {
            this.ReceivedRequest = request;
        }

        /// <summary>
        /// Gets or sets the recevied requests.
        /// </summary>
        public Request ReceivedRequest { get; set; }
    }
}