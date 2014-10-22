using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BlackJack.view
{
    class SimpleView : IView
    {

        public void DisplayWelcomeMessage()
        {
            System.Console.Clear();
            System.Console.WriteLine("Hello Black Jack World");
            System.Console.WriteLine("Type 'p' to Play, 'h' to Hit, 's' to Stand or 'q' to Quit\n");
        }

        public Action GetInput()
        {
            Action action;
            string c;
            bool isValid;
            do
            {
                c = Console.ReadLine();
                isValid = Regex.IsMatch(c, @"[p|h|s|q]");
            } while (!isValid);
            switch (c)
            {
                case "P":
                    action = Action.NewGame;
                    break;
                case "h":
                    action = Action.Hit;
                    break;
                case "s":
                    action = Action.Stand;
                    break;
                case "q":
                    action = Action.Quit;
                    break;
                default:
                    throw new Exception("Input validation somehow failed.");
            }

            return action;
        }

        public void DisplayCard(model.Card a_card)
        {
            System.Console.WriteLine("{0} of {1}", a_card.GetValue(), a_card.GetColor());
        }

        public void DisplayResults(IEnumerable<model.Card> a_playerHand, int a_playerScore, IEnumerable<model.Card> a_dealerHand, int a_dealerScore)
        {
            Console.Clear();
            DisplayHand("Spelare", a_playerHand, a_playerScore);
            DisplayHand("Croupier", a_dealerHand, a_dealerScore);
        }

        public void DisplayPlayerHand(IEnumerable<model.Card> a_hand, int a_score)
        {
            DisplayHand("Player", a_hand, a_score);
        }

        public void DisplayDealerHand(IEnumerable<model.Card> a_hand, int a_score)
        {
            DisplayHand("Dealer", a_hand, a_score);
        }

        private void DisplayHand(String a_name, IEnumerable<model.Card> a_hand, int a_score)
        {
            System.Console.WriteLine("{0} Has: ", a_name);
            foreach (model.Card c in a_hand)
            {
                DisplayCard(c);
            }
            System.Console.WriteLine("Score: {0}", a_score);
            System.Console.WriteLine("");
        }

        public void DisplayGameOver(bool a_dealerIsWinner)
        {
            System.Console.Write("GameOver: ");
            if (a_dealerIsWinner)
            {
                System.Console.WriteLine("Dealer Won!");
            }
            else
            {
                System.Console.WriteLine("You Won!");
            }
            
        }
    }
}
