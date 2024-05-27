using System.Collections.Generic;

namespace _2_Game
{
    public class GameState
    {
        public readonly Teams Teams;
        public readonly Policies Policies;
        public GameState(IReadOnlyList<ulong> playerIds)
        {
            Teams = new Teams(playerIds);
            Policies = new Policies();
        }
    }
}