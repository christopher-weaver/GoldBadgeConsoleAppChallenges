using Badges.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badges.Models
{
    public class Badge
    {
        public int BadgeId { get; set; }
        public List<Door> Doors { get; set; } = new List<Door>();
        public string Name { get; set; }

        public Badge()
        {

        }

        public Badge(int badgeId, List<Door> doors, string name)
        {
            this.BadgeId = badgeId;
            this.Doors = doors;
            this.Name = name;
        }

    }
}
