using obServer.Network.Interface;
using obServer.Network.Structs;
using System;

namespace obServer.Network.NetworkElements
{
    public class ReceivedEventArgs : EventArgs, IReceivedEventArgs
    {
      public Request ReceivedRequest { get; set; }
    }
}