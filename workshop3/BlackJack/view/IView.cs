using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.view
{
    interface IView
    {
        void DisplayWelcomeMessage();
        Action GetInput();
        void DisplayCard(model.Card a_card);
        void DisplayResults(IEnumerable<model.Card> a_playerHand, int a_playerScore, IEnumerable<model.Card> a_dealerHand, int a_dealerScore);
        void DisplayPlayerHand(IEnumerable<model.Card> a_hand, int a_score);
        void DisplayDealerHand(IEnumerable<model.Card> a_hand, int a_score);
        void DisplayGameOver(bool a_dealerIsWinner);
    }
}
