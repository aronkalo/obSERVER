using obServer.Network.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace obServer.Network.NetworkController
{
    public abstract class TcpBase
    {
        private TcpBase() { }
        private TcpClient Client;

        private const int clientPort = 8000;

        private const int serverPort = 6500;

        //public async Task<Request> Receive()
        //{
        //    TcpListener list = new TcpListener(IPAddress.Any, clientPort);
        //    list.Start();
        //    list
        //    string data = Encoding.ASCII.GetString(Result.Buffer, 0, Result.Buffer.Length);
        //    string[] zones = data.Split(':');
        //    return new Request()
        //    {
        //        Operation = (Operation)Enum.Parse(typeof(Operation), zones[0]),
        //        Parameters = zones[1],
        //        Reply = zones.Length < 3 ? String.Empty : zones[2],
        //        Sender = Result.RemoteEndPoint,
        //    };
        //}
    }
}
