using obServer.Network.Structs;

namespace obServer.Network.Interface
{
    public interface IGameClient
    {
        void Send(Operation operation, string parameters);

        void StartListening();

        Request GetResponse();
    }
}
