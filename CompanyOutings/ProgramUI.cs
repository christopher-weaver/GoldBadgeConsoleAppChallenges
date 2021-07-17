using CompanyOutings.Models;
using CompanyOutings.Models.Enumerations;
using CompanyOutings.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyOutings
{
    public class ProgramUI
    {
        private OutingRepository _outingRepo = new OutingRepository();

        public void Run()
        {
            SeedRepo();
            List<Outing> listOfOutings = _outingRepo.GetAllOutings();
            int index = 0;
            bool continueLoop = true;

            while (continueLoop)
            {
                Console.Clear();
                decimal totalCostAllEvents = 0;
                Console.WriteLine("Komodo Insurance Outings Report v1.00\n\n" +
                                  "Select a menu item or event to edit:");
                DisplayOutingColumnHeadings();
                for (int i = 0; i < listOfOutings.Count + 3; i++)
                {
                    if (i < listOfOutings.Count)
                    {
                        DisplayOutingLine(listOfOutings[i], i == index);
                        totalCostAllEvents += listOfOutings[i].TotalEventCost;
                    }
                    else if (i == listOfOutings.Count)
                    {
                        DisplayOtherLine("TOTAL FOR ALL EVENTS:", totalCostAllEvents.ToString("C2"),  i == index);
                    }
                    else if (i == listOfOutings.Count + 1)
                    {
                        DisplayOtherLine("=[ADD NEW OUTING]=", "", i == index);
                    }
                    else if (i == listOfOutings.Count + 2)
                    {
                        DisplayOtherLine("=[DISPLAY TOTAL COST FOR EACH CATEGORY]=", "", i == index);
                    }
                }
                ConsoleKey input = Console.ReadKey().Key;
                switch (input)
                {
                    case ConsoleKey.UpArrow:
                        index--;
                        break;
                    case ConsoleKey.DownArrow:
                        index++;
                        break;
                    case ConsoleKey.Enter:
                        if (index < listOfOutings.Count)
                        {
                            DisplayEditMenu(listOfOutings[index]);
                        }
                        else if (index == listOfOutings.Count + 1)
                        {
                            DisplayAddMenu();
                        }
                        else if (index == listOfOutings.Count + 2)
                        {
                            DisplayTotals(listOfOutings);
                        }
                        break;
                    case ConsoleKey.Escape:
                        continueLoop = false;
                        break;
                    default:
                        break;
                }
                if (index > listOfOutings.Count + 2)
                {
                    index = listOfOutings.Count + 2;
                }
                else if (index < 0)
                {
                    index = 0;
                }
            }
        }

        private void DisplayAddMenu()
        {
            DisplayEditMenu(null);
        }

        private void DisplayEditMenu(Outing existingOuting)
        {
            Console.Clear();
            Outing newOuting = new Outing();
            newOuting.Date = GetDate();
            newOuting.EventType = GetEventType();
            newOuting.NumberOfAttendees = GetNumber("Number of Attendees: ");
            newOuting.EventCostPerPerson = GetCost("Price per person: ");
            newOuting.EventCostFlat = GetCost("Additional cost (flat): ");
            if (existingOuting == null)
            {
                _outingRepo.AddOutingToList(newOuting);
            }
            else
            {
                _outingRepo.UpdateOuting(existingOuting.OutingId, newOuting);
            }
        }

        private void DisplayTotals(List<Outing> listOfOutings)
        {
            Console.Clear();
            foreach (EventType eventType in Enum.GetValues(typeof(EventType)))
            {
                decimal combinedEventCost = 0;
                foreach (Outing outing in listOfOutings)
                {
                    if (outing.EventType == eventType)
                    {
                        combinedEventCost += outing.TotalEventCost;
                    }
                }
                Console.WriteLine($"All {eventType.ToString()} outings for the year cost {combinedEventCost.ToString("C2")}.");
            }
            DisplayAnyKeyToContinue();
        }

        private void DisplayAnyKeyToContinue()
        {
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        private EventType GetEventType()
        {
            ConsoleKey response = default;
            Console.Write($"{"Event Type:".PadRight(24)} 1. Golf\n" +
                              $"{"".PadRight(24)} 2. Bowling\n" +
                              $"{"".PadRight(24)} 3. Amusement Park\n" +
                              $"{"".PadRight(24)} 4. Concert");
            while (response != ConsoleKey.D1 && response != ConsoleKey.NumPad1 &&
                   response != ConsoleKey.D2 && response != ConsoleKey.NumPad2 &&
                   response != ConsoleKey.D3 && response != ConsoleKey.NumPad3 &&
                   response != ConsoleKey.D4 && response != ConsoleKey.NumPad4)
            {
                Console.Write("\nPlease provide the corresponding number: [1-4] ");
                response = Console.ReadKey().Key;
            }
            switch (response)
            {
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    return EventType.Bowling;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    return EventType.AmusementPark;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    return EventType.Concert;
                default:
                    return EventType.Golf;
            }
        }

        private DateTime GetDate()
        {
            DateTime date = default;
            Console.Write($"{"Date Of Event:".PadRight(25)}");
            string response = Console.ReadLine();
            while (!DateTime.TryParse(response, out date) || date == default)
            {
                Console.Write($"Please enter a valid date:");
                response = Console.ReadLine();
            }
            return date;
        }

        private int GetNumber(string prompt)
        {
            int amount = default;
            Console.Write($"\n{prompt.PadRight(25)}");
            string response = Console.ReadLine();
            while (!int.TryParse(response, out amount) || amount <= 0)
            {
                Console.Write($"Please enter a number:");
                response = Console.ReadLine();
            }
            return amount;
        }

        private decimal GetCost(string prompt)
        {
            decimal amount = default;
            Console.Write($"{prompt.PadRight(25)}");
            string response = Console.ReadLine();
            while (!Decimal.TryParse(response, out amount) || amount < 0)
            {
                Console.Write($"Please enter a valid amount:");
                response = Console.ReadLine();
            }
            return amount;
        }

        private void DisplayOutingColumnHeadings()
        {
            StringBuilder outingText = new StringBuilder();
            outingText.Append("Date".PadRight(15));
            outingText.Append("EventType".PadRight(15));
            outingText.Append("#OfAttendees".PadRight(15));
            outingText.Append("$PerPerson".PadRight(15));
            outingText.Append("$FlatCost".PadRight(15));
            outingText.Append("$TotalCost".PadRight(15));
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(outingText);
            Console.ResetColor();
        }

        private void DisplayOutingLine(Outing outing, bool highlight)
        {
            StringBuilder outingText = new StringBuilder();
            outingText.Append(outing.Date.ToString("M-d-yyyy").PadRight(15));
            outingText.Append(outing.EventType.ToString().PadRight(15));
            outingText.Append(outing.NumberOfAttendees.ToString().PadRight(15));
            outingText.Append(outing.EventCostPerPerson.ToString("C2").PadLeft(15));
            outingText.Append(outing.EventCostFlat.ToString("C2").PadLeft(15));
            outingText.Append(outing.TotalEventCost.ToString("C2").PadLeft(15));
            if (highlight)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.WriteLine(outingText);
            Console.ResetColor();
        }

        private void DisplayOtherLine(string textLeft, string textRight, bool highlight)
        {
            if (highlight)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.WriteLine(textLeft.PadRight(60) + textRight.PadLeft(30));
            Console.ResetColor();
        }

        private void SeedRepo()
        {
            Outing outing1 = new Outing();
            outing1.EventType = EventType.Golf;
            outing1.NumberOfAttendees = 23;
            outing1.EventCostFlat = 2000;
            outing1.EventCostPerPerson = 25;
            outing1.Date = new DateTime(2021, 6, 20);
            _outingRepo.AddOutingToList(outing1);

            Outing outing2 = new Outing();
            outing2.EventType = EventType.Bowling;
            outing2.NumberOfAttendees = 15;
            outing2.EventCostPerPerson = 13.50m;
            outing2.Date = new DateTime(2021, 7, 10);
            _outingRepo.AddOutingToList(outing2);

            Outing outing3 = new Outing();
            outing3.EventType = EventType.AmusementPark;
            outing3.NumberOfAttendees = 42;
            outing3.EventCostPerPerson = 45.95m;
            outing3.Date = new DateTime(2021, 5, 10);
            _outingRepo.AddOutingToList(outing3);

            Outing outing4 = new Outing();
            outing4.EventType = EventType.Concert;
            outing4.NumberOfAttendees = 324;
            outing4.EventCostFlat = 5000;
            outing4.Date = new DateTime(2021, 5, 10);
            _outingRepo.AddOutingToList(outing4);
        }
    }
}
