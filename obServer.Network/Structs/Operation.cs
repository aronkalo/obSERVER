// <copyright file="Operation.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.Structs
{
    /// <summary>
    /// Operation enum field.
    /// </summary>
    public enum Operation
    {
        /// <summary>
        /// Numer of connection.
        /// </summary>
        Connect = 1,

        /// <summary>
        /// Number of Disconnect.
        /// </summary>
        StartGame = 2,

        /// <summary>
        /// Number of Check the server.
        /// </summary>
        CheckServerAvaliable = 3,

        /// <summary>
        /// Number of Object sending.
        /// </summary>
        SendObject = 4,

        /// <summary>
        /// Number of removing.
        /// </summary>
        Remove = 5,

        /// <summary>
        /// Number of Dying.
        /// </summary>
        Die = 6,

        /// <summary>
        /// Number of hitting.
        /// </summary>
        Hit = 7,

        /// <summary>
        /// Number of Sending chat messages.
        /// </summary>
        GetClientList = 8,

        /// <summary>
        /// Number of shooting.
        /// </summary>
        Shoot = 9,

        /// <summary>
        /// Number of moving.
        /// </summary>
        Move = 10,

        /// <summary>
        /// Number of picking up something.
        /// </summary>
        Pickup = 11,

        /// <summary>
        /// Number of dropping something.
        /// </summary>
        Drop = 12,
    }
}
