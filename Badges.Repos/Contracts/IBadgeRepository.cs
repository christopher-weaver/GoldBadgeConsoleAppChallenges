using Badges.Models;
using Badges.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badges.Repos.Contracts
{
    public interface IBadgeRepository
    {
        // Create
        bool AddBadgeToList(Badge badge);

        // Read
        List<Badge> GetAllBadges();

        // Update
        bool UpdateBadgeDoors(int idOfBadgeToUpdate, List<Door> newDoors);

        // Delete
        bool DeleteDoorsFromBadge(int idOfBadgeToUpdate);
    }
}
