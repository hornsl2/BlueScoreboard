using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication4.Models;
using ButtonFiles;
using EventArgs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;

namespace AvaloniaApplication4
{
    public partial class MainWindow : Window
    {

        private ViewModel viewModel;

        public MainWindow()
        {
            Buttons buttons = new Buttons();
            gthis.WindowState = WindowState.FullScreen;
            List<string> boolList = new List<string>();

            IEnumerable<GameScore> GameScoreModel = createGameScoreModel(0,0,new TimeSpan(0,0,0));

            //Dummy Data
            List<GameScore> prevGameScoreModel = createPrevGameModel();
          
            
            CountUpTimer timer = new CountUpTimer();

            InitializeComponent();
            this.viewModel = new ViewModel()
            {
                gameScoreModel = GameScoreModel,
                previousMatches = prevGameScoreModel,
                buttonDebugOutput = buttons.toReturn
            };

            this.DataContext = this.viewModel;

            buttons.ButtonChanged += ButtonChanged;
            timer.TimeChanged += TimeChanged;
        }

        public void TimeChanged(object sender, TimerEventArgs e)
        {
            var oldtime = this.viewModel.gameScoreModel.ToList().ElementAt(0).Duration;
            var newTime = oldtime.Add(new TimeSpan(0, 0, 1));

            this.viewModel.gameScoreModel.ToList().ElementAt(0).Duration = newTime;
        }

        public void ButtonChanged(object sender, ButtonEventArgs e)
        {
            switch (Int32.Parse((e.offset.ToString().Split("s")[1])))
            {
                #region "Team 1 Buttons"
                case 0: case 5: case 10: case 11://reset

                    this.viewModel.gameScoreModel.ToList().ElementAt(0).Duration = new TimeSpan(0, 0, 0);
                    if (this.viewModel.gameScoreModel.ToList().ElementAt(0).Team1Score > 0 || this.viewModel.gameScoreModel.ToList().ElementAt(0).Team2Score > 0)
                    {

                        this.viewModel.previousMatches.ElementAt(0).PreviousMatches.Add(new GameScore(this.viewModel.gameScoreModel.ToList().ElementAt(0).Team1Score, this.viewModel.gameScoreModel.ToList().ElementAt(0).Team2Score));
                        this.viewModel.previousMatches.ElementAt(0).PreviousMatches.ElementAt(0);
                        this.viewModel.gameScoreModel.ToList().ElementAt(0).Team1Score = 0;
                        this.viewModel.gameScoreModel.ToList().ElementAt(0).Team2Score = 0;

                    }
                    break;
                case 1: case 6://team 1 score ++
                    this.viewModel.gameScoreModel.ToList().ElementAt(0).Team1Score++;
                    break;
                case 2: case 7: // Team 1 score --
                    this.viewModel.gameScoreModel.ToList().ElementAt(0).Team1Score = (this.viewModel.gameScoreModel.ToList().ElementAt(0).Team1Score > 0) ? --this.viewModel.gameScoreModel.ToList().ElementAt(0).Team1Score : 0;
                    break;
                case 3: case 8://team 2 score ++
                    this.viewModel.gameScoreModel.ToList().ElementAt(0).Team2Score++;
                    break;
                case 4: case 9://team 2 score --
                    this.viewModel.gameScoreModel.ToList().ElementAt(0).Team2Score = (this.viewModel.gameScoreModel.ToList().ElementAt(0).Team2Score > 0) ? --this.viewModel.gameScoreModel.ToList().ElementAt(0).Team2Score : 0;
                    break;
                default:
                    break;
                    #endregion

            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public List<GameScore> createGameScoreModel(int team1Score, int team2Score, TimeSpan duration)
        {
            return new List<GameScore>()
            {
                new GameScore(){
                Team1Score=team1Score,
                Team2Score=team2Score,
                Duration=duration

                }
            };

        }

        public List<GameScore> createPrevGameModel()
            {

            ObservableCollection<GameScore> prevGameScoreModela = new ObservableCollection<GameScore>()
            {
                new GameScore(){

                Team1Score=10,
                Team2Score=10

                }
            };

            return new List<GameScore>()
            {
                new GameScore(){

                Team1Score=11,
                Team2Score=12,
                PreviousMatches=prevGameScoreModela

                },

                 new GameScore(){

                Team1Score=21,
                Team2Score=22,
                PreviousMatches=prevGameScoreModela

                },
                 new GameScore(){

                Team1Score=31,
                Team2Score=31,
                PreviousMatches=prevGameScoreModela
            }
            };

    }



    }//MainWindow


}//Namespace
