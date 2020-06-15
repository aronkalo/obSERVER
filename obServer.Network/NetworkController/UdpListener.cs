// <copyright file="UdpListener.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.NetworkController
{
    using System;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Text;
    using obServer.Network.Structs;

    /// <summary>
    /// UdpListener class.
    /// </summary>
    internal sealed class UdpListener : UdpBase
    {
        private IPEndPoint broadcastEndPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpListener"/> class.
        /// </summary>
        public UdpListener()
        {
            this.client = UdpElements.Listener;
            this.broadcastEndPoint = new IPEndPoint(IPAddress.Broadcast, ClientPort);
        }

        /// <summary>
        /// Make a reply.
        /// </summary>
        /// <param name="request">Request type.</param>
        /// <param name="reply">Reply parameters.</param>
        public void Reply(Request request, string reply)
        {
            var datagram = Encoding.ASCII.GetBytes($"{request.Operation}:{request.Parameters}:{reply}");
            this.client.Send(datagram, datagram.Length, request.Sender);
        }

        /// <summary>
        /// Broadcast the message.
        /// </summary>
        /// <param name="operation">Operation type.</param>
        /// <param name="parameters">Operation parameters.</param>
        /// <param name="reply">Reply parameters.</param>
        public void Send(Operation operation, string parameters, string reply)
        {
            var datagram = Encoding.ASCII.GetBytes($"{operation}:{parameters}:{reply}");
            this.client.Send(datagram, datagram.Length, this.broadcastEndPoint);
        }
    }
}