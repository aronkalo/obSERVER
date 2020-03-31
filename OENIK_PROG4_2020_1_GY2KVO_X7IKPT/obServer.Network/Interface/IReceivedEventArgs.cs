using obServer.Network.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.Network.Interface
{
    public interface IReceivedEventArgs
    {
        Request ReceivedRequest { get; set; }
    }
}
