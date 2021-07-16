using Badges.Models;
using Badges.Repos;
using Badges.Models.Enumerations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Badges.Repos.Contracts;

namespace Badges.UnitTests
{
    [TestClass]
    public class TestBadgeDictRepository
    {
        private readonly IBadgeRepository _badgeRepo = new BadgeDictRepository();
        private readonly Badge _badge1 = new Badge();
        private readonly Badge _badge2 = new Badge();
        private readonly Badge _badge3 = new Badge();

        [TestInitialize]
        public void Initialize()
        {
            _badge1.BadgeId = 12345;
            _badge1.Name = "Christopher";
            _badge1.Doors = new List<Door>() {Door.A7};

            _badge2.BadgeId = 22345;
            _badge2.Name = "Phil";
            _badge2.Doors = new List<Door>() { Door.A1, Door.A4, Door.B1, Door.B2 };

            _badge3.BadgeId = 32345;
            _badge3.Name = "Terry";
            _badge3.Doors = new List<Door>() { Door.A4, Door.A5 };
        }

        [TestMethod]
        public void BadgeRepository_AddBadgeToList_ShouldReturnTrue()
        {
            // Arrange
            // Necessary items arranged in TestInitialize method Initialize()

            // Act
            bool result = _badgeRepo.AddBadgeToList(_badge1);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BadgeRepository_AddBadgeToList_ShouldReturnFalse()
        {
            // Arrange
            // Necessary items arranged in TestInitialize method Initialize()

            // Act
            bool result = _badgeRepo.AddBadgeToList(null);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        // No logic in the tested method
        public void BadgeRepository_GetAllBadges_ShouldReturnCorrectCount()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _badgeRepo.AddBadgeToList(_badge1);
            _badgeRepo.AddBadgeToList(_badge2);
            _badgeRepo.AddBadgeToList(_badge3);

            // Act
            int actual = _badgeRepo.GetAllBadges().Count;
            int expected = 3;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BadgeRepository_UpdateBadgeDoors_ShouldReturnTrue()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _badgeRepo.AddBadgeToList(_badge1);
            _badgeRepo.AddBadgeToList(_badge2);
            List<Door> updatedDoors = new List<Door>() { Door.A8, Door.A9, Door.B8, Door.B9 };

            // Act
            bool result = _badgeRepo.UpdateBadgeDoors(_badge1.BadgeId, updatedDoors);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BadgeRepository_UpdateBadgeDoors_ShouldReturnFalse()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _badgeRepo.AddBadgeToList(_badge1);
            List<Door> updatedDoors = new List<Door>() { Door.A8, Door.A9, Door.B8, Door.B9 };

            // Act
            // No badge number 99999
            bool result = _badgeRepo.UpdateBadgeDoors(99999, updatedDoors);

            //Assert
            Assert.IsFalse(result);
        }

        public void BadgeRepository_UpdateBadgeDoors_ShouldReturnFalseForNullDoorList()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _badgeRepo.AddBadgeToList(_badge1);

            // Act
            // No badge number 99999
            bool result = _badgeRepo.UpdateBadgeDoors(_badge1.BadgeId, null);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BadgeRepository_DeleteDoorsFromBadge_ShouldReturnTrue()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _badgeRepo.AddBadgeToList(_badge1);
            _badgeRepo.AddBadgeToList(_badge2);

            // Act
            bool result = _badgeRepo.DeleteDoorsFromBadge(_badge2.BadgeId);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BadgeRepository_DeleteDoorsFromBadge_ShouldReturnFalse()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _badgeRepo.AddBadgeToList(_badge1);
            _badgeRepo.AddBadgeToList(_badge2);

            // Act
            // No badge number 99999
            bool result = _badgeRepo.DeleteDoorsFromBadge(99999);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
