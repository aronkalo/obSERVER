// <copyright file="GameServer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.NetworkElements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using obServer.Network.Interface;
    using obServer.Network.NetworkController;
    using obServer.Network.Structs;

    /// <summary>
    /// Server constructor.
    /// </summary>
    public sealed class GameServer : GameBase, IGameServer
    {
        private static object clientLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameServer"/> class.
        /// </summary>
        public GameServer()
            : base()
        {
            this.Clients = new List<GameClientInfo>();
            this.RequestPool = new RequestPool();
            this.Receive += this.OnReceive;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="GameServer"/> class.
        /// </summary>
        ~GameServer()
        {
            if (this.Listener != null)
            {
                this.Listener.Dispose();
            }
        }

        /// <summary>
        /// Gets a value indicating whether everyone is ready.
        /// </summary>
        public bool AllReady
        {
            get
            {
                return this.Clients.All(x => x.Ready);
            }
        }

        private UdpListener Listener { get; set; }

        private RequestPool RequestPool { get; set; }

        private bool ActiveSession { get; set; }

        private List<GameClientInfo> Clients { get; set; }

        private EventHandler<ReceivedEventArgs> Receive { get; set; }

        /// <summary>
        /// Setting client ready based on ip address.
        /// </summary>
        /// <param name="address">client ip address.</param>
        public void ReadyClient(string address)
        {
            for (int i = 0; i < this.Clients.Count; i++)
            {
                this.Clients[i].SetReady(address);
            }
        }

        /// <summary>
        /// Start listening the requests. Exception if its already listening.
        /// </summary>
        public void StartListening()
        {
            if (this.Listener == null)
            {
                this.Listener = new UdpListener();
                this.ActiveSession = true;
                Task.Factory.StartNew(async () =>
                {
                    while (this.ActiveSession)
                    {
                        var received = await this.Listener.Receive();
                        this.Receive?.Invoke(this, new ReceivedEventArgs(received));
                    }
                });
            }
            else
            {
                throw new Exception("Already listening");
            }
        }

        /// <summary>
        /// Stop listening the client requests.
        /// </summary>
        public void StopListening()
        {
            this.ActiveSession = false;
        }

        /// <summary>
        /// Send a reply to the client.
        /// </summary>
        /// <param name="request">Reply to send.</param>
        public void ReplyHandler(Request request)
        {
            if (request.Broadcast)
            {
                this.Send(request);
            }
            else
            {
                this.Reply(request);
            }
        }

        /// <summary>
        /// Returns there is a request int the request pool.
        /// </summary>
        /// <returns>Returning if there is a pending request.</returns>
        public Request? GetRequest()
        {
            if (this.RequestPool.NotNullElement())
            {
                return this.RequestPool.GetRequest();
            }

            return null;
        }

        private void OnReceive(object sender, ReceivedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                this.RequestPool.AddPoolElement(e.ReceivedRequest);
                this.CheckClients(e.ReceivedRequest);
            });
        }

        private void CheckClients(Request recieved)
        {
            lock (clientLock)
            {
                if (this.Clients.Where(x => x.EndPoint.ToString() == recieved.Sender.ToString()).Count() == 0)
                {
                    this.Clients.Add(new GameClientInfo()
                    {
                        EndPoint = recieved.Sender,
                        Id = Guid.NewGuid(),
                        Ready = false,
                    });
                }
            }
        }

        private void Reply(Request request)
        {
            this.Listener.Reply(request, request.Reply);
        }

        private void Send(Request request)
        {
            this.Listener.Send(request.Operation, request.Parameters, request.Reply);
        }
    }
}
