using System.Collections.Generic;

namespace _2_Game
{
    public class GameState
    {
        private Teams teams;
        private Policies policies;
        public GameState(IReadOnlyList<ulong> playerIds)
        {
            teams = new Teams(playerIds);
            policies = new Policies();
        }
    }
}