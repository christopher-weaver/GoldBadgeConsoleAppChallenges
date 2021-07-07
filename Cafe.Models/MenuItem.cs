using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Models
{
    public class MenuItem
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<Ingredient, decimal> Recipe = new Dictionary<Ingredient, decimal>();
        public decimal Markup { get; set; }
        public decimal Price
        {
            get
            {
                decimal price = 0;
                foreach (var recipeItem in Recipe)
                {
                    price += Math.Round(recipeItem.Key.GetCostForAmount(recipeItem.Value) * Markup, 2);
                }
                return price;
            }
        }

        public MenuItem()
        {

        }

        public MenuItem(int number, string name, string description, Dictionary<Ingredient, decimal> recipe, decimal markup)
        {
            this.Number = number;
            this.Name = name;
            this.Description = description;
            this.Recipe = recipe;
            this.Markup = markup;
        }
    }
}
