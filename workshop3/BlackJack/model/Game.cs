using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackJack.model.rules;

namespace BlackJack.model
{
    class Game
    {
        private Dealer m_dealer;
        private Player m_player;
        private RulesFactory rules;
        private VisitorGetRules fetchedRules;

        public Game(AbstractRulesFactory ruleSet)
        {
            rules = new RulesFactory(ruleSet);
            m_dealer = new Dealer(rules);
            m_player = new Player();
            PrepareGetRules();
        }

        private void PrepareGetRules()
        {
            fetchedRules = new VisitorGetRules();
            rules.Accept(fetchedRules);
        }

        public string GetHitRule()
        {
            return fetchedRules.HitRuleName;
        }

        public string GetNewGameRule()
        {
            return fetchedRules.NewGameRuleName;
        }
        
        public string GetWinRule() 
        {
            return fetchedRules.WinRuleName;
        }

            
        public void Subscribe(CardDealtListener a_subscriber)
        {
            m_player.Subscribe(a_subscriber);
            m_dealer.Subscribe(a_subscriber);
        }

        public bool IsGameOver()
        {
            return m_dealer.IsGameOver();
        }

        public bool IsDealerWinner()
        {
            return m_dealer.IsDealerWinner(m_player);
        }

        public bool NewGame()
        {
            return m_dealer.NewGame(m_player);
        }

        public bool Hit()
        {
            return m_dealer.Hit(m_player);
        }

        public bool Stand()
        {
            return m_dealer.Stand();
        }

        public IEnumerable<Card> GetDealerHand()
        {
            return m_dealer.GetHand();
        }

        public IEnumerable<Card> GetPlayerHand()
        {
            return m_player.GetHand();
        }

        public int GetDealerScore()
        {
            return m_dealer.CalcScore();
        }

        public int GetPlayerScore()
        {
            return m_player.CalcScore();
        }
    }
}
