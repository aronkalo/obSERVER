using System;
using System.Windows.Media;

namespace obServer.Model.Interfaces
{
    public interface IStaticItem : IBaseItem
    {
        string Type { get; set; }

        double[] Dimensions { get; set; }
    }
}