using System;
using System.Windows.Media;

namespace obServer.GameModel.Interfaces
{
    public interface IStaticItem : IBaseItem
    {
        string Type { get; set; }

        double[] Dimensions { get; set; }
    }
}