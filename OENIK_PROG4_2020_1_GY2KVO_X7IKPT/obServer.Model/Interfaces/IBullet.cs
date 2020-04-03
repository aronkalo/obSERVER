using System;
using System.Windows.Media;

namespace obServer.Model.Interfaces
{
    public interface IBullet : IBaseItem
    {
        void Fly(double deltaTime);
        void DoDamage(IPlayer player);

        double BulletDamage { get; }

        double[] Direction { get; }

        double Weight { get; }

        double Speed { get; }
        double RealSpeed { get; }
    }
}