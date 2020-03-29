using obServer.Model.Interfaces;
using System;
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
                double[] direction = BulletRotation();
                for (int i = 0; i < bullets.Length; i++)
                {
                    bullets[i] = new Bullet(
                        Bullet.BulletGeometry,
                        Guid.NewGuid(),
                        new double[] 
                        {
                            Position[0],
                            Position[1]
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
            return new Bullet[0];
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

        private double[] BulletRotation()
        {
            double xbase = 0;
            double ybase = -1;
            double rad = Rotation / 180 * Math.PI;
            double sin = Math.Sin(rad);
            double cos = Math.Cos(rad);
            return new double[] 
            {
                xbase * cos - ybase * sin,
                ybase * sin - xbase * cos,
            };
        }
    }
}
