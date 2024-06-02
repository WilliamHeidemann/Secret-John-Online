using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Random = UnityEngine.Random;

namespace _2_Game
{
    public class Policies
    {
        private readonly Stack<Alignment> drawPile;
        private readonly List<Alignment> discardPile;
        private int enactedLiberalPolicies;
        private int enactedFascistPolicies;

        public Policies()
        {
            drawPile = new Stack<Alignment>();
            discardPile = new List<Alignment>();
            for (var i = 0; i < 11; i++)
                discardPile.Add(Alignment.Fascist);

            for (var i = 0; i < 6; i++)
                discardPile.Add(Alignment.Liberal);
            
            ReShuffle();
        }

        public (Alignment, Alignment, Alignment) DrawThree()
        {
            var cards = new List<Alignment>();
            while (cards.Count < 3)
            {
                if (drawPile.Count == 0) ReShuffle();
                var topCard = drawPile.Pop();
                cards.Add(topCard);
                discardPile.Add(topCard);
            }

            return (cards[0], cards[1], cards[2]);
        }

        private void ReShuffle()
        {
            for (var i = 1; i < discardPile.Count; i++)
            {
                var swapIndex = Random.Range(0, i);
                (discardPile[i], discardPile[swapIndex]) = (discardPile[swapIndex], discardPile[i]);
            }

            foreach (var card in discardPile)
            {
                drawPile.Push(card);
            }
        }

        public void Discard(Alignment card)
        {
            discardPile.Add(card);
        }

        public void EnactPolicy(Alignment policy)
        {
            if (policy == Alignment.Liberal) enactedLiberalPolicies++;
            else enactedFascistPolicies++;
        }
    }
}