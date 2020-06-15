// <copyright file="UdpBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.NetworkController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using obServer.Network.Structs;

    /// <summary>
    /// UdpBase class.
    /// </summary>
    internal abstract class UdpBase
    {
        /// <summary>
        /// ServerPort parameter.
        /// </summary>
        internal const int ServerPort = 18500;

        /// <summary>
        /// Clientport parameter.
        /// </summary>
        internal const int ClientPort = 19321;

        /// <summary>
        /// UdpClient class.
        /// </summary>
        protected UdpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpBase"/> class.
        /// </summary>
        protected UdpBase()
        {
        }

        /// <summary>
        /// The Dispose method.
        /// </summary>
        public void Dispose()
        {
            this.client.Client.Shutdown(SocketShutdown.Both);
            this.client.Client.Close();
            this.client.Dispose();
        }

        /// <summary>
        /// Gets back Host addresses.
        /// </summary>
        /// <returns>IPAddress.</returns>
        public IPAddress GetIpv4HostAddress()
        {
            string strHostName = string.Empty;
            strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

            IPAddress[] addr = ipEntry.AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork).ToArray();
            if (addr.Length == 1)
            {
                return addr[0];
            }
            else
            {
                throw new Exception("Not correct IP Address");
            }
        }

        /// <summary>
        /// Processing data.
        /// </summary>
        /// <returns>Returns the processed data.</returns>
        public async Task<Request> Receive()
        {
            this.client.AllowNatTraversal(true);
            var result = await this.client.ReceiveAsync();
            string data = Encoding.ASCII.GetString(result.Buffer, 0, result.Buffer.Length);
            string[] zones = data.Split(':');
            return new Request()
            {
                Operation = (Operation)Enum.Parse(typeof(Operation), zones[0]),
                Parameters = zones[1],
                Reply = zones.Length < 3 ? string.Empty : zones[2],
                Sender = result.RemoteEndPoint,
            };
        }
    }
}
