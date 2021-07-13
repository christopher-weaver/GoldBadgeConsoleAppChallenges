using Claims.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claims.Repos
{
    public class ClaimRepository
    {
        private Queue<Claim> _claimQueue = new Queue<Claim>();

        // Create
        public bool AddClaimToQueue(Claim claim)
        {
            if (claim == null)
            {
                return false;
            }

            int initialCount = _claimQueue.Count;
            _claimQueue.Enqueue(claim);
            return _claimQueue.Count > initialCount;
        }

        // Read
        public Queue<Claim> GetAllClaims() => _claimQueue;

        // Update
        public bool UpdateClaim(Guid idOfClaimToReplace, Claim updatedClaim)
        {
            foreach(Claim claim in _claimQueue)
            {
                if (claim.ClaimId == idOfClaimToReplace)
                {
                    claim.ClaimType = updatedClaim.ClaimType;
                    claim.DateOfIncident = updatedClaim.DateOfIncident;
                    claim.Description = updatedClaim.Description;
                    claim.ClaimAmount = updatedClaim.ClaimAmount;
                    return true;
                }
            }
            return false;
        }

        // Delete
        public Claim GetNextClaim()
        {
            return _claimQueue.Dequeue();
        }

        public bool DeleteClaimById(Guid claimId)
        {
            int initialCount = _claimQueue.Count;
            Queue<Claim> tempQueue = new Queue<Claim>(_claimQueue.Where(claim => claim.ClaimId != claimId));
            _claimQueue = tempQueue;
            return _claimQueue.Count < initialCount;
        }
    }
}
