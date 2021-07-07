using Cafe.Models;
using Cafe.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuItemRepository menuRepo = new MenuItemRepository();

            Marinara marinara = new Marinara(80m, 50m);
            Alfredo alfredo = new Alfredo(120m, 55m);
            Spaghetti spaghetti = new Spaghetti(40m, 350m);
            Fettuccine fettuccine = new Fettuccine(50m, 350m);
            Meatball meatball = new Meatball(70m, 200m);

            MenuItem item1 = new MenuItem();
            item1.Number = 1;
            item1.Name = "Classic spaghetti and meatballs";
            item1.Description = "One serving of spaghetti with marinara and 3 meatballs";
            item1.Recipe = new Dictionary<Ingredient, decimal>
            {
                { spaghetti, 2m },
                { marinara, 3m },
                { meatball, 3m }
            };
            item1.Markup = 1.563m;

            MenuItem item2 = new MenuItem();
            item2.Number = 2;
            item2.Name = "Classic fettuccine alfredo";
            item2.Description = "One serving of fettuccine with alfredo sauce";
            item2.Recipe = new Dictionary<Ingredient, decimal>
            {
                { fettuccine, 2m },
                { alfredo, 3m },
            };
            item2.Markup = 1.5m;

            MenuItem item3 = new MenuItem();
            item3.Number = 3;
            item3.Name = "Spaghetti with marinara";
            item3.Description = "One serving of spaghetti with marinara";
            item3.Recipe = new Dictionary<Ingredient, decimal>
            {
                { spaghetti, 2m },
                { marinara, 3m },
            };
            item3.Markup = 1.65m;

            Console.WriteLine(Math.Round(item3.Price, 2));
            Console.ReadKey();
        }
    }
}
