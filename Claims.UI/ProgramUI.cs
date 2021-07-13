using Claims.Models;
using Claims.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claims.UI
{
    public class ProgramUI
    {
        private ClaimRepository _claimQueue = new ClaimRepository();

        public void Run()
        {
            Claim claim1 = new Claim();
            Claim claim2 = new Claim();
            Claim claim3 = new Claim();
            Claim claim4 = new Claim();

            Console.WriteLine(_claimQueue.AddClaimToQueue(claim1));
            Console.WriteLine(_claimQueue.AddClaimToQueue(claim2));
            Console.WriteLine(_claimQueue.AddClaimToQueue(claim3));
            foreach (Claim claim in _claimQueue.GetAllClaims())
            {
                Console.WriteLine(claim.ClaimId);
            }
            Console.WriteLine(_claimQueue.UpdateClaim(claim4.ClaimId, claim4));
            foreach (Claim claim in _claimQueue.GetAllClaims())
            {
                Console.WriteLine(claim.ClaimId);
            }
            Console.ReadKey();
        }
    }
}
