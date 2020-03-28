using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace obServer.Model.GameModel.Item
{
    public sealed class Bullet : BaseItem
    {
        public static EllipseGeometry BulletGeometry { get { return new EllipseGeometry() { RadiusX = BulletWidth, RadiusY = BulletHeigth }; } }
        private const double BulletWidth = 5;
        private const double BulletHeigth = 5;

        public Bullet(Geometry geometry, string id, bool impact) : base(geometry, id, impact)
        {

        }

        protected override void ChangePosition(double x, double y, double angle = 0)
        {
            base.ChangePosition(x, y, angle);
        }

        protected override void SetPosition(double x, double y)
        {
            base.SetPosition(x, y);
        }
    }
}
