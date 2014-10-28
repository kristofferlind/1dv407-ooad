using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class PlayerWinStrategy : IWinStrategy
    {
        private int maxScore = 21;
        public bool isDealerWinner(int playerScore, int dealerScore)
        {
            //player tjock?
            if (playerScore > maxScore)
            {
                return true;
            }

            //dealer tjock?
            if (dealerScore > maxScore)
            {
                return false;
            }

            //player vinner vid lika
            return dealerScore > playerScore;
        }
    }
}
