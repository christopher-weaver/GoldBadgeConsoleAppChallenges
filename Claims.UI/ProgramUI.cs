using Claims.Models;
using Claims.Models.Enumerations;
using Claims.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claims.UI
{
    public class ProgramUI
    {
        private ClaimRepository _claimQueue = new ClaimRepository();

        public void Run()
        {
            SeedRepo();

            bool keepRunning = true;
            while (keepRunning)
            {
                keepRunning = DisplayMainMenu();
            }
        }

        private bool DisplayMainMenu()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Komodo Insurance Claims Tool v1.00\n");
            Console.ResetColor();
            Console.Write("Main Menu:\n" +
                          "1. See all claims\n" +
                          "2. Take care of next claim\n" +
                          "3. Enter a new claim\n" +
                          "4. Exit\n" +
                          "Please select a menu option: ");
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
                    DisplayAllClaims();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    DisplayTakeCareOfNext();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    DisplayGetClaimInfo();
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    return false;
                default:
                    break;
            }
            return true;
        }

        private void DisplayAllClaims()
        {
            Console.Clear();
            DisplayClaimColumnHeadings();
            foreach (Claim claim in _claimQueue.GetAllClaims())
            {
                DisplayClaimLine(claim);
            }
            DisplayAnyKeyToContinue();
        }

        private void DisplayAnyKeyToContinue()
        {
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        private void DisplayClaimColumnHeadings()
        {
            StringBuilder claimText = new StringBuilder();
            claimText.Append("ClaimID".PadRight(10));
            claimText.Append("Type".PadRight(10));
            claimText.Append("Description".PadRight(32));
            claimText.Append("Amount".PadRight(20));
            claimText.Append("DateOfIncident".PadRight(15));
            claimText.Append("DateOfClaim".PadRight(15));
            claimText.Append("IsValid".PadRight(10));
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(claimText);
            Console.ResetColor();
        }

        private void DisplayClaimLine(Claim claim)
        {
            StringBuilder claimText = new StringBuilder();
            claimText.Append(claim.LastFourOfClaimId.PadRight(10));
            claimText.Append(claim.ClaimType.ToString().PadRight(10));
            claimText.Append(claim.Description.ToString().PadRight(32));
            claimText.Append(claim.ClaimAmount.ToString("C2").PadLeft(15) + "     ");
            claimText.Append(claim.DateOfIncident.ToString("M-d-yyyy").PadRight(15));
            claimText.Append(claim.DateOfClaim.ToString("M-d-yyyy").PadRight(15));
            claimText.Append(claim.IsValid.ToString().PadRight(10));
            Console.WriteLine(claimText);
        }

        private void DisplayTakeCareOfNext()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Here are the details for the next claim to be handled:");
            Console.ResetColor();
            Claim nextClaim = _claimQueue.GetAllClaims().Peek();
            DisplayClaim(nextClaim);
            Console.Write("Do you want to deal with this claim now? [Y/N] ");
            if (GetYesNoInput())
            {
                Claim pulledClaim = _claimQueue.GetNextClaim();
                Console.WriteLine("\n\nThis claim has been pulled from the queue.");
                DisplayAnyKeyToContinue();
            }
        }

        private void DisplayClaim(Claim claim)
        {
            Console.WriteLine($"\n{"ClaimID:".PadRight(15)}{claim.LastFourOfClaimId}\n" +
                              $"{"Type:".PadRight(15)}{claim.ClaimType}\n" +
                              $"{"Description:".PadRight(15)}{claim.Description}\n" +
                              $"{"Amount:".PadRight(15)}{claim.ClaimAmount.ToString("C2")}\n" +
                              $"{"DateOfIncident:".PadRight(15)}{claim.DateOfIncident.ToString("M-d-yyyy")}\n" +
                              $"{"DateOfClaim:".PadRight(15)}{claim.DateOfClaim.ToString("M-d-yyyy")}\n" +
                              $"{"IsValid:".PadRight(15)}{claim.IsValid}\n");
        }

        private bool GetYesNoInput()
        {
            ConsoleKey input = Console.ReadKey().Key;
            return (input == ConsoleKey.Y);
        }

        private void DisplayGetClaimInfo()
        {
            DisplayNewClaimHeading();
            Claim newClaim = new Claim();
            DisplayClaimId(newClaim);
            GetClaimType(newClaim);
            GetDescription(newClaim);
            GetAmount(newClaim);
            GetDateOfIncident(newClaim);
            Console.Write($"{"Date Of Claim:".PadRight(20)}{newClaim.DateOfClaim.ToString("M-d-yyyy")}");
            Console.WriteLine($"\n{(newClaim.IsValid ? "\nThe claim is valid." : "\n=====CLAIM NOT VALID!=====")}");
            if (_claimQueue.AddClaimToQueue(newClaim))
            {
                Console.WriteLine("\nNew claim successfully added to the queue.");
            }
            else
            {
                Console.WriteLine("\nUnable to add this claim to the queue.");
            }
            DisplayAnyKeyToContinue();
        }

        private void DisplayNewClaimHeading()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("New Claim Submission\n");
            Console.ResetColor();
        }

        private void DisplayClaimId(Claim newClaim)
        {
            Console.WriteLine($"{"Full Claim ID:".PadRight(20)}{newClaim.ClaimId}");
            Console.WriteLine($"{"Short Claim ID:".PadRight(20)}{newClaim.LastFourOfClaimId}");
        }

        private void GetClaimType(Claim newClaim)
        {
            ConsoleKey response = default;
            Console.Write($"{"Type:".PadRight(20)} 1. Car\n" +
                              $"{"".PadRight(20)} 2. Home\n" +
                              $"{"".PadRight(20)} 3. Theft");
            while (response != ConsoleKey.D1 && response != ConsoleKey.NumPad1 &&
                   response != ConsoleKey.D2 && response != ConsoleKey.NumPad2 &&
                   response != ConsoleKey.D3 && response != ConsoleKey.NumPad3)
            {
                Console.Write("\nPlease provide the corresponding number: [1/2/3] ");
                response = Console.ReadKey().Key;
            }
            newClaim.ClaimType = (ClaimType)response;
        }

        private void GetDescription(Claim newClaim)
        {
            Console.Write("\nDescription: ");
            string description = "";
            while (description == "")
            {
                description = Console.ReadLine();
            }
            newClaim.Description = description;
        }

        private void GetAmount(Claim newClaim)
        {
            decimal amount = default;
            Console.Write("Amount: ");
            string response = Console.ReadLine();
            while (!Decimal.TryParse(response, out amount) || amount <= 0)
            {
                Console.Write($"Please enter a valid amount:");
                response = Console.ReadLine();
            }
            newClaim.ClaimAmount = amount;
        }

        private void GetDateOfIncident(Claim newClaim)
        {
            DateTime dateOfIncident = default;
            Console.Write($"{"Date Of Incident:".PadRight(20)}");
            string response = Console.ReadLine();
            while (!DateTime.TryParse(response, out dateOfIncident) || dateOfIncident == default || dateOfIncident > newClaim.DateOfClaim)
            {
                Console.Write($"Please enter a valid date:");
                response = Console.ReadLine();
            }
            newClaim.DateOfIncident = dateOfIncident;
        }

        private void SeedRepo()
        {
            Claim claim1 = new Claim();
            claim1.ClaimType = ClaimType.Car;
            claim1.Description = "Car was rear-ended.";
            claim1.ClaimAmount = 5000;
            claim1.DateOfIncident = new DateTime(2021, 6, 20);
            _claimQueue.AddClaimToQueue(claim1);

            Claim claim2 = new Claim();
            claim2.ClaimType = ClaimType.Home;
            claim2.Description = "Bathroom fire.";
            claim2.ClaimAmount = 50000;
            claim2.DateOfIncident = new DateTime(2021, 7, 10);
            _claimQueue.AddClaimToQueue(claim2);

            Claim claim3 = new Claim();
            claim3.ClaimType = ClaimType.Theft;
            claim3.Description = "Dog stole homework.";
            claim3.ClaimAmount = 1000;
            claim3.DateOfIncident = new DateTime(2021, 5, 10);
            _claimQueue.AddClaimToQueue(claim3);

            Claim claim4 = new Claim();
            claim4.ClaimType = ClaimType.Car;
            claim4.Description = "Car accident on 465.";
            claim4.ClaimAmount = 400;
            claim4.DateOfIncident = new DateTime(2021, 6, 25);
            _claimQueue.AddClaimToQueue(claim4);

            Claim claim5 = new Claim();
            claim5.ClaimType = ClaimType.Home;
            claim5.Description = "House fire in kitchen.";
            claim5.ClaimAmount = 4000;
            claim5.DateOfIncident = new DateTime(2021, 7, 1);
            _claimQueue.AddClaimToQueue(claim5);

            Claim claim6 = new Claim();
            claim6.ClaimType = ClaimType.Theft;
            claim6.Description = "Stolen pancakes.";
            claim6.ClaimAmount = 4;
            claim6.DateOfIncident = new DateTime(2021, 7, 4);
            _claimQueue.AddClaimToQueue(claim6);
        }
    }
}
