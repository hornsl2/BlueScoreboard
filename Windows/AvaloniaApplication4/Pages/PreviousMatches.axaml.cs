using Avalonia.Controls;
using AvaloniaApplication4.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AvaloniaApplication4.Pages
{
    public partial class PreviousMatches: Window
    {

        private readonly ViewModel viewModel;
        public PreviousMatches()
        {


            List<GameScoreModel> GameScoreModel = new List<GameScoreModel>()
            {
                new GameScoreModel(){

                Team1Score=1,
                Team2Score=2,
                    //currentMatch = new GameScore(),
                   

                }

            };
       //     Gamecore gamescore = new GameScore();
       //    List<GameScore> previousMatches=GameScore.GetFakePreviousGames();


           /* 
            GameScoreModel GameScoreModel = new GameScoreModel
            {
            //currentMatch = new GameScore(),
            //previousMatches = new List<GameScore>()

            };

          //  GameScoreModel.currentMatch.Team1Score = 1;
          //  GameScoreModel.currentMatch.Team2Score = 2;
            GameScoreModel.Team1Score = 1;
            GameScoreModel.Team2Score = 2;

            GameScore gameScore = new GameScore();

            
            /
                        this.viewModel = new ViewModel() {
                            DataGridItems = new List<DataGridItems>()
                            {
                                new DataGridItems() {  Team1Score=1 , Team2Score=2, GameType="darts"} ,
                                new DataGridItems() { Team1Score=5 , Team2Score=6, GameType="shuffleboard" }

                            }

                        };
            */
            
            InitializeComponent();
            this.viewModel = new ViewModel()
            {
               // gameScoreModel = GameScoreModel,
             //    previousMatches = previousMatches
                // DataGridItems = new List<DataGridItems>()
                //         {
                //               new DataGridItems() {  Team1Score=1 , Team2Score=2, GameType="darts"} ,
                //             new DataGridItems() { Team1Score=5 , Team2Score=6, GameType="shuffleboard" }
                //
                //       }
            };





            this.DataContext = this.viewModel;
           // myGrid.DataContext= Employee.GetEmployees();
         
        }
    }

 









}
