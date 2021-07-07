using Cafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Repos
{
    public class MenuItemRepository
    {
        private List<MenuItem> _menu = new List<MenuItem>();

        // Create
        public void AddItemToMenu(MenuItem item)
        {
            _menu.Add(item);
        }

        // Read
        public List<MenuItem> GetMenu()
        {
            return _menu;
        }

        // Update
        // Not implemented per assignment description

        // Delete
        public bool RemoveItem(MenuItem item)
        {
            if (item == null)
            {
                return false;
            }

            int initialCount = _menu.Count;
            _menu.Remove(item);

            if (_menu.Count < initialCount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveItem(string name)
        {
            MenuItem item = GetItemByName(name);
            return RemoveItem(item);
        }

        public bool RemoveItem(int id)
        {
            MenuItem item = GetItemById(id);
            return RemoveItem(item);
        }

        public int RemoveItemsWithIngredient(Ingredient ingredient)
        {
            int itemsRemoved = 0;
            foreach (MenuItem item in GetItemByIngredient(ingredient))
            {
                if (RemoveItem(item))
                {
                    itemsRemoved++;
                }
            }
            return itemsRemoved;
        }

        // Helper methods
        public MenuItem GetItemById(int id)
        {
            foreach (MenuItem item in _menu)
            {
                if (item.Number == id)
                {
                    return item;
                }
            }
            return null;
        }

        public MenuItem GetItemByName(string name)
        {
            foreach (MenuItem item in _menu)
            {
                if (name.ToLower() == item.Name.ToLower())
                {
                    return item;
                }
            }
            return null;
        }

        public List<MenuItem> GetItemByIngredient(Ingredient ingredient)
        {
            List<MenuItem> itemsWithIngredient = new List<MenuItem>();
            foreach (MenuItem item in _menu)
            {
                foreach (Ingredient recipeItem in item.Recipe.Keys)
                {
                    if (recipeItem == ingredient)
                    {
                        itemsWithIngredient.Add(item);
                    }
                }
            }
            return itemsWithIngredient;
        }
    }
}
