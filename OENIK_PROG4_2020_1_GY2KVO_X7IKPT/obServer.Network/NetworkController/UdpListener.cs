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
        private IPAddress LocalAddress;

        private const int port = 3200;

        private IPEndPoint ListenPoint;

        //Multiple Clients, One Listener
        public UdpListener() : this(new IPEndPoint(IPAddress.Any,3200))
        {
            this.LocalAddress = GetIpv4HostAddress();
        }
        //One Client, One Listener
        public UdpListener(IPEndPoint endpoint)
        {
            ListenPoint = endpoint;
            Client = new UdpClient(ListenPoint);
            this.LocalAddress = GetIpv4HostAddress();
        }

        public IPEndPoint GetLocalEndPoint()
        {
            return new IPEndPoint(LocalAddress,port);
        }
        public static void ShowUdpStatistics(NetworkInterfaceComponent version)
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            System.Net.NetworkInformation.UdpStatistics udpStat = null;

            switch (version)
            {
                case NetworkInterfaceComponent.IPv4:
                    udpStat = properties.GetUdpIPv4Statistics();
                    Console.WriteLine("UDP IPv4 Statistics");
                    break;
                case NetworkInterfaceComponent.IPv6:
                    udpStat = properties.GetUdpIPv6Statistics();
                    Console.WriteLine("UDP IPv6 Statistics");
                    break;
                default:
                    throw new ArgumentException("version");
                    //    break;
            }
            Console.WriteLine("  Datagrams Received ...................... : {0}",
                udpStat.DatagramsReceived);
            Console.WriteLine("  Datagrams Sent .......................... : {0}",
                udpStat.DatagramsSent);
            Console.WriteLine("  Incoming Datagrams Discarded ............ : {0}",
                udpStat.IncomingDatagramsDiscarded);
            Console.WriteLine("  Incoming Datagrams With Errors .......... : {0}",
                udpStat.IncomingDatagramsWithErrors);
            Console.WriteLine("  UDP Listeners ........................... : {0}",
                udpStat.UdpListeners);
            Console.WriteLine("");
        }

        public void Reply(Request request, string reply)
        {
            var datagram = Encoding.ASCII.GetBytes($"{request.Operation}" +
                $":{request.Parameters}:{reply}");
            Client.Send(datagram, datagram.Length, request.Sender);
        }
        //Ha küldeni akar és nem válaszolni.
        public void Send(IPEndPoint endPoint, Operation operation, string parameters, string reply)
        {
            var datagram = Encoding.ASCII.GetBytes($"{operation}" +
                 $":{parameters}:{reply}");
            Client.Send(datagram, datagram.Length, endPoint);
        }
    }
}