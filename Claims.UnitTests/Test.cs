using Claims.Models;
using Claims.Models.Enumerations;
using Claims.Repos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Claims.UnitTests
{
    [TestClass]
    public class Test
    {
        private readonly ClaimRepository _claimRepo = new ClaimRepository();
        private readonly Claim _claim1 = new Claim();
        private readonly Claim _claim2 = new Claim();
        private readonly Claim _claim3 = new Claim();

        [TestInitialize]
        public void Initialize()
        {
            _claim1.ClaimType = ClaimType.Car;
            _claim1.Description = "Car was rear-ended.";
            _claim1.ClaimAmount = 5000;
            _claim1.DateOfIncident = new DateTime(2021, 6, 20);

            _claim2.ClaimType = ClaimType.Home;
            _claim2.Description = "House fire.";
            _claim2.ClaimAmount = 50000;
            _claim2.DateOfIncident = new DateTime(2021, 7, 10);

            _claim3.ClaimType = ClaimType.Theft;
            _claim3.Description = "Dog stole homework.";
            _claim3.ClaimAmount = 1000;
            _claim3.DateOfIncident = new DateTime(2021, 5, 10);
        }

        [TestMethod]
        public void ClaimRepository_AddClaimToQueue_ShouldReturnTrue()
        {
            // Arrange
            // Necessary items arranged in TestInitialize method Initialize()

            // Act
            bool result = _claimRepo.AddClaimToQueue(_claim1);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ClaimRepository_AddClaimToQueue_ShouldReturnFalse()
        {
            // Arrange
            // Necessary items arranged in TestInitialize method Initialize()

            // Act
            bool result = _claimRepo.AddClaimToQueue(null);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        // No logic in the tested method
        public void ClaimRepository_GetAllClaims_ShouldReturnCorrectCount()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _claimRepo.AddClaimToQueue(_claim1);
            _claimRepo.AddClaimToQueue(_claim2);
            _claimRepo.AddClaimToQueue(_claim3);

            // Act
            int actual = _claimRepo.GetAllClaims().Count;
            int expected = 3;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ClaimRepository_UpdateClaim_ShouldReturnTrue()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _claimRepo.AddClaimToQueue(_claim1);
            _claimRepo.AddClaimToQueue(_claim2);
            Claim updatedClaim = new Claim(ClaimType.Home, "Home was rear-ended.", 500000, new DateTime(2021, 6, 30));

            // Act
            bool result = _claimRepo.UpdateClaim(_claim1.ClaimId, updatedClaim);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ClaimRepository_UpdateClaim_ShouldReturnFalse()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _claimRepo.AddClaimToQueue(_claim1);
            _claimRepo.AddClaimToQueue(_claim2);
            Claim updatedClaim = new Claim(ClaimType.Home, "Home was rear-ended.", 500000, new DateTime(2021, 6, 30));

            // Act
            // Updated claim is new and therefore not in the claim repo.
            bool result = _claimRepo.UpdateClaim(updatedClaim.ClaimId, updatedClaim);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        // No logic in the tested method
        public void ClaimRepository_GetNextClaim_ShouldReturnCorrectClaim()
        {
            // Arrange
            // Necessary items arranged in TestInitialize method Initialize()
            _claimRepo.AddClaimToQueue(_claim1);
            _claimRepo.AddClaimToQueue(_claim2);

            // Act
            Claim nextClaim = _claimRepo.GetNextClaim();
            Guid expected = _claim1.ClaimId;
            Guid actual = nextClaim.ClaimId;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ClaimRepository_DeleteClaimById_ShouldReturnTrue()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _claimRepo.AddClaimToQueue(_claim1);
            _claimRepo.AddClaimToQueue(_claim2);

            // Act
            bool result = _claimRepo.DeleteClaimById(_claim2.ClaimId);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ClaimRepository_DeleteClaimById_ShouldReturnFalse()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _claimRepo.AddClaimToQueue(_claim1);
            _claimRepo.AddClaimToQueue(_claim2);

            // Act
            // New claim is not added to repo and should be absent.
            bool result = _claimRepo.DeleteClaimById(new Claim().ClaimId);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
