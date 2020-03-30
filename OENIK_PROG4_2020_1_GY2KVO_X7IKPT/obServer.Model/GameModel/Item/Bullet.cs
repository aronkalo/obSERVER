using obServer.Model.Interfaces;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace obServer.Model.GameModel.Item
{
    public sealed class Bullet : BaseItem, IBullet
    {
        public static EllipseGeometry BulletGeometry { get { return new EllipseGeometry() { RadiusX = BulletWidth, RadiusY = BulletHeight }; } }
        private static Brush BulletBrush { get { return new ImageBrush(new BitmapImage()); } }
        private const double BulletWidth = 5;
        private const double BulletHeight = 5;
        private double flySpeed;
        private double supressionCache;
        private double weight;
        private double damage;
        private double[] direction;
        private double[] startPos;
        private double supression 
        {
            get 
            {
                double a = (startPos[0] - Position[0]); 
                double b = (startPos[1] - Position[1]);
                double sup = 1 - (Math.Sqrt((a * a) + (b * b)) * weight);
                supressionCache =  sup > 0 ? sup : 0;
                return supressionCache;
            }
        }
        public Bullet(Geometry geometry, Guid id, double[] position, double rotation,  bool impact, double flySpeed, double damage, double[] direction, double weight) : base(geometry, id, position, rotation, impact)
        {
            this.flySpeed = flySpeed;
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
            double xMovement = direction[0] * flySpeed * deltaTime * supression;   
            double yMovement = direction[1] * flySpeed * deltaTime * supression;
            ChangePosition(xMovement, yMovement, 0);
        }

        public void DoDamage(IPlayer player)
        {
            player.Damaged(damage);
        }
    }
}
