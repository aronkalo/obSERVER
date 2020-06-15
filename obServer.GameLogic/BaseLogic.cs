// <copyright file="BaseLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using obServer.Network.Structs;

    /// <summary>
    /// The base logic class.
    /// </summary>
    public abstract class BaseLogic
    {
        /// <summary>
        /// Gets milis.
        /// </summary>
        public double Milis
        {
            get
            {
                return DateTime.Now.Ticks;
            }
        }

        /// <summary>
        /// Handle request.
        /// </summary>
        /// <param name="request">request.</param>
        protected virtual void HandleRequests(Request request)
        {
        }

        /// <summary>
        /// Handle move.
        /// </summary>
        /// <param name="request">request.</param>
        protected virtual void HandleMove(Request request)
        {
        }

        /// <summary>
        /// Handle request.
        /// </summary>
        /// <param name="request">request.</param>
        protected virtual void HandleDrop(Request request)
        {
        }

        /// <summary>
        /// Handle request.
        /// </summary>
        /// <param name="request">request.</param>
        protected virtual void HandleConnect(Request request)
        {
        }

        /// <summary>
        /// Handle request.
        /// </summary>
        /// <param name="request">request.</param>
        protected virtual void HandleStartGame(Request request)
        {
        }

        /// <summary>
        /// Handle request.
        /// </summary>
        /// <param name="request">request.</param>
        protected virtual void HandleShoot(Request request)
        {
        }

        /// <summary>
        /// Handle request.
        /// </summary>
        /// <param name="request">request.</param>
        protected virtual void HandleDie(Request request)
        {
        }

        /// <summary>
        /// Handle request.
        /// </summary>
        /// <param name="request">request.</param>
        protected virtual void HandleSendObject(Request request)
        {
        }

        /// <summary>
        /// Handle request.
        /// </summary>
        /// <param name="request">request.</param>
        protected virtual void HandlePickup(Request request)
        {
        }

        /// <summary>
        /// Handle request.
        /// </summary>
        /// <param name="request">request.</param>
        protected virtual void HandleRemove(Request request)
        {
        }

        /// <summary>
        /// Handle request.
        /// </summary>
        /// <param name="request">request.</param>
        protected virtual void HandleReady(Request request)
        {
        }

        /// <summary>
        /// Handle request.
        /// </summary>
        /// <param name="request">request.</param>
        protected virtual void HandleHit(Request request)
        {
        }

        /// <summary>
        /// Handle request.
        /// </summary>
        /// <param name="request">request.</param>
        protected virtual void HandleSendMessage(Request request)
        {
        }
    }
}
