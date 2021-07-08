using Cafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Repos
{
    public class PantryRepository
    {
        private List<Ingredient> _pantry = new List<Ingredient>();

        // Create
        public void AddIngredientToPantry(Ingredient ingredient)
        {
            _pantry.Add(ingredient);
        }

        // Read
        public Ingredient GetIngredientFromPantry(string ingredientName)
        {
            foreach (Ingredient ingredient in _pantry)
            {
                if (ingredient.GetType().ToString().ToLower() == "cafe.models." + ingredientName.ToLower())
                {
                    return ingredient;
                }
            }
            return null;
        }

        public List<Ingredient> GetPantryList()
        {
            return _pantry;
        }

        // Update
        // Not implemented

        // Delete
        // Not implemented
    }
}
