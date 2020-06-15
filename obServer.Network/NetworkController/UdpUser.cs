// <copyright file="UdpUser.cs" company="PlaceholderCompany">
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
    using System.Threading;
    using System.Threading.Tasks;
    using obServer.Network.Structs;

    /// <summary>
    /// UdpUser class.
    /// </summary>
    internal sealed class UdpUser : UdpBase
    {
        private const int PingTimeout = 100;

        private IPEndPoint broadcastEndPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpUser"/> class.
        /// </summary>
        public UdpUser()
        {
            this.client = UdpElements.User;
            this.broadcastEndPoint = new IPEndPoint(IPAddress.Broadcast, ServerPort);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpUser"/> class.
        /// </summary>
        /// <param name="hamachiAddress">hamachi ip.</param>
        public UdpUser(string hamachiAddress)
        {
            this.client = new UdpClient(ClientPort);
            this.client = new UdpClient(new IPEndPoint(IPAddress.Parse(hamachiAddress), ClientPort));
            this.broadcastEndPoint = new IPEndPoint(IPAddress.Broadcast, ServerPort);
        }

        /// <summary>
        /// Pinging the server.
        /// </summary>
        /// <returns>Ping success.</returns>
        public bool PingServer()
        {
            bool foundServer = false;
            Task receive = new Task(async () =>
            {
                while (!foundServer)
                {
                    Request r = await this.Receive();
                    if (r.Operation == Operation.Connect)
                    {
                        foundServer = true;
                    }
                }
            });
            receive.Start();
            Task pingServer = new Task(() =>
            {
                while (!foundServer)
                {
                    var datagram = Encoding.ASCII.GetBytes($"{Operation.Connect}:Client");
                    this.client.Send(datagram, datagram.Length, this.broadcastEndPoint);
                    Thread.Sleep(PingTimeout);
                }
            });
            pingServer.Start();
            while (!foundServer)
            {
            }

            return true;
        }

        /// <summary>
        /// Send commands to the server.
        /// </summary>
        /// <param name="operation">Operation type.</param>
        /// <param name="parameters">Operation parameters.</param>
        public void Send(Operation operation, string parameters)
        {
            var datagram = Encoding.ASCII.GetBytes($"{operation}:{parameters}");
            this.client.Send(datagram, datagram.Length, this.broadcastEndPoint);
        }
    }
}
