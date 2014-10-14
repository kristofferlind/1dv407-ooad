using _1DV407Labb2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV407Labb2.View
{
    class BoatView : BaseView
    {
        public BoatView()
        {

        }

        public void DisplayBoatTypeMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ █▓▒░            BOAT MEMBERSHIP - BOAT TYPES              ░▒▓█ ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");

            var boatTypes = Enum.GetNames(typeof(BoatType));
            for (int i = 0; i < boatTypes.Length; i++)
            {
                Console.WriteLine("║ {0}: {1,15}                                             ║", i + 1, boatTypes[i]);
            }

            Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ 0: Return to previous menu                                     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

        }

        public void DisplayBoatMenu(Member member, Boat boat)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ █▓▒░              BOAT MEMBERSHIP - BOAT                  ░▒▓█ ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ Boat type:       {0,15}                               ║", boat.BoatType.ToString());
            Console.WriteLine("║ Boat length:     {0,15} meters                        ║", boat.Length);
            Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ 1: Edit boat                                                   ║");
            Console.WriteLine("║ 2: Remove boat                                                 ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ 0: Back to member menu                                         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

        }

        public void DisplayBoatsMenu(ReadOnlyCollection<Boat> boats)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ █▓▒░              BOAT MEMBERSHIP - BOATS                 ░▒▓█ ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");
            for (int i = 0; i < boats.Count; i++)
            {
                Console.WriteLine("║ {0,2}:   Boat type: {1,15}      Boat length: {2,3} meters  ║", i + 1, boats[i].BoatType, boats[i].Length);
            }
            Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ 0: Back to member menu                                         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
        }
    }
}
