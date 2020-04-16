using obServer.GameModel.Interfaces;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace obServer.GameModel.Item
{
    public sealed class Bullet : BaseItem, IBullet
    {
        public static EllipseGeometry BulletGeometry { get { return new EllipseGeometry() { RadiusX = BulletWidth, RadiusY = BulletHeight }; } }
        private const double BulletWidth = 3;
        private const double BulletHeight = 3;
        private double flySpeed;
        private double supressionCache;
        private double realspeedCache;
        private double weight;
        private double damage;
        private double[] direction;
        private double[] startPos;
        private double supression
        {
            get
            {
                double a = (startPos[0] - Position.X);
                double b = (startPos[1] - Position.Y);
                double sup = 1 - (Math.Sqrt((a * a) + (b * b)) * weight);
                supressionCache = sup > 0 ? sup : 0;
                return supressionCache;
            }
        }

        public double RealSpeed
        {
            get { return realspeedCache; }
        }

        public double BulletDamage
        {
            get
            {
                return damage;
            }
        }

        public double[] Direction
        {
            get
            {
                return direction;
            }
        }

        public double Weight
        {
            get
            {
                return weight;
            }
        }

        public double Speed
        {
            get
            {
                return flySpeed;
            }
        }
        public Bullet(Geometry geometry, Guid id, double[] position, double rotation,  bool impact, double flySpeed, double damage, double[] direction, double weight) : base(geometry, id, position, rotation, impact)
        {
            this.flySpeed = flySpeed;
            this.realspeedCache = flySpeed;
            this.damage = damage;
            this.direction = new double[2];
            this.direction[0] = direction[0];
            this.direction[1] = direction[1];
            this.startPos = new double[2];
            this.startPos[0] = position[0];
            this.startPos[1] = position[1];
            this.supressionCache = 1;
            this.weight = weight;
        }

        public void Fly(double deltaTime)
        {
            realspeedCache = flySpeed * supression;
            double xMovement = direction[0] * realspeedCache * deltaTime;   
            double yMovement = direction[1] * realspeedCache * deltaTime;
            ChangePosition(xMovement, yMovement, 0);
        }

        public void DoDamage(IPlayer player)
        {
            player.Damaged(damage);
        }
    }
}
