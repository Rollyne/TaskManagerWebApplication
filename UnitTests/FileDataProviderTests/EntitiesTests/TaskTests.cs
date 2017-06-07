using System;
using Data.Entities.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.FileDataProviderTests.EntitiesTests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void IsTheIdSetProperly()
        {
            //Arrange
            var item = new Task();
            const int idTester = 56;

            //Act
            item.Id = idTester;

            //Assert
            Assert.AreEqual(idTester, item.Id);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesPropertyThrowArgumentExceptionWhenNegativeIdIsSet()
        {
            //Arrange
            var item = new Task();
            int idTester = -56;

            //Act
            item.Id = idTester;
        }

        [TestMethod]
        public void IsTheHeaderSetProperly()
        {
            //Arrange
            var item = new Task();
            string headerTester = "RAM Diagnostics Tool";

            //Act
            item.Header = headerTester;

            //Assert
            Assert.AreEqual(headerTester, item.Header);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesPropertyThrowArgumentExceptionWhenEmptyHeaderStringIsSet()
        {
            //Arrange
            var item = new Task();
            string headerTester = string.Empty;

            //Act
            item.Header = headerTester;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesPropertyThrowArgumentExceptionWhenNullUsernameIsSet()
        {
            //Arrange
            var item = new Task();
            const string headerTester = null;

            //Act
            item.Header = headerTester;
        }
        [TestMethod]
        public void IsTheRequiredHoursSetProperly()
        {
            //Arrange
            var item = new Task();
            const int hoursTester = 4;

            //Act
            item.RequiredHours = hoursTester;

            //Assert
            Assert.AreEqual(hoursTester, item.RequiredHours);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesPropertyThrowArgumentExceptionWhenNegativeRequiredHoursIsSet()
        {
            //Arrange
            var item = new Task();
            const int hoursTester = -4;

            //Act
            item.RequiredHours = hoursTester;
        }
        [TestMethod]
        public void IsTheExecutitiveIdSetProperly()
        {
            //Arrange
            var item = new Task();
            const int idTester = 56;

            //Act
            item.ExecutitiveId = idTester;

            //Assert
            Assert.AreEqual(idTester, item.ExecutitiveId);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesPropertyThrowArgumentExceptionWhenNegativeExecutitiveIdIsSet()
        {
            //Arrange
            var item = new Task();
            int idTester = -56;

            //Act
            item.ExecutitiveId = idTester;
        }
        [TestMethod]
        public void IsTheCreatorIdSetProperly()
        {
            //Arrange
            var item = new Task();
            const int idTester = 56;

            //Act
            item.CreatorId = idTester;

            //Assert
            Assert.AreEqual(idTester, item.CreatorId);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesPropertyThrowArgumentExceptionWhenNegativeCreatorIdIsSet()
        {
            //Arrange
            var item = new Task();
            int idTester = -56;

            //Act
            item.CreatorId = idTester;
        }
    }
}