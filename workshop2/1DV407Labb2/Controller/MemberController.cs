using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1DV407Labb2.Model;
using _1DV407Labb2.View;

namespace _1DV407Labb2.Controller
{
    class MemberController
    {
        private MemberRegister memberRegister;
        private MemberView memberView;
        private BoatView boatView;
        private BoatManager boatManager;

        internal MemberController()
        {
            memberRegister = new MemberRegister();
            memberView = new MemberView(memberRegister);
            boatView = new BoatView();
        }

        internal void Start()
        {
            memberRegister.LoadMembers();

            int input;
            do
            {
                memberView.DisplayMenu();

                if (memberRegister.IsLoggedIn) {
                    input = memberView.GetIntegerInput(4, "Make your selection");
                }
                else
                {
                    input = memberView.GetIntegerInput(3, "Make your selection");
                }


                switch (input)
                {
                    case 1:     //display compact list
                        HandleMemberList(false);
                        break;
                    case 2:     //display verbose list
                        HandleMemberList(true);
                        break;
                    case 3:     //add member
                        if (memberRegister.IsLoggedIn)
                        {
                            HandleAddMember();
                        }
                        else
                        {
                            HandleAuthentication();
                        }
                        break;
                    case 4:
                        memberRegister.SaveMembers();
                        break;
                }
            } while (input != 0);
         }

        private void HandleAuthentication()
        {
            var username = memberView.GetStringInput(50, "Enter username");
            var password = memberView.GetStringInput(50, "Enter password");
            memberRegister.Authenticate(username, password);
        }

        private void HandleAddMember()
        {
            var inputName = memberView.GetStringInput(100, "Enter member name", 3);
            var inputSocialSecurityNumber = memberView.GetStringInput(11, "Enter social security number (YYMMDD-XXXX or XXX-XX-XXXX)", 10);
            var savedMember = memberRegister.AddMember(inputName, inputSocialSecurityNumber);
            HandleMember(savedMember);
        }

        private void HandleMemberList(bool showDetailed)
        {
            memberView.DisplayMemberList(showDetailed);
            int validMaxInput = memberRegister.GetMemberList().Count;

            var input = memberView.GetIntegerInput(validMaxInput, "Pick member");

            if (input != 0)
            {
                var memberId = memberRegister.GetMemberList()[input - 1].Id;
                var member = memberRegister.GetMemberInfo(memberId);
                boatManager = memberRegister.GetBoatManager(member);
                HandleMember(member);
            }
        }

        private void HandleMember(Member member)
        {
            int menuInput;
            do
            {
                if (memberRegister.IsMember(member.Id))
                {
                    var boats = boatManager.GetBoatList();
                    memberView.DisplayMember(member, boats);
                    if (memberRegister.IsLoggedIn)
                    {
                        menuInput = memberView.GetIntegerInput(4, "Make your selection");
                    }
                    else
                    {
                        menuInput = memberView.GetIntegerInput(0, "Make your selection");
                    }
                    switch (menuInput)
                    {
                        case 1:
                            HandleAddBoat(member);
                            break;
                        case 2:
                            HandleBoats(member);
                            break;
                        case 3:
                            HandleEditMember(member);
                            break;
                        case 4:
                            HandleRemoveMember(member);
                            break;
                    }
                }
                else
                {
                    menuInput = 0; //Leave this menu if user doesn't exist
                }
            } while (menuInput != 0);
        }

        private void HandleRemoveMember(Member member)
        {
            var confirmRemove = memberView.GetConfirm("Are you sure you want to remove this member?");
            if (confirmRemove)
            {
                memberRegister.DeleteMember(member);
            }
        }

        private void HandleEditMember(Member member)
        {
            var newName = member.Name;
            var newSocialSecurityNumber = member.SocialSecurityNumber;
            var name = memberView.GetStringInput(100, "Current name: " + member.Name + ", new name", 0);
            if (string.IsNullOrWhiteSpace(name))
            {
                newName = name;
            }
            var ssn = memberView.GetStringInput(11, "Enter social security number", 0);
            if (string.IsNullOrWhiteSpace(ssn))
            {
                newSocialSecurityNumber = ssn;
            }
            member.Update(newName, newSocialSecurityNumber);
        }

        private void HandleBoats(Member member)
        {
            var boats = boatManager.GetBoatList();
            if (boats.Count == 0)
            {
                memberView.DisplayNoBoats();
                ContinueOnKeyPressed();
                return;
            }
            if (boats.Count == 1)
            {
                HandleBoat(member, 0);
                return;
            }
            boatView.DisplayBoatsMenu(boats);
            var menuInput = memberView.GetIntegerInput(boats.Count, "Make your selection");
            if (menuInput != 0)
            {
                HandleBoat(member, menuInput - 1);
            }
        }
        private static void ContinueOnKeyPressed()
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private void HandleBoat(Member member, int boatIndex)
        {
            int menuInput;
            do
            {
                var hasBoat = boatManager.HasBoat(boatIndex);
        
                if (hasBoat)
                {
                    var boat = boatManager.GetBoat(boatIndex);
                    //TODO: skicka boat?
                    boatView.DisplayBoatMenu(member, boat);
                    menuInput = memberView.GetIntegerInput(2, "Make your selection");
                    switch (menuInput)
                    {
                        case 1:
                            HandleEditBoat(member, boat);
                            break;
                        case 2:
                            HandleRemoveBoat(member, boat);
                            break;
                    }
                }
                else
                {
                    menuInput = 0;      //In case we end up in this menu after removing a boat
                }
            } while (menuInput != 0);    
        }
        private void HandleAddBoat(Member member)
        {
            boatView.DisplayBoatTypeMenu();
            var inputValue = boatView.GetIntegerInput(5, "Pick boat type", 1);
            var boatType = (BoatType)(inputValue - 1);
            var boatLength = boatView.GetDoubleInput(100.0, "Specify boat length as meters (min 1, max 100 m)", 1.0);
            var boatIndex = boatManager.AddBoat(boatLength, boatType);
        }

        private void HandleRemoveBoat(Member member, Boat boat)
        {
            var confirmRemove = memberView.GetConfirm("Are you sure you want to remove this boat?");
            if (confirmRemove)
            {
                boatManager.RemoveBoat(boat);
            }
        }

        private void HandleEditBoat(Member member, Boat boat)
        {
            //var boat = boatManager.GetBoat(boat);
            boatView.DisplayBoatTypeMenu();
            var inputValue = memberView.GetIntegerInput(5, "Current type: " + boat.BoatType.ToString() + ", new boat type", 0);
            if (inputValue == 0)
            {
                return;
            }
            var boatType = (BoatType)(inputValue - 1);
            var boatLength = memberView.GetDoubleInput(100.0, "Current length: " + boat.Length + ", new boat length (meters)", 1.0);
            //member.UpdateBoat(boatIndex, boatLength, boatType);
            //boat.Update(boatLength, boatType);
            boatManager.Update(boat, boatLength, boatType);
        }
    }
}
