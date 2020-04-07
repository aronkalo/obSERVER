using obServer.Network.Structs;
using System;

namespace obServer.Network.Interface
{
    public interface IGameClient : IGameBase
    {
        void Send(Operation operation, string parameters);

        void StartListening();

        EventHandler<IReceivedEventArgs> Receive { get; set; }
    }
}
