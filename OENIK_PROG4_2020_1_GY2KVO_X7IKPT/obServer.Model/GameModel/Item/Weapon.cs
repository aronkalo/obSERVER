using obServer.Model.Interfaces;
using System;
using System.Windows;
using System.Windows.Media;

namespace obServer.Model.GameModel.Item
{
    public sealed class Weapon : BaseItem, IWeapon, IBaseItem
    {
        public const int PistolCapacity = 20;
        public const int ShotgunCapacity = 8;
        public const int MachinegunCapacity = 30;
        public const int PistolShoot = 1;
        public const int ShotgunShoot = 6;
        public const int MachinegunShoot = 3;
        public const int PistolSpeed = 120;
        public const int ShotgunSpeed = 200;
        public const int MachinegunSpeed = 150;

        public Weapon(Geometry geometry, Guid id, double[] position, double rotation, bool impact, int bulletCapacity, int bulletShoot, double bulletSpeed, double damage, double bulletWeight) : base(geometry, id, position, rotation, impact)
        {
            BulletRandomizer = new Random();
            this.bulletCapacity = bulletCapacity;
            this.bulletCount = BulletRandomizer.Next(0, bulletCapacity + 1);
            this.bulletShoot = bulletShoot;
            this.bulletSpeed = bulletSpeed;
            this.damage = damage;
            this.bulletWeight = bulletWeight;
        }

        private Random BulletRandomizer;
        private int bulletCapacity;
        private int bulletShoot;
        private double bulletSpeed;
        private double damage;
        private double bulletWeight;
        private int bulletCount;

        public IBullet[] DoShoot()
        {
            if (bulletCount > 0)
            {
                bulletCount--;
                Bullet[] bullets = new Bullet[bulletShoot];
                for (int i = 0; i < bullets.Length; i++)
                {
                    double rot = Rotation + (BulletRandomizer.Next(-1, 2) * BulletRandomizer.NextDouble() * 10);
                    double[] direction = BulletDirection(rot);
                    bullets[i] = new Bullet(
                        Bullet.BulletGeometry,
                        Guid.NewGuid(),
                        new double[] 
                        {
                            Position.X,
                            Position.Y
                        },
                        Rotation,
                        true,
                        bulletSpeed,
                        damage,
                        direction,
                        bulletWeight);
                }
                return bullets;
            }
            return null;
        }


        public void Move(double xMovement, double yMovement, double rotation)
        {
            ChangePosition(xMovement, yMovement, rotation);
        }
        public void DoReload(int storedBullets)
        {
            if (storedBullets > (bulletCapacity - bulletCount))
            {
                bulletCount = bulletCapacity;
            }
            else
            {
                bulletCount += storedBullets;
            }
        }

        private double[] BulletDirection(double angle)
        {
            Matrix m = Matrix.Identity;
            m.Rotate(angle);
            Vector trans = m.Transform(new Vector(0,1));
            return new double[] { trans.X, trans.Y};
        }
    }
}
