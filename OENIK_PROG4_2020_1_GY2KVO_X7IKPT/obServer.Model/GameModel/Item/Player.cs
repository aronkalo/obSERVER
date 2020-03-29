using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace obServer.Model.GameModel.Item
{
    public sealed class Player : BaseItem
    {
        private const double movementSpeed = 100;
        public Player(Geometry geometry, Guid id, double[] position, double rotation, bool impact) : base(geometry, id, position, rotation, impact)
        {
            CurrentWeapon = null;
        }

        private Weapon CurrentWeapon;

        public void ChangeWeapon(Weapon newWeapon)
        {
            if (CurrentWeapon == null)
            {
                CurrentWeapon = newWeapon;
            }
            else
            {
                Weapon helper = CurrentWeapon;
                helper = null;
                CurrentWeapon = newWeapon;
            }
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
