using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    interface IWinStrategy
    {
        bool isDealerWinner(int playerScore, int dealerScore);
    }
}
