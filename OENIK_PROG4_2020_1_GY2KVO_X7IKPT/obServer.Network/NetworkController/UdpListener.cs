using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using obServer.Network.Structs;

namespace obServer.Network.NetworkController
{
    internal sealed class UdpListener : UdpBase
    {

        private IPEndPoint broadcastEndPoint;

        public UdpListener()
        {
            Client = new UdpClient(serverPort);
            broadcastEndPoint = new IPEndPoint(IPAddress.Broadcast, clientPort);
        }

        public void Reply(Request request, string reply)
        {
            var datagram = Encoding.ASCII.GetBytes($"{request.Operation}:{request.Parameters}:{reply}");
            Client.Send(datagram, datagram.Length, request.Sender);
        }
        //Broadcasts the message
        public void Send(Operation operation, string parameters, string reply)
        {
            var datagram = Encoding.ASCII.GetBytes($"{operation}:{parameters}:{reply}");
            Client.Send(datagram, datagram.Length, broadcastEndPoint);
        }
    }
}