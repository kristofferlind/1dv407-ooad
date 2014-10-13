using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _1DV407Labb2.Model;

namespace _1DV407Labb2.View
{
    class MemberView : BaseView
    {
        private MemberRegister memberRegister;

        public MemberView(MemberRegister memberRegister)
        {
            this.memberRegister = memberRegister;
        }

        private void DisplayMemberListHeader()
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ █▓▒░            BOAT MEMBERSHIP - MEMBER LIST             ░▒▓█ ║");
            Console.WriteLine("╠═════╦═══════════╦═══════════════════════╦═════════════╦════════╣");
            Console.WriteLine("║ Row ║ Member ID ║          Name         ║     SSN     ║ Boats  ║");
            Console.WriteLine("╠═════╬═══════════╬═══════════════════════╬═════════════╬════════╣");
        }

        private void DisplayMemberListLine(int rowNumber, int id, string name, string ssn, int count)
        {
            Console.WriteLine("║  {0,2} ║   {1,5}   ║ {2,-21} ║ {3,11} ║ {4,5}  ║", rowNumber, id, name, ssn, count);
        }

        public void DisplayNoBoats()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Couldn't find any boats for member.");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
        }

        private void DisplayBoatList(ReadOnlyCollection<Boat> boats)
        {
            for (int i = 0; i < boats.Count; i++)
            {
                var midValue = ((decimal)boats.Count - 1) / 2;
                if (i == (int)midValue)
                {
                    Console.Write("║  Boats   ");
                }
                else
                {
                    Console.Write("║          ");
                }
                Console.WriteLine("  Boat type: {0,15}     Length: {1,5} meter  ║", boats[i].BoatType.ToString(), boats[i].Length);
            }
        }
        private void DisplayMemberListFooter()
        {
            Console.WriteLine("║ 0: Return to main menu                                         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
        }

        public void DisplayMemberList(bool showBoats)
        {
            Console.Clear();
            var members = memberRegister.GetMemberList();

            DisplayMemberListHeader();

            for (int i = 0; i < members.Count; i++)
            {
                var member = members[i];
                var boatManager = memberRegister.GetBoatManager(member);
                var boats = boatManager.GetBoatList();
                var boatCount = boats.Count;
                //var boats = members[i].GetBoatList();

                DisplayMemberListLine(i + 1, members[i].Id, members[i].Name, members[i].SocialSecurityNumber, boatCount);

                //only show boats if it's requested
                if (showBoats)
                {
                    Console.WriteLine("╠═════╩═══╦═══════╩═══════════════════════╩═════════════╩════════╣");
                    DisplayBoatList(boats);
                    //last boat
                    if (i == members.Count - 1)
                    {
                        Console.WriteLine("╠═════════╩══════════════════════════════════════════════════════╣");
                    }
                    //not last boat
                    else
                    {
                        Console.WriteLine("╠═════╦═══╩═══════╦═══════════════════════╦═════════════╦════════╣");
                    }
                }
                else
                {
                    if (i == members.Count - 1)
                    {
                        Console.WriteLine("╠═════╩═══════════╩═══════════════════════╩═════════════╩════════╣");
                    }
                    else
                    {
                        Console.WriteLine("╠═════╬═══════════╬═══════════════════════╬═════════════╬════════╣");
                    }
                }

            }
            DisplayMemberListFooter();
        }

        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ █▓▒░             BOAT MEMBERSHIP - MAIN MENU              ░▒▓█ ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ 1: Show compact list                                           ║");
            Console.WriteLine("║ 2: Show detailed list                                          ║");
            if (memberRegister.IsLoggedIn)
            {
                Console.WriteLine("║ 3: Add member                                                  ║");
                Console.WriteLine("║ 4: Save                                                        ║");
            }
            else
            {
                Console.WriteLine("║ 3: Login                                                       ║");                
            }
            Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ 0: Exit program                                                ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
        }

        public void DisplayMember(Member member, ReadOnlyCollection<Boat> boats)
        {
            //var boats = member.GetBoatList();
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ █▓▒░             BOAT MEMBERSHIP - MEMBER                 ░▒▓█ ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ Name:       {0,15}                                    ║", member.Name);
            Console.WriteLine("║ Member id:  {0,15}                                    ║", member.Id);
            Console.WriteLine("║ Member ssn: {0,15}                                    ║", member.SocialSecurityNumber);

            if (boats.Count > 0)
            {
                Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");
                DisplayBoatList(boats);
            }
            Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");
            if (memberRegister.IsLoggedIn)
            {
                Console.WriteLine("║ 1: Add boat                                                    ║");
                Console.WriteLine("║ 2: Manage boats                                                ║");
                Console.WriteLine("║ 3: Edit member                                                 ║");
                Console.WriteLine("║ 4: Remove member                                               ║");
                Console.WriteLine("╠════════════════════════════════════════════════════════════════╣");
            }
            Console.WriteLine("║ 0: Back to main menu                                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
        }
    }
}
