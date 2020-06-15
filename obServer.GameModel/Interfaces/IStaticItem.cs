// <copyright file="IStaticItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameModel.Interfaces
{
    using System;
    using System.Windows.Media;

    /// <summary>
    /// Static items interface.
    /// </summary>
    public interface IStaticItem : IBaseItem
    {
        /// <summary>
        /// Gets or sets the type of static item.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Gets or sets the dimensions of static item.
        /// </summary>
        double[] Dimensions { get; set; }
    }
}