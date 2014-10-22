using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    class Player
    {
        private List<CardDealtListener> m_subscribers = new List<CardDealtListener>();

        private List<Card> m_hand = new List<Card>();
        //private Hand m_hand = new Hand();

        public void Subscribe(CardDealtListener a_subscriber)
        {
            m_subscribers.Add(a_subscriber);
        }


        public void DealCard(Card a_card)
        {
            foreach (var subscriber in m_subscribers)
            {
                subscriber.CardDealt(a_card, this);
            }
            //m_hand.add()?
            m_hand.Add(a_card);
        }

        public IEnumerable<Card> GetHand()
        {
            //return Hand m_hand
            return m_hand.Cast<Card>();
        }

        public void ClearHand()
        {
            m_hand.Clear();
        }

        public void ShowHand()
        {
            //hand.show?
            foreach (Card c in GetHand())
            {
                c.Show(true);
            }
        }

        public int CalcScore()
        {
            //hand.calculate?
            int[] cardScores = new int[(int)model.Card.Value.Count]
                {2, 3, 4, 5, 6, 7, 8, 9, 10, 10 ,10 ,10, 11};
            int score = 0;

            foreach(Card c in GetHand()) {
                if (c.GetValue() != Card.Value.Hidden)
                {
                    score += cardScores[(int)c.GetValue()];
                }
            }

            if (score > 21)
            {
                foreach (Card c in GetHand())
                {
                    if (c.GetValue() == Card.Value.Ace && score > 21)
                    {
                        score -= 10;
                    }
                }
            }

            return score;
        }

    }
}
