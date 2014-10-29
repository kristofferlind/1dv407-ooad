using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BlackJack.controller
{
    class PlayGame : model.CardDealtListener
    {
        private view.IView m_view;
        private model.Game m_game;
        private bool isWelcomed;

        public bool Play(model.Game a_game, view.IView a_view)
        {
            m_view = a_view;
            m_game = a_game;


            if (!isWelcomed)
            {
                isWelcomed = true;
                a_view.DisplayWelcomeMessage();
                m_view.DisplayRules(m_game.GetHitRule(), m_game.GetNewGameRule(), m_game.GetWinRule());
            }
            else { 
                a_view.DisplayResults(a_game.GetPlayerHand(), a_game.GetPlayerScore(), a_game.GetDealerHand(), a_game.GetDealerScore());
            }

            if (a_game.IsGameOver())
            {
                a_view.DisplayGameOver(a_game.IsDealerWinner());
            }

            view.Action action = a_view.GetInput();

            switch (action)
            {
                case view.Action.NewGame:
                    a_game.NewGame();
                    break;
                case view.Action.Hit:
                    a_game.Hit();
                    break;
                case view.Action.Stand:
                    a_game.Stand();
                    break;
            }

            return action != view.Action.Quit;

        }

        //Card and player never used. This is however not the problem, 
        //the issue here is that we're redrawing the entire view instead of adding a card.
        //However, this is solved in the hand in for grade 5.
        public void CardDealt(model.Card a_card, model.Player a_player)
        {
            Thread.Sleep(500);

            m_view.DisplayResults(m_game.GetPlayerHand(), m_game.GetPlayerScore(), m_game.GetDealerHand(), m_game.GetDealerScore());
        }
    }
}
