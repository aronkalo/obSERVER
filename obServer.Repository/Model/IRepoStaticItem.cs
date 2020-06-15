// <copyright file="IRepoStaticItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Repository.Model
{
    /// <summary>
    /// Static item repo interface.
    /// </summary>
    public interface IRepoStaticItem
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
