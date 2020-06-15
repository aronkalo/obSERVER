// <copyright file="MusicPlayer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.ViewController.Sound
{
    using System;
    using System.Diagnostics;
    using System.Windows.Media;

    /// <summary>
    /// Music player class.
    /// </summary>
    public static class MusicPlayer
    {
        private static bool canPlay = true;

        /// <summary>
        /// Filename field.
        /// </summary>
        private static string fileName = string.Empty;

        private static MediaPlayer musicplayer = new MediaPlayer();

        private static string directory = System.IO.Directory.GetCurrentDirectory();

        private static double duration;

        private static Stopwatch timer = new Stopwatch();

        /// <summary>
        /// Gets or sets a value indicating whether play.
        /// </summary>
        public static bool CanPlay
        {
            get
            {
                return canPlay;
            }

            set
            {
                canPlay = value;
            }
        }

        /// <summary>
        /// Playsound method.
        /// </summary>
        /// <param name="fileName">File string.</param>
        public static void PlaySound(string fileName)
        {
            MusicPlayer.fileName = fileName;
            musicplayer.Open(new Uri(directory + fileName));
            if (musicplayer.NaturalDuration.HasTimeSpan)
            {
                duration = musicplayer.NaturalDuration.TimeSpan.TotalSeconds;
            }

            musicplayer.Play();
            timer.Restart();
        }

        /// <summary>
        /// Restart method.
        /// </summary>
        public static void Restart()
        {
            if (timer.Elapsed.TotalSeconds > duration)
            {
                if (fileName != string.Empty)
                {
                    PlaySound(fileName);
                }
            }
        }

        /// <summary>
        /// Stop method.
        /// </summary>
        public static void Stop()
        {
            musicplayer.Pause();
        }
    }
}
