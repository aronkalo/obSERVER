using obServer.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace obServer.Model.GameModel.Item
{
    public sealed class Player : BaseItem, IPlayer, IBaseItem
    {
        public static Geometry PlayerGeometry = new EllipseGeometry() { RadiusX = Width, RadiusY = Height};
        private const double movementSpeed = 100;
        private const double Width = 100;
        private const double Height = 100;

        public Player(Geometry geometry, Guid id, double[] position, double rotation, bool impact) : base(geometry, id, position, rotation, impact)
        {
            CurrentWeapon = null;
        }

        public IWeapon CurrentWeapon { get; private set; }

        public void ChangeWeapon(IWeapon newWeapon)
        {
            if (CurrentWeapon == null)
            {
                CurrentWeapon = newWeapon;
            }
            else
            {
                IWeapon helper = CurrentWeapon;
                helper = null;
                CurrentWeapon = newWeapon;
            }
        }

        public IBullet[] Shoot()
        {
            return CurrentWeapon.DoShoot();
        }

        public void Reload(int storedBullets)
        {
            CurrentWeapon.DoReload(storedBullets);
        }

        public void Move(double xMove, double yMove, double deltaTime, double rotation)
        {
            double xMovement = xMove * deltaTime * movementSpeed;
            double yMovement = yMove * deltaTime * movementSpeed;
            if (CurrentWeapon != null)
            {
                CurrentWeapon.Move(xMovement, yMovement, rotation);
            }
            ChangePosition(xMovement, yMovement, rotation);
        }
    }
}
