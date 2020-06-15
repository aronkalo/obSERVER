// <copyright file="SoundPlayer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.ViewController.Sound
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Media;

    /// <summary>
    /// The sound player.
    /// </summary>
    public class SoundPlayer
    {
        private const string Movement = @"\sounds\footsteps.wav";
        private const string Shoot = @"\sounds\shoot.wav";
        private const string Reload = @"\sounds\reload.wav";
        private const string BackSound = @"\sounds\backsound.mp3";
        private string directory = Directory.GetCurrentDirectory();
        private MediaPlayer selfMovementPlayer;
        private MediaPlayer shootPlayer;
        private MediaPlayer backSoundPlayer;
        private Stopwatch movementTimer;
        private Stopwatch otherMovementTimer;
        private Stopwatch backTimer;
        private double moveDuration = 0.5;
        private double otherMoveDuration = 0.5;
        private double backDuration = 25;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundPlayer"/> class.
        /// </summary>
        public SoundPlayer()
        {
            this.selfMovementPlayer = new MediaPlayer();
            this.shootPlayer = new MediaPlayer();
            this.backSoundPlayer = new MediaPlayer();
            this.movementTimer = new Stopwatch();
            this.movementTimer.Start();
            this.otherMovementTimer = new Stopwatch();
            this.otherMovementTimer.Start();
            this.backTimer = new Stopwatch();
            this.backTimer.Start();
        }

        /// <summary>
        /// Gets or sets a value indicating whether other shooting.
        /// </summary>
        public bool OtherShoot { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether other move.
        /// </summary>
        public bool OtherMove { get; set; }

        /// <summary>
        /// Gets or sets the other move balance.
        /// </summary>
        public double OtherMoveBalance { get; set; }

        /// <summary>
        /// Gets or sets the other move volume.
        /// </summary>
        public double OtherMoveVolume { get; set; }

        /// <summary>
        /// Gets or sets the other shoot balance.
        /// </summary>
        public double OtherShootBalance { get; set; }

        /// <summary>
        /// Gets or sets the other shoot volume.
        /// </summary>
        public double OtherShootVolume { get; set; }

        /// <summary>
        /// The move sound.
        /// </summary>
        public void MoveSound()
        {
            if (this.moveDuration < this.movementTimer.Elapsed.TotalSeconds)
            {
                this.selfMovementPlayer.Open(new Uri(this.directory + Movement));
                this.selfMovementPlayer.Volume = 0.3;
                this.selfMovementPlayer.Play();
                this.movementTimer.Restart();
            }
        }

        /// <summary>
        /// The shoot sound.
        /// </summary>
        public void ShootSound()
        {
            this.shootPlayer.Open(new Uri(this.directory + Shoot));
            this.shootPlayer.Volume = 0.7;
            this.shootPlayer.Play();
        }

        /// <summary>
        /// The Evironment sound.
        /// </summary>
        public void EnvironmentSound()
        {
            if (this.backDuration < this.backTimer.Elapsed.TotalSeconds)
            {
                this.backSoundPlayer.Open(new Uri(this.directory + BackSound));
                this.backSoundPlayer.Volume = 0.05;
                this.backSoundPlayer.Play();
                this.backTimer.Restart();
            }
        }

        /// <summary>
        /// The sound updator.
        /// </summary>
        public void UpdateSounds()
        {
            this.EnvironmentSound();
            if (this.OtherMove)
            {
                this.OtherMoveSound();
                this.OtherMove = false;
            }

            if (this.OtherShoot)
            {
                this.OtherShootSound();
                this.OtherShoot = false;
            }
        }

        /// <summary>
        /// Reload sound playing.
        /// </summary>
        public void ReloadSound()
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(this.directory + Reload));
            player.Volume = 0.6;
            player.Play();
        }

        private void OtherMoveSound()
        {
            if (this.otherMoveDuration < this.otherMovementTimer.Elapsed.TotalSeconds)
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(this.directory + Movement));
                player.Balance = this.OtherMoveBalance;
                player.Volume = this.OtherShootVolume;
                player.Play();
                this.otherMovementTimer.Restart();
            }
        }

        private void OtherShootSound()
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(this.directory + Shoot));
            player.Balance = this.OtherMoveBalance;
            player.Volume = this.OtherMoveVolume;
            player.Play();
        }
    }
}
