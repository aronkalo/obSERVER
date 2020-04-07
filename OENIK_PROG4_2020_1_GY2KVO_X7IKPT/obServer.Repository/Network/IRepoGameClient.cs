using obServer.Network.Interface;
using obServer.Network.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.Repository.Network
{
    public interface IRepoGameClient
    {
        void Send(Operation operation, string parameters);

        void StartListening();

        EventHandler<IReceivedEventArgs> ReceiveRequest { get; set; }
    }
}
