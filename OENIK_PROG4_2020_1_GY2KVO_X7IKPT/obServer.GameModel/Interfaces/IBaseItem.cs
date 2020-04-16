using System;
using System.Windows;
using System.Windows.Media;

namespace obServer.GameModel.Interfaces
{
    public interface IBaseItem
    {
        Guid Id { get; set; }
        Vector Position { get; set; }
        double Rotation { get; set; }
        bool Impact { get; set; }
        Geometry RealPrimitive { get; }
        bool CollidesWith(Geometry geometry);
    }
}