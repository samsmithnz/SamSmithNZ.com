using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Threading.Tasks;
using System.Collections.Generic;
using SamSmithNZ.Service;
using System.Diagnostics;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TournamentOddsTests : BaseIntegrationTest
    {
        [TestMethod()]
        public async Task TouramentEuro2024OddsTest()
        {
            //arrange
            GameController gameController = new(new GameDataAccess(base.Configuration));
            int tournamentCode = 317;

            //act
            List<Game> games = await gameController.GetGamesByTournament(tournamentCode);
            foreach (Game game in games)
            {
                int team1Won = 0;
                int team2Won = 0;
                int teamDraw = 0;
                int sampleSize = 10000;
                game.GameCanEndInADraw = true;
                double team1ChanceToWin = game.Team1ChanceToWin;
                double team2ChanceToWin = game.Team2ChanceToWin;
                double teamChanceToDraw = game.TeamChanceToDraw;
                Debug.WriteLine(game.Team1Name + " has a " + team1ChanceToWin + "% chance to win against " + game.Team2Name + ", with a " + teamChanceToDraw + "% chance to draw");
                for (int i = 0; i < sampleSize; i++)
                {
                    double randomNumber = Utility.GenerateRandomNumber(0, 100);
                    if (randomNumber < team1ChanceToWin )
                    {
                        team1Won++;
                    }
                    else if (randomNumber < (team1ChanceToWin + teamChanceToDraw ))
                    {
                        teamDraw++;
                    }
                    else
                    {
                        team2Won++;
                    }
                }
                Debug.WriteLine(game.Team1Name + " won " + team1Won + " times, for " + ((double)team1Won / (double)sampleSize).ToString("0.00%") + " of the time against " + game.Team2Name);
                Debug.WriteLine(game.Team1Name + " drew " + teamDraw + " times, for " + ((double)teamDraw / (double)sampleSize).ToString("0.00%") + " of the time against " + game.Team2Name);
                Debug.WriteLine(game.Team1Name + " lost " + team2Won + " times, for " + ((double)team2Won / (double)sampleSize).ToString("0.00%") + " of the time against " + game.Team2Name);
                break;
            }

        }
    }
}