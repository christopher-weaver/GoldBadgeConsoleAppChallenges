using Cafe.Models;
using Cafe.Repos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Cafe.UnitTests
{
    [TestClass]
    public class Test
    {
        private readonly MenuItemRepository _menuRepo = new MenuItemRepository();
        private readonly MenuItem _item1 = new MenuItem();
        private readonly MenuItem _item2 = new MenuItem();
        private readonly MenuItem _item3 = new MenuItem();
        private readonly Spaghetti _spaghetti = new Spaghetti(40m, 350m);
        private readonly Meatball _meatball = new Meatball(70m, 200m);

        [TestInitialize]
        public void Initialize()
        {
            Marinara marinara = new Marinara(80m, 50m);
            Alfredo alfredo = new Alfredo(120m, 55m);
            Fettuccine fettuccine = new Fettuccine(50m, 350m);

            _item1.Number = 1;
            _item1.Name = "Classic spaghetti and meatballs";
            _item1.Description = "One serving of spaghetti with marinara and 3 meatballs";
            _item1.Recipe = new Dictionary<Ingredient, decimal>
            {
                { _spaghetti, 2m },
                { marinara, 3m },
                { _meatball, 3m }
            };
            _item1.Markup = 1.563m;

            _item2.Number = 2;
            _item2.Name = "Classic fettuccine alfredo";
            _item2.Description = "One serving of fettuccine with alfredo sauce";
            _item2.Recipe = new Dictionary<Ingredient, decimal>
            {
                { fettuccine, 2m },
                { alfredo, 3m },
            };
            _item2.Markup = 1.5m;

            _item3.Number = 3;
            _item3.Name = "Spaghetti with marinara";
            _item3.Description = "One serving of spaghetti with marinara";
            _item3.Recipe = new Dictionary<Ingredient, decimal>
            {
                { _spaghetti, 2m },
                { marinara, 3m },
            };
            _item3.Markup = 1.65m;
        }

        [TestMethod]
        // Tests create
        public void MenuItemRepository_AddItemToMenu_ShouldReturnTrue()
        {
            // Arrange
            // Necessary items arranged in TestInitialize method Initialize()

            // Act
            bool itemAdded = _menuRepo.AddItemToMenu(_item1);

            // Assert
            Assert.IsTrue(itemAdded);
        }

        [TestMethod]
        // Tests create
        public void MenuItemRepository_AddItemToMenu_ShouldReturnFalse()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            MenuItem item1 = null;

            // Act
            bool itemAdded = _menuRepo.AddItemToMenu(item1);

            // Assert
            Assert.IsFalse(itemAdded);
        }

        [TestMethod]
        // Tests read
        public void MenuItemRepository_GetMenu_ShouldHaveCorrectCount()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            _menuRepo.AddItemToMenu(_item1);

            // Act
            List<MenuItem> fetchedMenuList = _menuRepo.GetMenu();
            int actual = fetchedMenuList.Count;
            int expected = 1;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // Tests delete by item
        public void MenuItemRepository_RemoveItem_ShouldReturnTrue()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            _menuRepo.AddItemToMenu(_item1);

            // Act
            bool result = _menuRepo.RemoveItem(_item1);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        // Tests delete by item
        public void MenuItemRepository_RemoveItem_ShouldReturnFalse()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            _menuRepo.AddItemToMenu(_item1);

            // Act
            bool result = _menuRepo.RemoveItem(_item2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        // Tests delete by ID
        public void MenuItemRepository_RemoveItemById_ShouldReturnTrue()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            _menuRepo.AddItemToMenu(_item1);

            // Act
            bool result = _menuRepo.RemoveItem(_item1.Number);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        // Tests delete by ID
        public void MenuItemRepository_RemoveItemById_ShouldReturnFalse()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            _menuRepo.AddItemToMenu(_item1);

            // Act
            bool result = _menuRepo.RemoveItem(99);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        // Tests delete by name
        public void MenuItemRepository_RemoveItemByName_ShouldReturnTrue()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            _menuRepo.AddItemToMenu(_item1);

            // Act
            bool result = _menuRepo.RemoveItem(_item1.Name);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        // Tests delete by name
        public void MenuItemRepository_RemoveItemByName_ShouldReturnFalse()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            _menuRepo.AddItemToMenu(_item1);

            // Act
            bool result = _menuRepo.RemoveItem("Mapo Tofu");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        // Tests delete by ingredient
        public void MenuItemRepository_RemoveItemsWithIngredient_ShouldReturnCorrectInt()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            _menuRepo.AddItemToMenu(_item1);
            _menuRepo.AddItemToMenu(_item2);
            _menuRepo.AddItemToMenu(_item3);

            // Act
            int actual = _menuRepo.RemoveItemsWithIngredient(_spaghetti);
            int expected = 2;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // Tests delete by ingredient
        public void MenuItemRepository_RemoveItemsWithIngredient_ShouldNotRemoveAnyMenuItems()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            _menuRepo.AddItemToMenu(_item2);
            _menuRepo.AddItemToMenu(_item3);

            // Act
            int actual = _menuRepo.RemoveItemsWithIngredient(_meatball);
            int expected = 0;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MenuItemRepository_GetByItemById_ShouldReturnMenuItem()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            _menuRepo.AddItemToMenu(_item1);

            // Act
            MenuItem fetchedItem = _menuRepo.GetItemById(1);
            int actual = fetchedItem.Number;
            int expected = 1;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MenuItemRepository_GetByItemById_ShouldReturnNull()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            _menuRepo.AddItemToMenu(_item1);

            // Act
            MenuItem fetchedItem = _menuRepo.GetItemById(99);

            // Assert
            Assert.IsNull(fetchedItem);
        }

        [TestMethod]
        public void MenuItemRepository_GetByItemByName_ShouldReturnMenuItem()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            _menuRepo.AddItemToMenu(_item1);

            // Act
            MenuItem fetchedItem = _menuRepo.GetItemByName("Classic spaghetti and meatballs");
            string actual = fetchedItem.Name;
            string expected = _item1.Name;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MenuItemRepository_GetByItemByName_ShouldReturnNull()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            _menuRepo.AddItemToMenu(_item1);

            // Act
            MenuItem fetchedItem = _menuRepo.GetItemByName("Lazi Chicken");

            // Assert
            Assert.IsNull(fetchedItem);
        }

        [TestMethod]
        public void MenuItemRepository_GetItemsWithIngredient_ShouldReturnCorrectInt()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            _menuRepo.AddItemToMenu(_item1);
            _menuRepo.AddItemToMenu(_item2);
            _menuRepo.AddItemToMenu(_item3);

            // Act
            List<MenuItem> fetchedMenuItems = _menuRepo.GetItemsByIngredient(_spaghetti);
            int actual = fetchedMenuItems.Count;
            int expected = 2;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MenuItemRepository_GetItemsWithIngredient_ShouldReturnEmptyList()
        {
            // Arrange
            // Most necessary items arranged in TestInitialize method Initialize()
            _menuRepo.AddItemToMenu(_item2);
            _menuRepo.AddItemToMenu(_item3);

            // Act
            List<MenuItem> fetchedMenuItems = _menuRepo.GetItemsByIngredient(_meatball);
            int actual = fetchedMenuItems.Count;
            int expected = 0;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
