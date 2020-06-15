// <copyright file="ClientInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.ViewController.ViewModel
{
    /// <summary>
    /// Client info class.
    /// </summary>
    public class ClientInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientInfo"/> class.
        /// </summary>
        /// <param name="name">Name string.</param>
        /// <param name="ip">Ip string.</param>
        public ClientInfo(string name, string ip)
        {
            this.Name = name;
            this.IPAddress = ip;
        }

        /// <summary>
        /// Gets or sets Name string.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Ip address.
        /// </summary>
        public string IPAddress { get; set; }
    }
}