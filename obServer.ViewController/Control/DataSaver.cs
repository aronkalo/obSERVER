// <copyright file="DataSaver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.ViewController.Control
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The data saver.
    /// </summary>
    public class DataSaver
    {
        private const string FileName = @"\MatchHistory.log";
        private const string MatchHeader = "[Match]-DateTime:";
        private string directory = Directory.GetCurrentDirectory();

        /// <summary>
        /// Saves a player.
        /// </summary>
        /// <param name="player">the player name.</param>
        /// <param name="position">the position.</param>
        public void SavePlayer(string player, int position)
        {
            string[] lines = File.ReadAllLines(this.directory + FileName);
            string[] newLines = new string[lines.Length + 1];
            Array.Copy(lines, 0, newLines, 0, lines.Length);
            newLines[lines.Length] = $"Placement: [{position}.] - Name: [{player}] Time: [{DateTime.Now.ToString()}]";
            File.WriteAllLines(this.directory + FileName, newLines);
        }

        /// <summary>
        /// Saving match method.
        /// </summary>
        /// <param name="places">places string.</param>
        public void SaveMatch(string[] places)
        {
            string[] fileString = new string[places.Length + 1];
            fileString[0] = MatchHeader + DateTime.Now.ToString();
            Array.Copy(places, 0, fileString, 1, places.Length);
            File.WriteAllLines(this.directory + FileName, fileString);
        }

        /// <summary>
        /// New match method.
        /// </summary>
        /// <param name="gameName">name.</param>
        /// <param name="time">time.</param>
        internal void NewMatch(string gameName, string time)
        {
            string[] lines = File.ReadAllLines(this.directory + FileName);
            string[] newLines = new string[lines.Length + 1];
            Array.Copy(lines, 0, newLines, 0, lines.Length);
            newLines[lines.Length] = $"{MatchHeader}#{gameName}#{time}";
            File.WriteAllLines(this.directory + FileName, newLines);
        }
    }
}
