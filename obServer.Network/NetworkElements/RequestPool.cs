// <copyright file="RequestPool.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.NetworkElements
{
    using System;
    using obServer.Network.Structs;

    /// <summary>
    /// Request pool class.
    /// </summary>
    internal sealed class RequestPool : BasePool<Request>
    {
        /// <summary>
        /// Gets or sets the requests.
        /// </summary>
        /// <returns>Pool element.</returns>
        public Request GetRequest()
        {
            return this.GetPoolElement();
        }

        /// <summary>
        /// Gets back a pool element. If no more, throw exception.
        /// </summary>
        /// <returns>Returns a pool element.</returns>
        protected override Request GetPoolElement()
        {
            if (this.NotNullElement())
            {
                return base.GetPoolElement();
            }
            else
            {
                throw new Exception("RequestPool Contains no element");
            }
        }
    }
}