using CompanyOutings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyOutings.Repo
{
    public class OutingRepository
    {
        private List<Outing> _outingList = new List<Outing>();

        // Create
        public bool AddOutingToList(Outing outing)
        {
            if (outing == null)
            {
                return false;
            }

            int initialCount = _outingList.Count;
            _outingList.Add(outing);
            return _outingList.Count > initialCount;
        }

        // Read
        public List<Outing> GetAllOutings() => _outingList;

        // Update
        public bool UpdateOuting(Guid idOfOutingToReplace, Outing updatedOuting)
        {
            foreach (Outing outing in _outingList)
            {
                if (outing.OutingId == idOfOutingToReplace)
                {
                    outing.EventType = updatedOuting.EventType;
                    outing.NumberOfAttendees = updatedOuting.NumberOfAttendees;
                    outing.Date = updatedOuting.Date;
                    outing.EventCostFlat = updatedOuting.EventCostFlat;
                    outing.EventCostPerPerson = updatedOuting.EventCostPerPerson;
                    return true;
                }
            }
            return false;
        }

        // Delete
        public bool DeleteOutingById(Guid outingId)
        {
            int initialCount = _outingList.Count;
            List<Outing> tempList = new List<Outing>(_outingList.Where(outing => outing.OutingId != outingId));
            _outingList = tempList;
            return _outingList.Count < initialCount;
        }
    }
}
