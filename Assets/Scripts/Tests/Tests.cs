using System.Collections.Generic;
using System.Linq;
using _2_Game;
using NUnit.Framework;
using UnityEditor;

namespace Tests
{
    public class Tests
    {

        [Test]
        public void Test5PlayerTeamsCreation()
        {
            var playerIds = new List<ulong>()
            {
                0, 1, 2, 3, 4
            };

            var teams = new Teams(playerIds);
            
            Assert.AreEqual(5, teams.AllPlayerInfo().Count());
            Assert.AreEqual(1, teams.AllPlayerInfo().Count(tuple => tuple.Item3 == Role.Hitler));
            Assert.AreEqual(4, teams.AllPlayerInfo().Count(tuple => tuple.Item3 == Role.Member));
            Assert.AreEqual(3, teams.AllPlayerInfo().Count(tuple => tuple.Item2 == Alignment.Liberal));
            Assert.AreEqual(2, teams.AllPlayerInfo().Count(tuple => tuple.Item2 == Alignment.Fascist));
        }
        
        [Test]
        public void Test6PlayerTeamsCreation()
        {
            var playerIds = new List<ulong>()
            {
                0, 1, 2, 3, 4, 5
            };

            var teams = new Teams(playerIds);
            
            Assert.AreEqual(6, teams.AllPlayerInfo().Count());
            Assert.AreEqual(1, teams.AllPlayerInfo().Count(tuple => tuple.Item3 == Role.Hitler));
            Assert.AreEqual(5, teams.AllPlayerInfo().Count(tuple => tuple.Item3 == Role.Member));
            Assert.AreEqual(4, teams.AllPlayerInfo().Count(tuple => tuple.Item2 == Alignment.Liberal));
            Assert.AreEqual(2, teams.AllPlayerInfo().Count(tuple => tuple.Item2 == Alignment.Fascist));
        }
        
        [Test]
        public void Test7PlayerTeamsCreation()
        {
            var playerIds = new List<ulong>()
            {
                0, 1, 2, 3, 4, 5, 6
            };

            var teams = new Teams(playerIds);
            
            Assert.AreEqual(7, teams.AllPlayerInfo().Count());
            Assert.AreEqual(1, teams.AllPlayerInfo().Count(tuple => tuple.Item3 == Role.Hitler));
            Assert.AreEqual(6, teams.AllPlayerInfo().Count(tuple => tuple.Item3 == Role.Member));
            Assert.AreEqual(4, teams.AllPlayerInfo().Count(tuple => tuple.Item2 == Alignment.Liberal));
            Assert.AreEqual(3, teams.AllPlayerInfo().Count(tuple => tuple.Item2 == Alignment.Fascist));
        }

        [Test]
        public void DrawCards()
        {
            var policies = new Policies();
            for (int i = 0; i < 15; i++)
            {
                policies.DrawThree();
            }
        }
    }
}