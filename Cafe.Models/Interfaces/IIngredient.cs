using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Models
{
    // Interface not really needed; just practicing here.
    public interface IIngredient
    {
        DateTime Purchased { get; set; }
        TimeSpan ShelfLife { get; set; }
        DateTime Expires { get; }
        decimal AmountPurchased { get; set; }
        decimal Cost { get; set; }
        decimal AmountRemaining { get; }

        decimal GetCostForAmount(decimal amountNeeded);
    }
}
