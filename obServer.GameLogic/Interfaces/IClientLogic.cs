// <copyright file="IClientLogic.cs" company="PlaceholderCompany">
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
    /// The client logic interface.
    /// </summary>
    public interface IClientLogic
    {
        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        Dictionary<string, string> Clients { get; set; }

        /// <summary>
        /// Gets or sets connect event.
        /// </summary>
        EventHandler Connect { get; set; }

        /// <summary>
        /// Gets or sets death event.
        /// </summary>
        EventHandler Death { get; set; }

        /// <summary>
        /// Gets or sets render event.
        /// </summary>
        EventHandler Render { get; set; }

        /// <summary>
        /// Gets or sets start event.
        /// </summary>
        EventHandler Start { get; set; }

        /// <summary>
        /// Gets or sets reload event.
        /// </summary>
        EventHandler Reload { get; set; }

        /// <summary>
        /// Gets or sets pick event.
        /// </summary>
        EventHandler Pick { get; set; }

        /// <summary>
        /// Gets or sets sound event.
        /// </summary>
        EventHandler SoundActive { get; set; }

        /// <summary>
        /// Gets or sets shoot event.
        /// </summary>
        EventHandler Shoot { get; set; }

        /// <summary>
        /// Removes the visual objects.
        /// </summary>
        void RemoveVisuals();

        /// <summary>
        /// Checks the client list.
        /// </summary>
        void CheckState();

        /// <summary>
        /// Appends the player.
        /// </summary>
        void AppendPlayer();

        /// <summary>
        /// Pickup weapon.
        /// </summary>
        /// <param name="sender">null.</param>
        /// <param name="e">empty.</param>
        void OnPickup(object sender, EventArgs e);

        /// <summary>
        /// Reload weapon.
        /// </summary>
        /// <param name="sender">null.</param>
        /// <param name="e">empty.</param>
        void OnReload(object sender, EventArgs e);

        /// <summary>
        /// Move player.
        /// </summary>
        /// <param name="sender">player.</param>
        /// <param name="e">empty.</param>
        void OnMove(object sender, EventArgs e);

        /// <summary>
        /// Shoot weapon.
        /// </summary>
        /// <param name="sender">null.</param>
        /// <param name="e">empty.</param>
        void OnShoot(object sender, EventArgs e);

        /// <summary>
        /// Bullet update method.
        /// </summary>
        /// <param name="deltaTime">deltaTime.</param>
        void FlyBullets(double deltaTime);

        /// <summary>
        /// Ready the client.
        /// </summary>
        /// <param name="name">Username.</param>
        void Ready(string name);
    }
}
