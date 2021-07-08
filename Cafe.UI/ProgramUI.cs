using Cafe.Models;
using Cafe.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.UI
{
    public class ProgramUI
    {
        private PantryRepository _pantryRepo = new PantryRepository();
        private MenuItemRepository _menuRepo = new MenuItemRepository();

        public void Run()
        {
            SeedPantryRepository();
            SeedMenuRepository();

            bool continueGame = true;
            while (continueGame)
            {
                continueGame = ProcessMenuSelection(DisplayMainMenu());
            }
        }

        private ConsoleKey DisplayMainMenu()
        {
            Console.WriteLine("_____Komodo Cafe Console_____\n\n" +
                              "1. Create menu item\n" +
                              "2. Delete menu item\n" +
                              "3. View list of all menu items\n" +
                              "4. Exit\n\n" +
                              "What would you like to do?");
            return Console.ReadKey().Key;
        }

        private bool ProcessMenuSelection(ConsoleKey key)
        {
            bool returnContinue = true;

            Console.Clear();
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    // Create menu item
                    DisplayCreateMenuItem();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    // Delete menu item
                    DisplayDeleteMenuItem();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    // View list of all menu items
                    DisplayViewMenu();
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    // Exit
                    returnContinue = false;
                    break;
                default:
                    Console.WriteLine("Please select a valid option.\n" );
                    return returnContinue;
            }
            Console.Clear();
            return returnContinue;
        }

        private void DisplayCreateMenuItem()
        {
            List<MenuItem> currentMenu = _menuRepo.GetMenu();
            MenuItem newItem = new MenuItem();

            newItem.Number = DisplayCreateMenuItem_GetNumber(currentMenu);
            newItem.Name = DisplayCreateMenuItem_GetName(currentMenu);
            newItem.Description = DisplayCreateMenuItem_GetDescription();
            newItem.Recipe = DisplayCreateMenuItem_GetRecipe();
            newItem.Markup = DisplayCreateMenuItem_GetMarkup();

            _menuRepo.AddItemToMenu(newItem);
        }

        private int DisplayCreateMenuItem_GetNumber(List<MenuItem> currentMenu)
        {
            int suggestedNumber = 1;
            IEnumerable<int> usedNumbers = currentMenu.Select(item => item.Number).Distinct();

            // Suggests the smallest number not currently occupied by another menu item
            while (true)
            {
                if (usedNumbers.Contains(suggestedNumber))
                {
                    suggestedNumber++;
                }
                else
                {
                    break;
                }
            }
            Console.Write($"Meal number? (suggestion: {suggestedNumber}) ");
            int number = default;
            while (!Int32.TryParse(Console.ReadLine(), out number) || usedNumbers.Contains(number))
            {
                Console.WriteLine($"{(usedNumbers.Contains(number) ? "Menu number already exists" : "Invalid input")}; try again.");
            }

            return number;
        }

        private string DisplayCreateMenuItem_GetName(List<MenuItem> currentMenu)
        {
            IEnumerable<string> usedNames = currentMenu.Select(item => item.Name.ToLower()).Distinct();

            Console.Write("\nMeal name? ");
            string name = Console.ReadLine();
            while (usedNames.Contains(name.ToLower()))
            {
                Console.WriteLine("Menu item name already exists; try again.");
                name = Console.ReadLine();
            }

            return name;
        }

        private string DisplayCreateMenuItem_GetDescription()
        {
            Console.Write("\nMeal description? ");
            return Console.ReadLine();
        }

        private Dictionary<Ingredient, decimal> DisplayCreateMenuItem_GetRecipe()
        {
            Dictionary<Ingredient, decimal> recipe = new Dictionary<Ingredient, decimal>();
            decimal amount;

            List<Ingredient> listOfIngredients = DisplayIngredients();
            Console.WriteLine();
            foreach(Ingredient ingredient in listOfIngredients)
            {
                // Do not repeat ingredients
                if (!recipe.ContainsKey(ingredient))
                {
                    // Print ingredient without 'Cafe.Models'
                    Console.Write($"{ingredient.GetType().ToString().Substring(12)} - How many ounces of this ingredient do you need for the recipe? ");
                    while (!Decimal.TryParse(Console.ReadLine(), out amount) || amount == 0)
                    {
                        Console.WriteLine("Invalid input; try again.");
                    }
                    recipe.Add(ingredient, amount);
                }
            }
            return recipe;
        }

        private List<Ingredient> DisplayIngredients()
        {
            int index = 0;
            List<Ingredient> listOfIngredients = new List<Ingredient>();

            Console.WriteLine("\nWhat ingredients are needed for this menu item? (separated by a comma, e.g. \"1,3,5\")");
            foreach (Ingredient ingredient in _pantryRepo.GetPantryList())
            {
                listOfIngredients.Add(ingredient);
                // Print ingredient without 'Cafe.Models'
                Console.WriteLine($"{index}. {ingredient.GetType().ToString().Substring(12)}");
                index++;
            }
            return GetIngredientList(listOfIngredients);
        }

        private List<Ingredient> GetIngredientList(List<Ingredient> listOfIngredients)
        {
            List<Ingredient> selectedIngredients = new List<Ingredient>();

            string selection = Console.ReadLine();
            // Get input and check if string is numeric (with commas removed) and valid (not less than 0)
            while (!Int32.TryParse(selection.Replace(",", ""), out int i) || i < 0)
            {
                Console.WriteLine("Invalid input; try again.");
                selection = Console.ReadLine();
            }
            string[] numbersSelected = selection.Split(',');
            foreach (string strNumber in numbersSelected)
            {
                // Add ingredient to list if the index is not greater than the list count
                int number = Int32.Parse(strNumber);
                if (number < listOfIngredients.Count)
                {
                    selectedIngredients.Add(listOfIngredients[number]);
                }
            }
            return selectedIngredients;
        }

        private decimal DisplayCreateMenuItem_GetMarkup()
        {
            Console.WriteLine("\nHow much above cost should the meal be priced?\n" +
                              "Example: input of 1.5 would price the meal 50% above cost.");
            decimal markup = default;
            while (!Decimal.TryParse(Console.ReadLine(), out markup) || markup < 1)
            {
                if (markup == 0)
                {
                    Console.WriteLine("Invalid input; try again.");
                }
                else
                {
                    Console.WriteLine("Menu items cannot be priced below cost; try again.");
                }
            }

            return markup;
        }

        private void DisplayDeleteMenuItem()
        {
            int number = default;
            if (MenuIsEmpty())
            {
                return;
            }
            Console.WriteLine("Enter the number of the menu item you would like to delete:");
            foreach (MenuItem item in _menuRepo.GetMenu())
            {
                Console.WriteLine($"{item.Number}. {item.Name}");
            }
            while (!Int32.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Invalid input; try again.");
            }
            if (_menuRepo.RemoveItem(number))
            {
                Console.WriteLine("Menu item successfully deleted.\n" +
                                  "Press any key to continue.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Invalid input; no menu items were deleted.\n" +
                                  "Press any key to continue.");
                Console.ReadKey();
            }
        }

        private bool MenuIsEmpty()
        {
            if (_menuRepo.GetMenu().Count < 1)
            {
                Console.WriteLine("The menu does not currently contain any menu items.\n" +
                                  "Press any key to continue.");
                Console.ReadKey();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DisplayViewMenu()
        {
            if (MenuIsEmpty())
            {
                return;
            }
            foreach (MenuItem item in _menuRepo.GetMenu())
            {
                Console.WriteLine($"--------------------------------------------------------------------------------\n" +
                                  $"Meal name: {item.Name}\n" +
                                  $"Meal number: {item.Number}\n" +
                                  $"Description: {item.Description}\n" +
                                  $"Price: ${item.Price}\n" +
                                  $"Recipe:");
                foreach (var ingredient in item.Recipe)
                {
                    Console.WriteLine($"   {ingredient.Value} oz. of {ingredient.Key.GetType().ToString().Substring(12)}");
                }
            }
            Console.WriteLine($"--------------------------------------------------------------------------------");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private void SeedMenuRepository()
        {
            MenuItem item1 = new MenuItem();
            item1.Number = 1;
            item1.Name = "Classic spaghetti and meatballs";
            item1.Description = "One serving of spaghetti with marinara and 3 meatballs";
            item1.Recipe = new Dictionary<Ingredient, decimal>
            {
                { _pantryRepo.GetIngredientFromPantry("spaghetti"), 2m },
                { _pantryRepo.GetIngredientFromPantry("marinara"), 3m },
                { _pantryRepo.GetIngredientFromPantry("meatball"), 3m }
            };
            item1.Markup = 1.563m;

            MenuItem item2 = new MenuItem();
            item2.Number = 2;
            item2.Name = "Classic fettuccine alfredo";
            item2.Description = "One serving of fettuccine with alfredo sauce";
            item2.Recipe = new Dictionary<Ingredient, decimal>
            {
                { _pantryRepo.GetIngredientFromPantry("fettuccine"), 2m },
                { _pantryRepo.GetIngredientFromPantry("alfredo"), 3m },
            };
            item2.Markup = 1.5m;

            MenuItem item3 = new MenuItem();
            item3.Number = 3;
            item3.Name = "Spaghetti with marinara";
            item3.Description = "One serving of spaghetti with marinara";
            item3.Recipe = new Dictionary<Ingredient, decimal>
            {
                { _pantryRepo.GetIngredientFromPantry("spaghetti"), 2m },
                { _pantryRepo.GetIngredientFromPantry("marinara"), 3m },
            };
            item3.Markup = 1.65m;

            _menuRepo.AddItemToMenu(item1);
            _menuRepo.AddItemToMenu(item2);
            _menuRepo.AddItemToMenu(item3);
        }

        private void SeedPantryRepository()
        {
            Marinara marinara = new Marinara(80m, 50m);
            Alfredo alfredo = new Alfredo(120m, 55m);
            Spaghetti spaghetti = new Spaghetti(40m, 350m);
            Fettuccine fettuccine = new Fettuccine(50m, 350m);
            Meatball meatball = new Meatball(70m, 200m);

            _pantryRepo.AddIngredientToPantry(marinara);
            _pantryRepo.AddIngredientToPantry(alfredo);
            _pantryRepo.AddIngredientToPantry(spaghetti);
            _pantryRepo.AddIngredientToPantry(fettuccine);
            _pantryRepo.AddIngredientToPantry(meatball);
        }
    }
}
