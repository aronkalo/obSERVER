// <copyright file="UdpElements.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.NetworkController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The udpelements class.
    /// </summary>
    public static class UdpElements
    {
        /// <summary>
        /// ServerPort parameter.
        /// </summary>
        private const int ServerPort = 18500;

        /// <summary>
        /// Clientport parameter.
        /// </summary>
        private const int ClientPort = 19321;

        private static UdpClient listener;
        private static UdpClient user;

        /// <summary>
        /// Gets the listener.
        /// </summary>
        public static UdpClient Listener
        {
            get
            {
                if (listener == null)
                {
                    listener = new UdpClient(ServerPort);
                }

                return listener;
            }
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        public static UdpClient User
        {
            get
            {
                if (user == null)
                {
                    user = new UdpClient(ClientPort);
                }

                return user;
            }
        }
    }
}
