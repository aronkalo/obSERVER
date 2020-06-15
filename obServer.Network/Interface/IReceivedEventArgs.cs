// <copyright file="IReceivedEventArgs.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using obServer.Network.Structs;

    /// <summary>
    /// ReceivingEventArgs interface.
    /// </summary>
    public interface IReceivedEventArgs
    {
        /// <summary>
        /// Gets or sets Received requests.
        /// </summary>
        Request ReceivedRequest { get; set; }
    }
}
