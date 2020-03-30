﻿using System;
using System.Windows.Media;

namespace obServer.Model.Interfaces
{
    public interface IBaseItem
    {
        Guid Id { get; set; }
        double[] Position { get; set; }
        double Rotation { get; set; }
        bool Impact { get; set; }
        Geometry RealPrimitive { get; }
        bool CollidesWith(Geometry geometry);
    }
}