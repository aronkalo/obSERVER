using obServer.Model.Interfaces;
using System;

namespace obServer.Logic
{
    public class PlayerInputEventArgs :EventArgs
    {
        public double deltaTime = 0;
        public IPlayer Player = null;
        public double[] Movement = new double[2];
        public double Angle = 0;
        public bool Shoot = false;
        public bool Reload = false;
        public bool Pickup = false;
    }
}