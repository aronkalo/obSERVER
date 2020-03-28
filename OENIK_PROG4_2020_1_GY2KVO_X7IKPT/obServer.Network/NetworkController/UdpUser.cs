using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using obServer.Network.Structs;

namespace obServer.Network.NetworkController
{
    internal sealed class UdpUser : UdpBase
    {
        private UdpUser(int port) { ClientPort = port; Client = new UdpClient(ClientPort); }

        private static int ClientPort { get; set; }

        private static IPEndPoint server { get; set; }

        public static UdpUser ConnectTo(IPEndPoint endPoint, int clientPort)
        {
            var connection = new UdpUser(clientPort);
            connection.Client.Connect(endPoint.Address, endPoint.Port);
            return connection;
        }

        public void Send(Operation operation, string parameters)
        {
            string data = $"{operation}:{parameters}";
            var datagram = Encoding.ASCII.GetBytes(data);
            Client.Send(datagram, datagram.Length);
        }
        //Sends Connecting Banner: 1
        public static IPEndPoint SearchForServers(int serverPort, int clientPort)
        {
            var Client = new UdpClient(clientPort);
            byte[] RequestData = Encoding.ASCII.GetBytes($"{Operation.Connect}" +
                $":{"Client"}");
            var ServerEp = new IPEndPoint(IPAddress.Any, serverPort);
            Client.EnableBroadcast = true;
            Client.Client.SendTimeout = 100;
            Client.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Broadcast, serverPort));
            var ServerResponseData = Client.Receive(ref ServerEp);
            var ServerResponse = Encoding.ASCII.GetString(ServerResponseData);
            if (ServerResponse == String.Empty)
            {
                throw new Exception("No Server found in Subnet");
            }
            Client.Close();
            Client.Dispose();
            string[] zones = ServerResponse.Split(':');
            ClientPort = clientPort;
            return new IPEndPoint(IPAddress.Parse(zones[2].Split(';')[0]),
                int.Parse(zones[2].Split(';')[1]));
        }
    }
}
