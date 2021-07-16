using Badges.Models;
using Badges.Models.Enumerations;
using Badges.Repos;
using Badges.Repos.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badges.UI
{
    public class ProgramUI
    {
        private IBadgeRepository _badgeList;

        public void Run()
        {
            bool keepRunning = DisplayModularityChoice();
            while (keepRunning)
            {
                SeedRepo();
                while (keepRunning)
                {
                    keepRunning = DisplayMainMenu();
                }
                keepRunning = DisplayModularityChoice();
            }
        }

        private bool DisplayModularityChoice()
        {
            ConsoleKey selection = default;
            while (selection != ConsoleKey.D1 && selection != ConsoleKey.NumPad1
                   && selection != ConsoleKey.D2 && selection != ConsoleKey.NumPad2
                   && selection != ConsoleKey.D3 && selection != ConsoleKey.NumPad3)
            {
                Console.Clear();
                Console.Write("This console application utilizes modularity:\n\n" +
                  "1. BadgeDictRepository - Dictionary<int, string>\n" +
                  "2. BadgeObjRepository - List<Badge>\n" +
                  "3. Exit\n\n" +
                  "Please choose the Badge Repository type you would like to test: ");
                selection = Console.ReadKey().Key;
            }
            return ProcessModularitySelection(selection);
        }

        private bool ProcessModularitySelection(ConsoleKey selection)
        {
            switch (selection)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    _badgeList = new BadgeDictRepository();
                    return true;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    _badgeList = new BadgeObjRepository();
                    return true;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                default:
                    return false;
            }
        }

        private bool DisplayMainMenu()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Komodo Insurance Badge Tool v1.00\n");
            Console.ResetColor();
            Console.Write("Hello Security Admin!\n\n" +
                          "1. Add a badge.\n" +
                          "2. Edit a badge.\n" +
                          "3. List all badges.\n" +
                          "4. Exit\n\n" +
                          "What would you like to do? ");
            bool keepRunning = GetMainMenuInput();
            return keepRunning;
        }

        private bool GetMainMenuInput()
        {
            ConsoleKey input = Console.ReadKey().Key;
            switch (input)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    DisplayGetBadgeInfo();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    DisplayUpdateMenu();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    DisplayAllBadges();
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    return false;
                default:
                    break;
            }
            return true;
        }

        private void DisplayAllBadges()
        {
            Console.Clear();
            DisplayBadgeColumnHeadings();
            foreach (Badge badge in _badgeList.GetAllBadges())
            {
                DisplayBadgeLine(badge);
            }
            DisplayAnyKeyToContinue();
        }

        private void DisplayAnyKeyToContinue()
        {
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        private void DisplayBadgeColumnHeadings()
        {
            StringBuilder badgeText = new StringBuilder();
            badgeText.Append("Badge #".PadRight(10));
            badgeText.Append("Name".PadRight(25));
            badgeText.Append("Door Access                                                ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(badgeText);
            Console.ResetColor();
        }

        private void DisplayBadgeLine(Badge badge)
        {
            StringBuilder badgeText = new StringBuilder();
            badgeText.Append(badge.BadgeId.ToString().PadRight(10));
            badgeText.Append(badge.Name.ToString().PadRight(25));
            badgeText.Append(PrepareDoorString(badge.Doors));
            Console.WriteLine(badgeText);
        }

        private string PrepareDoorString(List<Door> doors)
        {
            string stringOfDoors = "";
            foreach (Door door in doors)
            {
                if (stringOfDoors != "")
                {
                    stringOfDoors += ", ";
                }
                stringOfDoors += door.ToString();
            }
            return stringOfDoors;
        }

        private string ListOfPossibleDoors()
        {
            string stringOfDoors = "";
            foreach (Door name in Enum.GetValues(typeof(Door)))
            {
                if (stringOfDoors != "")
                {
                    stringOfDoors += ", ";
                }
                stringOfDoors += name;
            }
            return stringOfDoors;
        }

        private bool GetYesNoInput()
        {
            ConsoleKey input = Console.ReadKey().Key;
            return (input == ConsoleKey.Y);
        }

        private void DisplayGetBadgeInfo()
        {
            DisplayNewBadgeHeading();
            Badge newBadge = new Badge();
            GetId(newBadge);
            GetName(newBadge);
            ModifyDoors(newBadge, true);
            if (_badgeList.AddBadgeToList(newBadge))
            {
                Console.WriteLine("\nNew badge successfully added to the list.");
            }
            else
            {
                Console.WriteLine("\nUnable to add this badge to the list.");
            }
            DisplayAnyKeyToContinue();
        }

        private void DisplayNewBadgeHeading()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("New Badge Setup\n");
            Console.ResetColor();
        }

        private void GetId(Badge newBadge)
        {
            int id = default;
            Console.Write("What is the number on the badge? ");
            while (!Int32.TryParse(Console.ReadLine(), out id) || id <= 0 || FetchBadge(id) != null)
            {
                Console.Write($"Badge ID already exists or was invaild.\n" +
                              $"Please enter a valid badge ID: ");
            }
            newBadge.BadgeId = id;
        }

        private void GetName(Badge newBadge)
        {
            Console.Write("\nWhat is the name of the badge holder? ");
            string name = "";
            while (name == "")
            {
                name = Console.ReadLine();
            }
            newBadge.Name = name;
        }

        private void AddDoor(string doorString, Badge newBadge)
        {
            foreach (Door name in Enum.GetValues(typeof(Door)))
            {
                if (doorString == name.ToString())
                {
                    List<Door> listOfDoors = newBadge.Doors;
                    listOfDoors.Add(name);
                    _badgeList.UpdateBadgeDoors(newBadge.BadgeId, listOfDoors);
                    break;
                }
            }
        }

        private void RemoveDoor(string doorString, Badge badge)
        {
            List<Door> listOfDoors = badge.Doors;
            foreach (Door door in listOfDoors)
            {
                if (door.ToString() == doorString)
                {
                    listOfDoors.Remove(door);
                    _badgeList.UpdateBadgeDoors(badge.BadgeId, listOfDoors);
                    return;
                }
            }
            Console.WriteLine("Unable to delete door.");
        }

        private bool DoorIsValid(string doorString, List<Door> doorsAlreadyAdded, bool doorExists)
        {
            // Test if door is null
            if (doorString == null)
            {
                return false;
            }
            // Test if door exists in door list for current badge
            foreach (Door existingDoor in doorsAlreadyAdded)
            {
                if (existingDoor.ToString() == doorString)
                {
                    if (!doorExists)
                    {
                        Console.WriteLine("\nThis door has already been added to the badge.");
                        return false;
                    }
                }
            }
            // Test to see if door name is valid
            foreach (Door name in Enum.GetValues(typeof(Door)))
            {
                if (doorString == name.ToString())
                {
                    return true;
                }
            }
            Console.WriteLine("\nThat is not a valid door name.");
            return false;
        }

        private void DisplayUpdateMenu()
        {
            int badgeId = default;
            Console.Clear();
            Console.Write("What is the badge number to update? ");
            while (!Int32.TryParse(Console.ReadLine(),  out badgeId) || badgeId < 0)
            {
                Console.Write($"Badge ID already exists or was invaild.\n" +
                              $"Please enter a valid badge ID: ");
            }
            Badge badgeToUpdate = FetchBadge(badgeId);
            if (badgeToUpdate == null)
            {
                Console.WriteLine("\nUnable to find the requested badge.  Returning to main menu.");
                DisplayAnyKeyToContinue();
                return;
            }
            Console.WriteLine("\nCurrent information:");
            DisplayBadgeColumnHeadings();
            DisplayBadgeLine(badgeToUpdate);
            DisplayUpdateSubmenu();
            GetSubmenuInput(badgeToUpdate);
        }

        private void DisplayUpdateSubmenu()
        {
            Console.Write("\n1. Add a door.\n" +
                          "2. Remove a door.\n" +
                          "3. Remove all doors.\n" +
                          "4. Exit\n\n" +
                          "What would you like to do? "); 
        }

        private void GetSubmenuInput(Badge badgeToUpdate)
        {
            ConsoleKey input = Console.ReadKey().Key;
            switch (input)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    ModifyDoors(badgeToUpdate, true);
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    ModifyDoors(badgeToUpdate, false);
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    DisplayRemoveAllDoors(badgeToUpdate);
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    break;
                default:
                    Console.WriteLine("\n\nInvalid menu option.  Returning to main menu.");
                    DisplayAnyKeyToContinue();
                    break;
            }
 
        }

        private void ModifyDoors(Badge badgeToUpdate, bool add)
        {
            bool continueLoop = true;
            string action = add ? "add to" : "remove from";
            if (add)
            {
                Console.WriteLine($"\nPossible doors to {action} badge: " + ListOfPossibleDoors());
            }
            while (continueLoop)
            {
                string doorString = null;
                while (!DoorIsValid(doorString, badgeToUpdate.Doors, !add))
                {
                    Console.Write($"\nEnter a door to {action} the badge: ");
                    doorString = Console.ReadLine().ToUpper();
                }
                if (add)
                {
                    AddDoor(doorString, badgeToUpdate);
                }
                else
                {
                    RemoveDoor(doorString, badgeToUpdate);
                }
                Console.Write($"{(add ? "Add" : "Remove")} another door? [Y/N] ");
                continueLoop = GetYesNoInput();
            }
        }

        private void DisplayRemoveAllDoors(Badge badgeToUpdate)
        {
            Console.Write("\nAre you sure you would like to remove all doors from this badge? [Y/N] ");
            if (GetYesNoInput())
            {
                if (_badgeList.DeleteDoorsFromBadge(badgeToUpdate.BadgeId))
                {
                    Console.WriteLine("\nAll doors have been deleted from the badge.");
                }
                else
                {
                    Console.WriteLine("\nUnable to delete all doors from the badge.");
                }
                DisplayAnyKeyToContinue();
            }
            else
            {
                Console.WriteLine("\nNo doors were removed from the badge.");
                DisplayAnyKeyToContinue();
            }
        }

        private Badge FetchBadge(int badgeId)
        {
            foreach(Badge badge in _badgeList.GetAllBadges())
            {
                if (badge.BadgeId == badgeId)
                {
                    return badge;
                }
            }
            return null;
        }

        private void SeedRepo()
        {
            Badge badge1 = new Badge();
            badge1.BadgeId = 12345;
            badge1.Name = "Christopher";
            badge1.Doors = new List<Door>() { Door.A7 };

            Badge badge2 = new Badge();
            badge2.BadgeId = 22345;
            badge2.Name = "Phil";
            badge2.Doors = new List<Door>() { Door.A1, Door.A4, Door.B1, Door.B2 };

            Badge badge3 = new Badge();
            badge3.BadgeId = 32345;
            badge3.Name = "Terry";
            badge3.Doors = new List<Door>() { Door.A4, Door.A5 };

            _badgeList.AddBadgeToList(badge1);
            _badgeList.AddBadgeToList(badge2);
            _badgeList.AddBadgeToList(badge3);
        }
    }
}
