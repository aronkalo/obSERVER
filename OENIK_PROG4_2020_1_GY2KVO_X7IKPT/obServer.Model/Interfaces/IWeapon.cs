using System;
using System.Windows.Media;

namespace obServer.Model.Interfaces
{
    public interface IWeapon : IBaseItem
    {
        IBullet[] DoShoot();
        void Move(double xMovement, double yMovement, double rotation);
        void DoReload(int storedBullets);
    }
}