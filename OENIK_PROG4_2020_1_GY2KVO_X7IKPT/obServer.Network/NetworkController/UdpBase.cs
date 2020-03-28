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
    internal abstract class UdpBase
    {
        public IPAddress GetIpv4HostAddress()
        {
            String strHostName = string.Empty;
            // Getting Ip address of local machine...
            // First get the host name of local machine.
            strHostName = Dns.GetHostName();
            //Console.WriteLine("Local Machine's Host Name: " + strHostName);
            // Then using host name, get the IP address list..
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

        protected UdpClient Client;
        
        protected UdpBase()
        {
            Client = new UdpClient();
        }

        public async Task<Request> Receive()
        {
            var Result = await Client.ReceiveAsync();
            string data = Encoding.ASCII.GetString(Result.Buffer, 0, Result.Buffer.Length);
            string[] zones = data.Split(':');
            return new Request()
            {
                Operation = (Operation)Enum.Parse(typeof(Operation), zones[0]),
                Parameters = zones[1],
                Reply = zones.Length < 3 ? String.Empty : zones[2],
                Sender = Result.RemoteEndPoint,
            };
        }
    }

}
