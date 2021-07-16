using Badges.Models;
using Badges.Models.Enumerations;
using Badges.Repos.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badges.Repos
{
    public class BadgeObjRepository : IBadgeRepository
    {
        private List<Badge> _badgeList = new List<Badge>();

        // Create
        public bool AddBadgeToList(Badge badge)
        {
            if (badge == null)
            {
                return false;
            }

            int initialCount = _badgeList.Count;
            _badgeList.Add(badge);
            return _badgeList.Count > initialCount;
        }

        // Read
        public List<Badge> GetAllBadges() => _badgeList;

        // Update
         public bool UpdateBadgeDoors(int idOfBadgeToUpdate, List<Door> newDoors)
        {
            if (newDoors == null)
            {
                return false;
            }

            foreach (Badge badge in _badgeList)
            {
                if (badge.BadgeId == idOfBadgeToUpdate)
                {
                    badge.Doors = newDoors;
                    return true;
                }
            }
            return false;
        }

        // Delete
        public bool DeleteDoorsFromBadge(int idOfBadgeToUpdate)
        {
            return UpdateBadgeDoors(idOfBadgeToUpdate, new List<Door>());
        }
    }
}
