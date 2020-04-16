using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using obServer.Network.Structs;

namespace obServer.Network.NetworkController
{
    internal sealed class UdpUser : UdpBase
    {
        private const int pingTimeout = 100;

        private IPEndPoint broadcastEndPoint;

        public UdpUser()
        {
            
            Client = new UdpClient(clientPort);
            broadcastEndPoint = new IPEndPoint(IPAddress.Broadcast, serverPort);
        }

        public bool PingServer()
        {
            bool foundServer = false;
            Task receive = new Task( async () =>
            {
                while (!foundServer)
                {
                    Request r = await Receive();
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
                    Client.Send(datagram, datagram.Length, broadcastEndPoint);
                    Thread.Sleep(pingTimeout);
                }

            });
            pingServer.Start();
            while (!foundServer)
            {

            }
            return true;
        }

        public void Send(Operation operation, string parameters)
        {
            var datagram = Encoding.ASCII.GetBytes($"{operation}:{parameters}");
            Client.Send(datagram, datagram.Length, broadcastEndPoint);
        }
    }
}
