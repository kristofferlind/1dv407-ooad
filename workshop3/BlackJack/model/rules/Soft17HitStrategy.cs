using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class Soft17HitStrategy : IHitStrategy
    {
        private const int g_hitLimit = 17;

        public bool DoHit(model.Player a_dealer)
        {
            int[] cardScores = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };
            var score = 0;
            var cards = a_dealer.GetHand();
            var numberOfAces = 0;
            foreach (var c in cards)
            {
                
                if (c.GetValue() != Card.Value.Hidden)
                {
                    score += cardScores[(int)c.GetValue()];

                    if (c.GetValue() == Card.Value.Ace)
                    {
                        numberOfAces++;
                    }
                }
            }

            while ((score == g_hitLimit || score > 21) && numberOfAces > 0)
            {
                numberOfAces--;
                score -= 10;
            }

            return score < g_hitLimit;
        }
    }
}
