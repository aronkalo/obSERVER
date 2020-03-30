using obServer.Model.Interfaces;

namespace obServer.Logic.Event
{
    public class MovementEventArgs
    {
        public IPlayer movementPlayer { get; private set; }
        public double[] movementDirection { get; private set; }
        public double deltaTime { get; private set; }
        public double angle { get; private set; }
    }
}