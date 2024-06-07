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
        public void TestTeamsCreation()
        {
            var playerIds = new List<ulong>()
            {
                0, 1, 2, 3, 4
            };

            var teams = new Teams(playerIds);
            
            Assert.AreEqual(5, teams.AllPlayerInfo().Count());
            Assert.AreEqual(teams.AllPlayerInfo().Count(tuple => tuple.Item3 == Role.Hitler), 1);
        }
    }
}