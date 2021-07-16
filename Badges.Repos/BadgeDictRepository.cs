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
    public class BadgeDictRepository : IBadgeRepository
    {
        private Dictionary<int, string> _badgeList = new Dictionary<int, string>();

        // Create
        public bool AddBadgeToList(Badge badge)
        {
            if (badge == null || _badgeList.ContainsKey(badge.BadgeId))
            {
                return false;
            }
            int initialCount = _badgeList.Count;
            _badgeList.Add(badge.BadgeId, ListOfDoorsToString(badge.Doors));
            return _badgeList.Count > initialCount;
        }

        // Read
        public List<Badge> GetAllBadges()
        {
            List<Badge> listOfBadges = new List<Badge>();
            foreach (KeyValuePair<int, string> keyValPair in _badgeList)
            {
                listOfBadges.Add(new Badge(keyValPair.Key, StringToListOfDoors(keyValPair.Value)));
            }
            return listOfBadges;
        }

        // Update
        public bool UpdateBadgeDoors(int idOfBadgeToUpdate, List<Door> newDoors)
        {
            if (newDoors == null)
            {
                return false;
            }
            if (_badgeList.ContainsKey(idOfBadgeToUpdate))
            {
                _badgeList[idOfBadgeToUpdate] = ListOfDoorsToString(newDoors);
                return true;
            }
            return false;
        }

        // Delete
        public bool DeleteDoorsFromBadge(int idOfBadgeToUpdate)
        {
            return UpdateBadgeDoors(idOfBadgeToUpdate, new List<Door>());
        }

        private string ListOfDoorsToString(List<Door> doors)
        {
            string stringOfDoors = "";
            foreach (Door door in doors)
            {
                if (stringOfDoors != "")
                {
                    stringOfDoors += ", ";
                }
                stringOfDoors += door.ToString();
            }
            return stringOfDoors;
        }

        private List<Door> StringToListOfDoors(string doors)
        {
            List<Door> listOfDoors = new List<Door>();
            doors = doors.Replace(" ", "");
            string[] doorArr = doors.Split(',');
            foreach (string doorString in doorArr)
            {
                Door? door = ValidateDoorString(doorString);
                if (door != null)
                {
                    listOfDoors.Add((Door)door);
                }
            }
            return listOfDoors;
        }

        private Door? ValidateDoorString(string door)
        {
            Door? doorObj = null;
            foreach (Door name in Enum.GetValues(typeof(Door)))
            {
                if (door == name.ToString())
                {
                    doorObj = name;
                }
            }
            return doorObj;
        }
    }
}
