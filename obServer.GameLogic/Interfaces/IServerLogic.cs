// <copyright file="IServerLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Logic.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The server logic interface.
    /// </summary>
    public interface IServerLogic
    {
        /// <summary>
        /// Serve.
        /// </summary>
        void Serve();

        /// <summary>
        /// Send game start.
        /// </summary>
        void SendGameStart();

        /// <summary>
        /// The update method.
        /// </summary>
        /// <param name="deltaTime">delta time.</param>
        void Update(double deltaTime);
    }
}
