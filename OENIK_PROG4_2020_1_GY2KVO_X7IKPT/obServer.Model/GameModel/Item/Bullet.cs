using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace obServer.Model.GameModel.Item
{
    public sealed class Bullet : BaseItem
    {
        public static EllipseGeometry BulletGeometry { get { return new EllipseGeometry() { RadiusX = BulletWidth, RadiusY = BulletHeigth }; } }
        public static Brush BulletBrush { get { return new ImageBrush(new BitmapImage()); } }
        private const double BulletWidth = 5;
        private const double BulletHeigth = 5;
        private double flySpeed;
        private double damage;
        private double[] direction;
        public Bullet(Geometry geometry, string id, double[] position, double rotation,  bool impact, double flySpeed, double damage, double[] direction) : base(geometry, id, position, rotation, impact)
        {
            this.flySpeed = flySpeed;
            this.damage = damage;
            this.direction = new double[2];
            this.direction[0] = direction[0];
            this.direction[1] = direction[1];
        }

        public void Fly(double deltaTime)
        {
            double xMovement = direction[0] * flySpeed * deltaTime;   
            double yMovement = direction[1] * flySpeed * deltaTime;
            ChangePosition(xMovement, yMovement, 0);
        }

        public double DoDamage()
        {
            return damage;
        }

        protected override void ChangePosition(double x, double y, double angle = 0)
        {
            base.ChangePosition(x, y);
        }

        protected override void SetPosition(double x, double y)
        {
            base.SetPosition(x, y);
        }
    }
}
