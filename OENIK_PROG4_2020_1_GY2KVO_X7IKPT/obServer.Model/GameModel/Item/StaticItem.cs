using obServer.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace obServer.Model.GameModel.Item
{
    public sealed class StaticItem : BaseItem,IStaticItem
    {
        public StaticItem(Geometry geometry, Guid id, double[] position, double rotation, double[] dimensions, bool impact, string type) : base(geometry, id, position, rotation, impact)
        {
            Dimensions = dimensions;
            Type = type;
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private double[] dimensions;

        public double[] Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

    }
}
