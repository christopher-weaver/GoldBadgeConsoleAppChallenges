using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Models
{
    public class Spaghetti : Ingredient
    {
        public Spaghetti(decimal cost, decimal amountPurchased) : base(cost, amountPurchased)
        {
            this.Cost = cost;
            this.AmountPurchased = amountPurchased;
        }
    }
}
