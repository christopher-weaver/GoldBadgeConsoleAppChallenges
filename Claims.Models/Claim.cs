using Claims.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claims.Models
{
    public class Claim
    {
        public Guid ClaimId { get; }
        public ClaimType ClaimType { get; set; }
        public string Description { get; set; }
        public decimal ClaimAmount { get; set; }
        public DateTime DateOfIncident { get; set; }
        public DateTime DateOfClaim { get; }
        public bool IsValid 
        { 
            get
            {
                TimeSpan timeSinceIncident;
                timeSinceIncident = DateOfClaim - DateOfIncident;
                return !(timeSinceIncident.Days > 30);                
            }
        }
        public string LastFourOfClaimId
        { 
            get
            {
                return ClaimId.ToString().Substring(ClaimId.ToString().Length - 4);
            }
        }

        public Claim()
        {
            ClaimId = Guid.NewGuid();
            this.DateOfClaim = DateTime.Now;
        }

        public Claim(ClaimType claimType, string description, decimal claimAmount, DateTime dateOfIncident)
        {
            ClaimId =  Guid.NewGuid();
            this.DateOfClaim = DateTime.Now;
            this.ClaimType = claimType;
            this.Description = description;
            this.ClaimAmount = claimAmount;
            this.DateOfIncident = dateOfIncident;
        }
    }
}
