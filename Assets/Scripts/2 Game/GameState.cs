using System.Collections.Generic;

namespace _2_Game
{
    public class GameState
    {
        private Dictionary<ulong, Membership> memberships = new();
        private int enactedFascistPolicies;
        private int enactedLiberalPolicies;
    }
}