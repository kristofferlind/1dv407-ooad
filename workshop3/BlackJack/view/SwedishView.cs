using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BlackJack.view
{
    class SwedishView : IView
    {
        public void DisplayWelcomeMessage()
        {
            System.Console.Clear();
            System.Console.WriteLine("Hej Black Jack Världen");
            System.Console.WriteLine("----------------------");
            System.Console.WriteLine("Skriv 'p' för att Spela, 'h' för nytt kort, 's' för att stanna 'q' för att avsluta\n");
        }

        public void DisplayRules(string hitRule, string newGameRule, string winRule)
        {
            Console.WriteLine("Rules in use:");
            Console.WriteLine("\t" + hitRule);
            Console.WriteLine("\t" + newGameRule);
            Console.WriteLine("\t" + winRule);
            Console.WriteLine();
        }

        public Action GetInput()
        {
            Action action;
            string c;
            bool isValid;
            do
            {
                c = Console.ReadLine();
                isValid = Regex.IsMatch(c, @"^[phsq]$");
            } while (!isValid);

            switch (c)
            {
                case "p":
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
            if (a_card.GetColor() == model.Card.Color.Hidden)
            {
                System.Console.WriteLine("Dolt Kort");
            }
            else
            {
                String[] colors = new String[(int)model.Card.Color.Count] { "Hjärter", "Spader", "Ruter", "Klöver" };
                String[] values = new String[(int)model.Card.Value.Count] { "två", "tre", "fyra", "fem", "sex", "sju", "åtta", "nio", "tio", "knekt", "dam", "kung", "ess" };
                System.Console.WriteLine("{0} {1}", colors[(int)a_card.GetColor()], values[(int)a_card.GetValue()]);
            }
        }

        public void DisplayResults(IEnumerable<model.Card> a_playerHand, int a_playerScore, IEnumerable<model.Card> a_dealerHand, int a_dealerScore)
        {
            Console.Clear();
            DisplayWelcomeMessage();
            DisplayHand("Croupier", a_dealerHand, a_dealerScore);
            DisplayHand("Spelare", a_playerHand, a_playerScore);
        }

        public void DisplayPlayerHand(IEnumerable<model.Card> a_hand, int a_score)
        {
            DisplayHand("Spelare", a_hand, a_score);
        }
        public void DisplayDealerHand(IEnumerable<model.Card> a_hand, int a_score)
        {
            DisplayHand("Croupier", a_hand, a_score);
        }
        public void DisplayGameOver(bool a_dealerIsWinner)
        {
            System.Console.Write("Slut: ");
            if (a_dealerIsWinner)
            {
                System.Console.WriteLine("Croupiern Vann!");
            }
            else
            {
                System.Console.WriteLine("Du vann!");
            }
        }

        private void DisplayHand(String a_name, IEnumerable<model.Card> a_hand, int a_score)
        {
            System.Console.WriteLine("{0} Har: ", a_name);
            foreach (model.Card c in a_hand)
            {
                DisplayCard(c);
            }
            System.Console.WriteLine("Poäng: {0}", a_score);
            System.Console.WriteLine("");
        }
    }
}
