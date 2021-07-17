using CompanyOutings.Models;
using CompanyOutings.Models.Enumerations;
using CompanyOutings.Repo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CompanyOutings.Tests
{
    [TestClass]
    public class Test
    {
        private readonly OutingRepository _outingRepo = new OutingRepository();
        private readonly Outing _outing1 = new Outing();
        private readonly Outing _outing2 = new Outing();
        private readonly Outing _outing3 = new Outing();
        private readonly Outing _outing4 = new Outing();

        [TestInitialize]
        public void Initialize()
        {
            _outing1.EventType = EventType.Golf;
            _outing1.NumberOfAttendees = 23;
            _outing1.EventCostFlat = 2000;
            _outing1.EventCostPerPerson = 25;
            _outing1.Date = new DateTime(2021, 6, 20);

            _outing2.EventType = EventType.Bowling;
            _outing2.NumberOfAttendees = 15;
            _outing2.EventCostPerPerson = 13.50m;
            _outing2.Date = new DateTime(2021, 7, 10);

            _outing3.EventType = EventType.AmusementPark;
            _outing3.NumberOfAttendees = 42;
            _outing3.EventCostPerPerson = 45.95m;
            _outing3.Date = new DateTime(2021, 5, 10);

            _outing4.EventType = EventType.Concert;
            _outing4.NumberOfAttendees = 324;
            _outing4.EventCostFlat = 5000;
            _outing4.Date = new DateTime(2021, 5, 10);
        }

        [TestMethod]
        public void OutingRepository_AddOutingToList_ShouldReturnTrue()
        {
            // Arrange
            // Necessary items arranged in TestInitialize method Initialize()

            // Act
            bool result = _outingRepo.AddOutingToList(_outing1);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OutingRepository_AddOutingToList_ShouldReturnFalse()
        {
            // Arrange
            // Necessary items arranged in TestInitialize method Initialize()

            // Act
            bool result = _outingRepo.AddOutingToList(null);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        // No logic in the tested method
        public void OutingRepository_GetAllOutings_ShouldReturnCorrectCount()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _outingRepo.AddOutingToList(_outing1);
            _outingRepo.AddOutingToList(_outing2);
            _outingRepo.AddOutingToList(_outing3);

            // Act
            int actual = _outingRepo.GetAllOutings().Count;
            int expected = 3;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OutingRepository_UpdateOuting_ShouldReturnTrue()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _outingRepo.AddOutingToList(_outing1);
            _outingRepo.AddOutingToList(_outing2);
            Outing updatedOuting = new Outing(EventType.Bowling, 13, new DateTime(2021, 6, 30), 0, 13.50m);

            // Act
            bool result = _outingRepo.UpdateOuting(_outing1.OutingId, updatedOuting);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OutingRepository_UpdateOuting_ShouldReturnFalse()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _outingRepo.AddOutingToList(_outing1);
            _outingRepo.AddOutingToList(_outing2);
            Outing updatedOuting = new Outing(EventType.Bowling, 13, new DateTime(2021, 6, 30), 0, 13.50m);

            // Act
            // Updated outing is new and therefore not in the outing repo.
            bool result = _outingRepo.UpdateOuting(updatedOuting.OutingId, updatedOuting);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void OutingRepository_DeleteOutingById_ShouldReturnTrue()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _outingRepo.AddOutingToList(_outing1);
            _outingRepo.AddOutingToList(_outing2);

            // Act
            bool result = _outingRepo.DeleteOutingById(_outing2.OutingId);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OutingRepository_DeleteOutingById_ShouldReturnFalse()
        {
            // Arrange
            // Some necessary items arranged in TestInitialize method Initialize()
            _outingRepo.AddOutingToList(_outing1);
            _outingRepo.AddOutingToList(_outing2);

            // Act
            // New outing is not added to repo and should be absent.
            bool result = _outingRepo.DeleteOutingById(new Outing().OutingId);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
