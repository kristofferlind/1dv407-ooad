﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    class Dealer : Player
    {
        private Deck m_deck = null;
        private const int g_maxScore = 21;

        private rules.INewGameStrategy m_newGameRule;
        private rules.IHitStrategy m_hitRule;
        private rules.IWinStrategy m_winRule;

        public Dealer(rules.RulesFactory a_rulesFactory)
        {
            m_newGameRule = a_rulesFactory.GetNewGameRule();
            m_hitRule = a_rulesFactory.GetHitRule();
            m_winRule = a_rulesFactory.GetWinRule();
        }

        public bool NewGame(Player a_player)
        {
            if (m_deck == null || IsGameOver())
            {
                m_deck = new Deck();
                ClearHand();
                a_player.ClearHand();
                return m_newGameRule.NewGame(m_deck, this, a_player);   
            }
            return false;
        }

        public bool Hit(Player a_player)
        {
            if (m_deck != null && a_player.CalcScore() < g_maxScore && !IsGameOver())
            {
                Deal(a_player, true);

                return true;
            }
            return false;
        }

        public bool Stand()
        {
            if (m_deck != null)
            {
                ShowHand();

                while (m_hitRule.DoHit(this))
                {
                    Deal(this, true);
                }
                return true;
            }
            return false;
        }

        public void Deal(Player a_player, bool show)
        {
            Card c = m_deck.GetCard();
            c.Show(show);
            a_player.DealCard(c);
        }

        /*
         hand.addCard() {
         * get
         * show
         * deal
         */

        public bool IsDealerWinner(Player a_player)
        {
            return m_winRule.isDealerWinner(a_player.CalcScore(), CalcScore());
        }

        public bool IsGameOver()
        {
            return (m_deck != null && !m_hitRule.DoHit(this));
        }
    }
}
