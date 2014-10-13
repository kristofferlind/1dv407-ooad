using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV407Labb2.View
{
    class BaseView
    {
        public BaseView()
        {
            Initialize();
        }
        protected void Initialize()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
        }

        public int GetIntegerInput(int max, string text, int min = 0)
        {
            //outs 0 if unsuccessful parse, so need to check parseSuccess too since 0 might be a valid input.
            int input;
            bool parseSuccess = false;
            text = text + ": ";
            do
            {
                int padding = Console.BufferWidth - text.Length;
                Console.Write("{0}", text.PadRight(padding));
                Console.SetCursorPosition(text.Length, Console.CursorTop);
                string inputStr = Console.ReadLine().Trim();
                Console.CursorTop--;
                parseSuccess = int.TryParse(inputStr, out input);
            } while (!parseSuccess || (input < min || input > max));

            return input;
        }

        public double GetDoubleInput(double max, string text, double min = 0.0)
        {
            //outs 0 if unsuccessful parse, so need to check parseSuccess too since 0 might be a valid input.
            double input;
            bool parseSuccess = false;
            text = text + ": ";
            do
            {
                int padding = Console.BufferWidth - text.Length;
                Console.Write("{0}", text.PadRight(padding));
                Console.SetCursorPosition(text.Length, Console.CursorTop);
                string inputStr = Console.ReadLine().Trim();
                Console.CursorTop--;
                parseSuccess = double.TryParse(inputStr, out input);
            } while (!parseSuccess || (input < min || input > max));
            return input;
        }

        public string GetStringInput(int maxLength, string text, int minLength = 0)
        {
            string input;
            text = text + ": ";
            do
            {
                int padding = Console.BufferWidth - text.Length;
                Console.Write("{0}", text.PadRight(padding));
                Console.SetCursorPosition(text.Length, Console.CursorTop);
                input = Console.ReadLine().Trim();
                Console.CursorTop--;
            } while (input.Length < minLength || input.Length > maxLength);
            return input;
        }

        public bool GetConfirm(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("(Y)es or (N)o?");
            var inputChoice = Console.ReadLine();
            if (inputChoice == "Y" || inputChoice == "y" || inputChoice == "Yes" || inputChoice == "yes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
