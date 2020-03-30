using System;
using System.Windows.Media;

namespace obServer.Model.Interfaces
{
    public interface IPlayer : IBaseItem
    {
        IWeapon CurrentWeapon { get; }
        void ChangeWeapon(IWeapon newWeapon);
        void Move(double xMove, double yMove, double deltaTime, double rotation);
        IBullet[] Shoot();
        void Reload();
        void PickBullet(int bullets);
        void Damaged(double damage);
    }
}