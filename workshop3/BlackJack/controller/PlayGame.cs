using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.controller
{
    class PlayGame : model.CardDealtListener
    {
        private view.IView m_view;
        private model.Game m_game;
        public bool Play(model.Game a_game, view.IView a_view)
        {
            m_view = a_view;
            m_game = a_game;

            a_view.DisplayWelcomeMessage();

            a_view.DisplayDealerHand(a_game.GetDealerHand(), a_game.GetDealerScore());
            a_view.DisplayPlayerHand(a_game.GetPlayerHand(), a_game.GetPlayerScore());

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

            //a_view.GetInput();
            ////int input = a_view.GetInput();

            //if (a_view.WantsToPlay())
            //{
            //    a_game.NewGame();
            //}
            //if (a_view.WantsToHit())
            //{
            //    a_game.Hit();
            //}
            //if (a_view.WantsToStand())
            //{
            //    a_game.Stand();
            //}

            return action != view.Action.Quit;

        }

        //Känns helknepigt att implementera från modellen..
        public void CardDealt(model.Card a_card, model.Player a_player)
        {
            System.Threading.Thread.Sleep(750);

            m_view.DisplayResults(m_game.GetPlayerHand(), m_game.GetPlayerScore(), m_game.GetDealerHand(), m_game.GetDealerScore());
        }
    }
}
