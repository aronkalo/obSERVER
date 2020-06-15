// <copyright file="IGameBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// IGameBase interface.
    /// </summary>
    public interface IGameBase
    {
        /// <summary>
        /// Gets back the connection time.
        /// </summary>
        double ConnectionTime { get; }
    }
}
