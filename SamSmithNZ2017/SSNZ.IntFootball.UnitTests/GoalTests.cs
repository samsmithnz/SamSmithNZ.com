using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;

namespace SSNZ.IntFootball.UnitTests
{
    [TestClass]
    public class GoalTests
    {
        [TestMethod]
        public async Task GoalsExistTest()
        {
            //arrange
            GoalDataAccess da = new GoalDataAccess();
            int gameCode = 7328;

            //act
            List<Goal> results = await da.GetListAsync(gameCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task GoalsFirstItemTest()
        {
            //arrange
            GoalDataAccess da = new GoalDataAccess();
            int gameCode = 7328; //This is the perfect test game. It has a normal goal, injury time goal, penalty and own goal

            //act
            List<Goal> results = await da.GetListAsync(gameCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].GameCode > 0);
            Assert.IsTrue(results[0].GoalCode > 0);
            Assert.IsTrue(results[0].GoalTime == 11);
            Assert.IsTrue(results[0].InjuryTime == 0);
            Assert.IsTrue(results[0].IsOwnGoal == true);
            Assert.IsTrue(results[0].IsPenalty == false);
            Assert.IsTrue(results[0].PlayerCode > 0);
            Assert.IsTrue(results[0].PlayerName == "Marcelo");
            Assert.IsTrue(results[1].GameCode > 0);
            Assert.IsTrue(results[1].GoalCode > 0);
            Assert.IsTrue(results[1].GoalTime == 29);
            Assert.IsTrue(results[1].InjuryTime == 0);
            Assert.IsTrue(results[1].IsOwnGoal == false);
            Assert.IsTrue(results[1].IsPenalty == false);
            Assert.IsTrue(results[1].PlayerCode > 0);
            Assert.IsTrue(results[1].PlayerName == "Neymar");
            Assert.IsTrue(results[2].GameCode > 0);
            Assert.IsTrue(results[2].GoalCode > 0);
            Assert.IsTrue(results[2].GoalTime == 71);
            Assert.IsTrue(results[2].InjuryTime == 0);
            Assert.IsTrue(results[2].IsOwnGoal == false);
            Assert.IsTrue(results[2].IsPenalty == true);
            Assert.IsTrue(results[2].PlayerCode > 0);
            Assert.IsTrue(results[2].PlayerName == "Neymar");
            Assert.IsTrue(results[3].GameCode > 0);
            Assert.IsTrue(results[3].GoalCode > 0);
            Assert.IsTrue(results[3].GoalTime == 90);
            Assert.IsTrue(results[3].InjuryTime == 1);
            Assert.IsTrue(results[3].IsOwnGoal == false);
            Assert.IsTrue(results[3].IsPenalty == false);
            Assert.IsTrue(results[3].PlayerCode > 0);
            Assert.IsTrue(results[3].PlayerName == "Oscar");
        }

        [TestMethod()]
        public async Task GoalsSaveItemTest()
        {
            //arrange
            GoalDataAccess da = new GoalDataAccess();
            int gameCode = 7328; //This is the perfect test game. It has a normal goal, injury time goal, penalty and own goal

            //act
            List<Goal> results = await da.GetListAsync(gameCode);
            //Save the four goals
            await da.SaveItemAsync(results[0]);
            await da.SaveItemAsync(results[1]);
            await da.SaveItemAsync(results[2]);
            await da.SaveItemAsync(results[3]);
            //Test that the results haven't changed
            results = await da.GetListAsync(gameCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].GameCode > 0);
            Assert.IsTrue(results[0].GoalCode > 0);
            Assert.IsTrue(results[0].GoalTime == 11);
            Assert.IsTrue(results[0].InjuryTime == 0);
            Assert.IsTrue(results[0].IsOwnGoal == true);
            Assert.IsTrue(results[0].IsPenalty == false);
            Assert.IsTrue(results[0].PlayerCode > 0);
            Assert.IsTrue(results[0].PlayerName == "Marcelo");
            Assert.IsTrue(results[1].GameCode > 0);
            Assert.IsTrue(results[1].GoalCode > 0);
            Assert.IsTrue(results[1].GoalTime == 29);
            Assert.IsTrue(results[1].InjuryTime == 0);
            Assert.IsTrue(results[1].IsOwnGoal == false);
            Assert.IsTrue(results[1].IsPenalty == false);
            Assert.IsTrue(results[1].PlayerCode > 0);
            Assert.IsTrue(results[1].PlayerName == "Neymar");
            Assert.IsTrue(results[2].GameCode > 0);
            Assert.IsTrue(results[2].GoalCode > 0);
            Assert.IsTrue(results[2].GoalTime == 71);
            Assert.IsTrue(results[2].InjuryTime == 0);
            Assert.IsTrue(results[2].IsOwnGoal == false);
            Assert.IsTrue(results[2].IsPenalty == true);
            Assert.IsTrue(results[2].PlayerCode > 0);
            Assert.IsTrue(results[2].PlayerName == "Neymar");
            Assert.IsTrue(results[3].GameCode > 0);
            Assert.IsTrue(results[3].GoalCode > 0);
            Assert.IsTrue(results[3].GoalTime == 90);
            Assert.IsTrue(results[3].InjuryTime == 1);
            Assert.IsTrue(results[3].IsOwnGoal == false);
            Assert.IsTrue(results[3].IsPenalty == false);
            Assert.IsTrue(results[3].PlayerCode > 0);
            Assert.IsTrue(results[3].PlayerName == "Oscar");
        }
    }
}
