using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GoalTests : BaseIntegrationTest
    {
        [TestMethod()]
        public async Task GoalsFirstItemTest()
        {
            //arrange
            GoalDataAccess da = new(base.Configuration);
            int gameCode = 7328; //This is the perfect test game. It has a normal goal, injury time goal, penalty and own goal

            //act
            List<Goal> results = await da.GetListByGame(gameCode);

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
            GoalDataAccess da = new(base.Configuration);
            int gameCode = 7328; //This is the perfect test game. It has a normal goal, injury time goal, penalty and own goal

            //act
            List<Goal> results = await da.GetListByGame(gameCode);
            //Save the four goals
            await da.SaveItem(results[0]);
            await da.SaveItem(results[1]);
            await da.SaveItem(results[2]);
            await da.SaveItem(results[3]);
            //Test that the results haven't changed
            results = await da.GetListByGame(gameCode);

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

        [TestMethod]
        public async Task GoalsInjuryTimeAreInRightPlaceTest()
        {
            //arrange
            GoalDataAccess da = new(base.Configuration);

            //act
            List<Goal> results = await da.GetList();
            int injuryTimeGoalsNotInInjuryTime = 0;
            foreach (Goal item in results)
            {
                if (item.GoalTime < 90 && item.GoalTime != 45 && item.InjuryTime > 0)
                {
                    injuryTimeGoalsNotInInjuryTime++;
                }
                else if (item.GoalTime > 90 && item.GoalTime < 120 && item.InjuryTime > 0)
                {
                    injuryTimeGoalsNotInInjuryTime++;
                }
            }

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual(0, injuryTimeGoalsNotInInjuryTime);
        }

        [TestMethod]
        public void GoalsHTMLRegularGoalTest()
        {
            //arrange
            GoalDataAccess da = new(base.Configuration);
            string goalText = "Modrić &#160;41&#39;";
            string playerName = "Modrić";

            //act
            List<Goal> goals = da.ProcessGoalHTML(goalText, playerName);

            //assert
            Assert.AreEqual(1, goals.Count);
            Assert.AreEqual(41, goals[0].GoalTime);
            Assert.AreEqual(0, goals[0].InjuryTime);
            Assert.AreEqual(false, goals[0].IsPenalty);
            Assert.AreEqual(false, goals[0].IsOwnGoal);
        }

        [TestMethod]
        public void GoalsHTMLInjuryTimeGoalTest()
        {
            //arrange
            GoalDataAccess da = new(base.Configuration);
            string goalText = "Payet &#160;90+6&#39;";
            string playerName = "Payet";

            //act
            List<Goal> goals = da.ProcessGoalHTML(goalText, playerName);

            //assert
            Assert.AreEqual(1, goals.Count);
            Assert.AreEqual(90, goals[0].GoalTime);
            Assert.AreEqual(6, goals[0].InjuryTime);
            Assert.AreEqual(false, goals[0].IsPenalty);
            Assert.AreEqual(false, goals[0].IsOwnGoal);
        }

        [TestMethod]
        public void GoalsHTMLPenaltyTimeGoalTest()
        {
            //arrange
            GoalDataAccess da = new(base.Configuration);
            string goalText = "Stancu &#160;65&#39;&#160;(pen.)";
            string playerName = "Stancu";

            //act
            List<Goal> goals = da.ProcessGoalHTML(goalText, playerName);

            //assert
            Assert.AreEqual(1, goals.Count);
            Assert.AreEqual(65, goals[0].GoalTime);
            Assert.AreEqual(0, goals[0].InjuryTime);
            Assert.AreEqual(true, goals[0].IsPenalty);
            Assert.AreEqual(false, goals[0].IsOwnGoal);
        }

        [TestMethod]
        public void GoalsHTMLOwnGoalTimeGoalTest()
        {
            //arrange
            GoalDataAccess da = new(base.Configuration);
            string goalText = "Clark &#160;71&#39;&#160;(o.g.)";
            string playerName = "Clark";

            //act
            List<Goal> goals = da.ProcessGoalHTML(goalText, playerName);

            //assert
            Assert.AreEqual(1, goals.Count);
            Assert.AreEqual(71, goals[0].GoalTime);
            Assert.AreEqual(0, goals[0].InjuryTime);
            Assert.AreEqual(false, goals[0].IsPenalty);
            Assert.AreEqual(true, goals[0].IsOwnGoal);
        }

        [TestMethod]
        public void GoalsHTMLMultipleGoalTest()
        {
            //arrange
            GoalDataAccess da = new(base.Configuration);
            string goalText = "Ronaldo &#160;50&#39;,&#160;62&#39;";
            string playerName = "Ronaldo";

            //act
            List<Goal> goals = da.ProcessGoalHTML(goalText, playerName);

            //assert
            Assert.AreEqual(2, goals.Count);
            Assert.AreEqual(50, goals[0].GoalTime);
            Assert.AreEqual(0, goals[0].InjuryTime);
            Assert.AreEqual(false, goals[0].IsPenalty);
            Assert.AreEqual(false, goals[0].IsOwnGoal);
            Assert.AreEqual(62, goals[1].GoalTime);
            Assert.AreEqual(0, goals[1].InjuryTime);
            Assert.AreEqual(false, goals[1].IsPenalty);
            Assert.AreEqual(false, goals[1].IsOwnGoal);
        }

        [TestMethod]
        public void GoalsHTMLMultipleHatTrickGoalTest()
        {
            //arrange
            GoalDataAccess da = new(base.Configuration);
            string goalText = "Villa 20&#39;, 44&#39;, 75&#39;";
            string playerName = "Villa";

            //act
            List<Goal> goals = da.ProcessGoalHTML(goalText, playerName);

            //assert
            Assert.AreEqual(3, goals.Count);
            Assert.AreEqual(20, goals[0].GoalTime);
            Assert.AreEqual(0, goals[0].InjuryTime);
            Assert.AreEqual(false, goals[0].IsPenalty);
            Assert.AreEqual(false, goals[0].IsOwnGoal);
            Assert.AreEqual(44, goals[1].GoalTime);
            Assert.AreEqual(0, goals[1].InjuryTime);
            Assert.AreEqual(false, goals[1].IsPenalty);
            Assert.AreEqual(false, goals[1].IsOwnGoal);
            Assert.AreEqual(75, goals[2].GoalTime);
            Assert.AreEqual(0, goals[2].InjuryTime);
            Assert.AreEqual(false, goals[2].IsPenalty);
            Assert.AreEqual(false, goals[2].IsOwnGoal);
        }

        [TestMethod]
        public void GoalsHTMLMultipleWithPenGoalTest()
        {
            //arrange
            GoalDataAccess da = new(base.Configuration);
            string goalText = "Griezmann &#160;45+2&#39;&#160;(pen.),&#160;72&#39;";
            string playerName = "Griezmann";

            //act
            List<Goal> goals = da.ProcessGoalHTML(goalText, playerName);

            //assert
            Assert.AreEqual(2, goals.Count);
            Assert.AreEqual(45, goals[0].GoalTime);
            Assert.AreEqual(2, goals[0].InjuryTime);
            Assert.AreEqual(true, goals[0].IsPenalty);
            Assert.AreEqual(false, goals[0].IsOwnGoal);
            Assert.AreEqual(72, goals[1].GoalTime);
            Assert.AreEqual(0, goals[1].InjuryTime);
            Assert.AreEqual(false, goals[1].IsPenalty);
            Assert.AreEqual(false, goals[1].IsOwnGoal);
        }
    }
}
