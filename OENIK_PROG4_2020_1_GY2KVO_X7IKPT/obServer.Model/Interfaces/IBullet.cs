﻿using System;
using System.Windows.Media;

namespace obServer.Model.Interfaces
{
    public interface IBullet : IBaseItem
    {
        void Fly(double deltaTime);
        double DoDamage();
    }
}