using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace _2_Game
{
    public class Teams
    {
        private readonly Dictionary<ulong, Membership> memberships = new();

        public Teams(IReadOnlyList<ulong> playerIds)
        {
            // This should shuffle somewhere
            
            var (liberals, fascists) = LiberalsAndFascistCount(playerIds.Count);
            var hitlerIndex = fascists + liberals - 1;
            for (int i = 0; i < liberals; i++)
            {
                var liberal = new Membership(Alignment.Liberal, Role.Member);
                memberships.Add(playerIds[i], liberal);
            }

            for (int i = liberals; i < hitlerIndex; i++)
            {
                var fascist = new Membership(Alignment.Fascist, Role.Member);
                memberships.Add(playerIds[i], fascist);
            }

            var hitler = new Membership(Alignment.Fascist, Role.Hitler);
            memberships.Add(playerIds[hitlerIndex], hitler);
            
            Assert.AreEqual(memberships.Count, playerIds.Count);
        }

        private (int, int) LiberalsAndFascistCount(int playerCount)
        {
            var offset = playerCount % 2;
            var liberals = playerCount / 2 + offset;
            var fascists = liberals;
            if (liberals == fascists)
            {
                liberals++;
                fascists--;
            }

            return (liberals, fascists);
        }

        public Alignment GetAlignment(ulong playerId)
        {
            return memberships[playerId].Alignment;
        }

        public Role GetRole(ulong playerId)
        {
            return memberships[playerId].Role;
        }
    }
}