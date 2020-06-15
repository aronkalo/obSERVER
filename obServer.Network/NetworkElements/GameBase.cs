// <copyright file="GameBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.NetworkElements
{
    using System.Diagnostics;

    /// <summary>
    /// GameBase abstract class.
    /// </summary>
    public abstract class GameBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameBase"/> class.
        /// </summary>
        protected GameBase()
        {
            this.Sw = new Stopwatch();
            this.Sw.Start();
        }

        /// <summary>
        /// Gets connection time.
        /// </summary>
        public double ConnectionTime
        {
            get { return this.Sw.ElapsedMilliseconds / 1000; }
        }

        private Stopwatch Sw { get; set; }
    }
}
