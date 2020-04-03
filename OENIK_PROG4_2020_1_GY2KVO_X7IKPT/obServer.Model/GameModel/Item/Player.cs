using obServer.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace obServer.Model.GameModel.Item
{
    public sealed class Player : BaseItem, IPlayer, IBaseItem
    {
        public static Geometry PlayerGeometry = new EllipseGeometry() { RadiusX = Width, RadiusY = Height};
        private const double movementSpeed = 300;
        private const double Width = 40;
        private const double Height = 40;

        public Player(Geometry geometry, Guid id, double[] position, double rotation, bool impact, double health) : base(geometry, id, position, rotation, impact)
        {
            CurrentWeapon = null;
            this.health = health;
            weaponCache = new Vector(0, 40);
        }

        public IWeapon CurrentWeapon { get; private set; }
        public EventHandler Die { get; set; }

        private double health;
        public double Health
        {
            get
            {
                return health;
            }
        }
        private int storedBullets;

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
                Vector pos = WeaponPosition(0);
                weaponCache = pos;
                CurrentWeapon.Position = new Vector(Position.X, Position.Y);
            }
        }

        public void PickBullet(int bullets)
        {
            storedBullets += bullets;
        }

        public IBullet[] Shoot()
        {
            if (CurrentWeapon != null)
            {
                return CurrentWeapon.DoShoot();
            }
            return null;
        }

        public void Reload()
        {
            CurrentWeapon.DoReload(storedBullets);
        }


        public void Move(double xMove, double yMove, double deltaTime, double rotation)
        {
            double xMovement = xMove * deltaTime * movementSpeed;
            double yMovement = yMove * deltaTime * movementSpeed;
            if (CurrentWeapon != null)
            {
                if (rotation != Rotation)
                {
                    Vector pos = WeaponPosition(rotation - Rotation);
                    Vector sub = Vector.Subtract(pos, weaponCache);
                    CurrentWeapon.Move(xMovement + sub.X, yMovement + sub.Y, rotation);
                    weaponCache = pos;
                }
                else
                {
                    CurrentWeapon.Move(xMovement, yMovement, rotation);
                }

            }
            ChangePosition(xMovement, yMovement, rotation);
        }

        public void Damaged(double damage)
        {
            health -= damage;
            if (health < 0)
            {
                Die?.Invoke(this, null);
            }
        }

        private Vector weaponCache;

        private Vector WeaponPosition(double angle)
        {
            Matrix m = Matrix.Identity;
            m.Rotate(angle);
            return m.Transform(weaponCache);
        }
    }
}
