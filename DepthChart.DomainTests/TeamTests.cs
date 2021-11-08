using Microsoft.VisualStudio.TestTools.UnitTesting;
using DepthChart.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DepthChart.Domain.Tests
{
    [TestClass()]
   
    public class TeamTests
    {

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod()]
        public void returnPlayerPositions()
        {
            var league = new League("NFL");
            var qbPosition = league.AddSupportingPosition("QB");
            league.AddSupportingPosition("WR");
            league.AddSupportingPosition("KR");
            var team = new Team(league, "Buffalo Bills");
            var alice = team.AddPlayer("Alice");
            var bob = team.AddPlayer("Bob");
            var sam = team.AddPlayer("Sam");
            var rob = team.AddPlayer("Rob");
            var kate = team.AddPlayer("Kate");



            team.UpdatePlayerPosition(alice.Id, qbPosition.Id, 0);
            team.UpdatePlayerPosition(bob.Id, qbPosition.Id, 1);
            team.UpdatePlayerPosition(sam.Id, qbPosition.Id, 1);
            team.UpdatePlayerPosition(rob.Id, qbPosition.Id, 3);
            team.UpdatePlayerPosition(kate.Id, qbPosition.Id);

            Assert.AreEqual(alice.Id, team.PlayerPositions.ElementAtOrDefault(0).Player.Id);
            Assert.AreEqual(bob.Id, team.PlayerPositions.ElementAtOrDefault(1).Player.Id);
            Assert.AreEqual(sam.Id, team.PlayerPositions.ElementAtOrDefault(2).Player.Id);
        }

        [TestMethod()]
        public void returnPlayerBackupPlayerPositions()
        {
            var league = new League("NFL");
            var qbPosition = league.AddSupportingPosition("QB");
            league.AddSupportingPosition("WR");
            league.AddSupportingPosition("KR");

            var team = new Team(league, "Buffalo Bills");
            var alice = team.AddPlayer("Alice");
            var bob = team.AddPlayer("Bob");
            var sam = team.AddPlayer("Sam");
            var rob = team.AddPlayer("Rob");
            var kate = team.AddPlayer("Kate");



            team.UpdatePlayerPosition(alice.Id, qbPosition.Id, 0);
            team.UpdatePlayerPosition(bob.Id, qbPosition.Id, 1);
            team.UpdatePlayerPosition(sam.Id, qbPosition.Id, 1);
            team.UpdatePlayerPosition(rob.Id, qbPosition.Id, 3);
            team.UpdatePlayerPosition(kate.Id, qbPosition.Id);

          

            var backupPlayerPositions = team.GetBackupPlayerPositions(sam.Id, qbPosition.Id);

            Assert.AreEqual(alice.Id, backupPlayerPositions.ElementAtOrDefault(0).Player.Id);
            Assert.AreEqual(sam.Id, backupPlayerPositions.ElementAtOrDefault(1).Player.Id);
        }

        [TestMethod()]
        public void removePlayerFromDepthChartTest()
        {
            var league = new League("NFL");
            var qbPosition = league.AddSupportingPosition("QB");
            league.AddSupportingPosition("WR");
            league.AddSupportingPosition("KR");

            var team = new Team(league, "Bills");
            var alice = team.AddPlayer("Alice");
            var bob = team.AddPlayer("Bob");

            team.UpdatePlayerPosition(alice.Id, qbPosition.Id, 0);
            team.UpdatePlayerPosition(bob.Id, qbPosition.Id, 1);


            team.removePlayerFromDepthChart(alice.Id, qbPosition.Id, 0);
        }

        [TestMethod()]
        public void getFullDepthChartTest()
        {
            
        var league = new League("NFL");
        var qbPosition = league.AddSupportingPosition("QB");
        league.AddSupportingPosition("WR");
        league.AddSupportingPosition("KR");

          

        var team = new Team(league, "Bills");
        var alice = team.AddPlayer("Alice");
        var bob = team.AddPlayer("Bob");

        team.UpdatePlayerPosition(alice.Id, qbPosition.Id, 0);
        team.UpdatePlayerPosition(bob.Id, qbPosition.Id, 1);

        

            List<string> list = new List<string>();
            list= team.getFullDepthChart();
            foreach (var item in list)
            {
              
                TestContext.WriteLine(item);
            }
          
        }
    }
}