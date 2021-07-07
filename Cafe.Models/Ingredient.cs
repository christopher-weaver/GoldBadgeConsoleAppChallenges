using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Models
{
    public class Ingredient : IIngredient
    {
        public DateTime Purchased { get; set; }
        public TimeSpan ShelfLife { get; set; }
        public DateTime Expires { get; }
        public decimal AmountPurchased { get; set; }
        public decimal Cost { get; set; }
        public decimal AmountRemaining { get; }

        public Ingredient(decimal cost, decimal amountPurchased)
        {
            this.Cost = cost;
            this.AmountPurchased = amountPurchased;
        }

        public decimal GetCostForAmount(decimal amountNeeded)
        {
            return Cost / AmountPurchased * amountNeeded;
        }
    }
}
