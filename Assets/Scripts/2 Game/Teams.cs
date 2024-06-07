using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace _2_Game
{
    public class Teams
    {
        private readonly Dictionary<ulong, Membership> memberships;

        public Teams(IEnumerable<ulong> playerIds)
        {
            var playerCount = playerIds.Count();
            var shuffledPlayerIds = playerIds.OrderBy(x => Random.value);
            var (liberals, fascists) = LiberalsAndFascistCount(playerCount);

            memberships = Enumerable.Repeat(new Membership(Alignment.Liberal, Role.Member), liberals)
                .Concat(Enumerable.Repeat(new Membership(Alignment.Fascist, Role.Member), fascists))
                .Zip(shuffledPlayerIds, (membership, id) => (membership, id))
                .ToDictionary(tuple => tuple.id, tuple => tuple.membership);

            if (memberships.Count >= 3)
            {
                var fascist = memberships.Values.First(membership => membership.Alignment == Alignment.Fascist);
                fascist.Role = Role.Hitler;
            }
        }

        private static (int, int) LiberalsAndFascistCount(int playerCount)
        {
            var liberals = playerCount / 2 + 1;
            var fascists = playerCount - liberals;
            return (liberals, fascists);
        }

        public Alignment GetAlignment(ulong playerId) => memberships[playerId].Alignment;

        public Role GetRole(ulong playerId) => memberships[playerId].Role;

        public IEnumerable<ulong> Fascists() =>
            memberships
                .Where(pair => pair.Value.Alignment == Alignment.Fascist)
                .Select(pair => pair.Key);

        public IEnumerable<(ulong, Alignment, Role)> AllPlayerInfo() =>
            memberships.Select(player => (player.Key, player.Value.Alignment, player.Value.Role));
    }
}