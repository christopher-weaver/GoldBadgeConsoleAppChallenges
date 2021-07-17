using CompanyOutings.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyOutings.Models
{
    public class Outing
    {
        public Guid OutingId { get; }
        public EventType EventType { get; set; }
        public int NumberOfAttendees { get; set; }
        public DateTime Date { get; set; }
        public decimal EventCostFlat { get; set; }
        public decimal EventCostPerPerson { get; set; }
        public decimal TotalEventCost 
        {
            get
            {
                return EventCostFlat + EventCostPerPerson * NumberOfAttendees;
            }
        }

        public Outing()
        {
            OutingId = Guid.NewGuid();
        }
   
        public Outing(EventType eventType, int numberOfAttendees, DateTime date, decimal eventCostFlat, decimal eventCostPerPerson)
        {
            OutingId = Guid.NewGuid();
            this.EventType = eventType;
            this.NumberOfAttendees = numberOfAttendees;
            this.Date = date;
            this.EventCostFlat = eventCostFlat;
            this.EventCostPerPerson = eventCostPerPerson;
        }
    }
}
