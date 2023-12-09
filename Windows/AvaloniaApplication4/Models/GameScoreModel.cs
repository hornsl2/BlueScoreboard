using AvaloniaApplication4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication4.Models
{
    class GameScoreModel : INotifyPropertyChanged
    {

        public GameScoreModel(int team1, int team2)
        {
            this.Team1Score = team1;
            this.Team2Score = team2;
        }

        public GameScoreModel() : this(0, 0)
        {
        }


        private int team1Score; public int Team1Score
        {
            get { return team1Score; }
            set { team1Score = value; RaiseTeam1Changed(); }
        }
        private int team2Score; public int Team2Score
        {
            get { return team2Score; }
            set { team2Score = value; RaiseTeam2Changed(); }
        }

        private int duration; public int Duration
        {
            get { return duration; }
            set { duration = value; RaiseDurationChanged(); }
        }


        //public GameScore currentMatch { get; set; }

        // public List<GameScore> previousMatches { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        private void RaiseTeam1Changed([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        private void RaiseTeam2Changed([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        private void RaiseDurationChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }





    }
    
}
