using System.Collections.Generic;
using System.Linq;
using Unity.Collections;

namespace _2_Game
{
    public class GameState
    {
        public readonly Teams Teams;
        public readonly Policies Policies;
        private readonly Dictionary<ulong, string> playerNames;

        public GameState(IEnumerable<(ulong OwnerClientId, FixedString32Bytes playerName)> players)
        {
            var playerIds = players.Select(p => p.OwnerClientId);
            Teams = new Teams(playerIds);
            Policies = new Policies();
            playerNames = new Dictionary<ulong, string>();
            foreach (var (id, playerName) in players)
            {
                playerNames.Add(id, playerName.ToString());
            }
        }

        public string GetName(ulong playerId) => playerNames[playerId];
    }
}