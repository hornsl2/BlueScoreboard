using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication4.Models
{
    public class GameScore : INotifyCollectionChanged, INotifyPropertyChanged
    {
        public GameScore(int team1, int team2)
        {
            this.Team1Score = team1;
            this.Team2Score = team2;
        }

        public GameScore() : this(0, 0)
        {
        }

        private int team1Score; public int Team1Score
        {
            get { return team1Score; }
            set { team1Score = value; RaiseProperChanged(); }
        }
        private int team2Score; public int Team2Score
        {
            get { return team2Score; }
            set { team2Score = value; RaiseProperChanged(); }
        }

        private TimeSpan duration; public TimeSpan Duration
        {
            get { return duration; }
            set { duration = value; RaiseProperChanged(); }
        }

        private string gameType; public string GameType
        {
            get { return gameType; }
            set { gameType = value; RaiseCollectionChanged(); }
        }


        private ObservableCollection<GameScore> previousMatches; public ObservableCollection<GameScore> PreviousMatches
        {
            get
            {
                var toReturn = new ObservableCollection<GameScore>();
                foreach (GameScore game in previousMatches)
                {
                    toReturn.Add(game);

                }
                RaiseCollectionChanged(0);
                return toReturn;
            }
            set { previousMatches = value; RaiseCollectionChanged(0); }
        }
        public GameScore GetPreviousGame(int index)
        {

            return previousMatches.ElementAt(index);
        }

        public bool ResetNewGame()
        {
            previousMatches.Add(new GameScore() { team1Score = this.team1Score, team2Score = this.team2Score });
            team1Score = 0;
            team2Score = 0;
            return true;
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        private void RaiseCollectionChanged(NotifyCollectionChangedAction caller = 0)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(caller));
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(caller));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
        //public static ObservableCollection<GameScore> GetFakePreviousGames()


        public ObservableCollection<GameScore> GetFakePreviousGames()
        {
            // List<GameScore> fakePrevious = new List<GameScore>();
            ObservableCollection<GameScore> fakePrevious = new ObservableCollection<GameScore>();
            fakePrevious.Add(new GameScore() { team1Score = 4, team2Score = 5 });
            fakePrevious.Add(new GameScore() { team1Score = 2, team2Score = 3 });
            fakePrevious.Add(new GameScore() { team1Score = 7, team2Score = 8 });
            fakePrevious.Add(new GameScore() { team1Score = 1, team2Score = 2 });
            this.PreviousMatches = fakePrevious;
            return fakePrevious;
            //return previousGames;
        }
    }
}
