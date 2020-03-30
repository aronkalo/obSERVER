using obServer.Model.Interfaces;

namespace obServer.Logic
{
    public class MovementEventArgs
    {
        public double deltaTime;
        public IPlayer Player;
        public double[] Movement;
        public double Angle;
    }
}