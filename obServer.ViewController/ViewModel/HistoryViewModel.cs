// <copyright file="HistoryViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.ViewController.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    /// <summary>
    /// The history view model.
    /// </summary>
    public class HistoryViewModel : ViewModelBase
    {
        private const string FileName = @"\MatchHistory.log";
        private string sel;
        private string directory = Directory.GetCurrentDirectory();
        private Dictionary<string, string[]> matchHistory;
        private string date;
        private string sumGames;
        private string avgPlayers;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryViewModel"/> class.
        /// </summary>
        public HistoryViewModel()
        {
            this.Places = new ObservableCollection<string>();
            this.matchHistory = new Dictionary<string, string[]>();
            if (File.Exists(this.directory + FileName))
            {
                string[] lines = File.ReadAllLines(this.directory + FileName);
                List<string> players = new List<string>();
                string match = string.Empty;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("[Match]-DateTime") && players.Count == 0)
                    {
                        string[] prefixes = lines[i].Split('#');
                        match = prefixes[1] + " Time:" + prefixes[2];
                    }
                    else if (lines[i].Contains("[Match]-DateTime"))
                    {
                        this.matchHistory.Add(match, players.ToArray());
                        string[] prefixes = lines[i].Split('#');
                        match = prefixes[1] + " Time:" + prefixes[2];
                        players.Clear();
                    }
                    else
                    {
                        players.Add(lines[i]);
                    }
                }

                this.Date = DateTime.Now.ToString();
                this.matchHistory.Add(match, players.ToArray());
                this.Matches = new string[this.matchHistory.Keys.Count];
                for (int i = 0; i < this.Matches.Length; i++)
                {
                    this.Matches[i] = this.matchHistory.Keys.ElementAt(i);
                }

                this.SumGames = $"{this.Matches.Length} Games";

                int sum = 0;

                foreach (var item in this.matchHistory)
                {
                    sum += item.Value.Length;
                }

                this.AvgPlayers = $"{sum / this.matchHistory.Count} Player(s)";
            }
        }

        /// <summary>
        /// Gets or sets places.
        /// </summary>
        public ObservableCollection<string> Places { get; set; }

        /// <summary>
        /// Gets or sets Matches.
        /// </summary>
        public string[] Matches { get; set; }

        /// <summary>
        /// Gets or sets Date.
        /// </summary>
        public string Date
        {
            get
            {
                return this.date;
            }

            set
            {
                this.date = value;
                this.RaisePropertyChanged(() => this.Date);
            }
        }

        /// <summary>
        /// Gets or sets Sel.
        /// </summary>
        public string Sel
        {
            get
            {
                return this.sel;
            }

            set
            {
                this.sel = value;
                this.SelChanged(this.sel);
                this.RaisePropertyChanged(() => this.Sel);
            }
        }

        /// <summary>
        /// Gets or sets games.
        /// </summary>
        public string SumGames
        {
            get
            {
                return this.sumGames;
            }

            set
            {
                this.sumGames = value;
                this.RaisePropertyChanged(() => this.SumGames);
            }
        }

        /// <summary>
        /// Gets or sets games.
        /// </summary>
        public string AvgPlayers
        {
            get
            {
                return this.avgPlayers;
            }

            set
            {
                this.avgPlayers = value;
                this.RaisePropertyChanged(() => this.AvgPlayers);
            }
        }

        private void SelChanged(string match)
        {
            this.Places.Clear();
            this.Date = match.Split(':')[1];
            var players = this.matchHistory.Where(x => x.Key == match).First().Value;
            foreach (var item in players)
            {
                this.Places.Add(item);
            }
        }
    }
}
