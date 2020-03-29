using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace obServer.Model.GameModel.Item
{
    public sealed class StaticItem : BaseItem
    {
        public StaticItem(Geometry geometry, Guid id, double[] position, double rotation, bool impact) : base(geometry, id, position, rotation, impact)
        {
        }
    }
}
