using System;
using FileDataProvider.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.FileDataProviderTests.EntitiesTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void IsTheIdSetProperly()
        {
            //Arrange
            var item = new User();
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
            var item = new User();
            int idTester = -56;

            //Act
            item.Id = idTester;
        }

        [TestMethod]
        public void IsTheUsernameSetProperly()
        {
            //Arrange
            var item = new User();
            string usernameTester = "jonathanDoe";

            //Act
            item.UserName = usernameTester;

            //Assert
            Assert.AreEqual(usernameTester, item.UserName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesPropertyThrowArgumentExceptionWhenEmptyUsernameStringIsSet()
        {
            //Arrange
            var item = new User();
            string usernameTester = string.Empty;

            //Act
            item.UserName = usernameTester;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesPropertyThrowArgumentExceptionWhenNullUsernameIsSet()
        {
            //Arrange
            var item = new User();
            const string usernameTester = null;

            //Act
            item.UserName = usernameTester;
        }
        [TestMethod]
        public void IsThePasswordSetProperly()
        {
            //Arrange
            var item = new User();
            const string passwordTester = "jonathanPass";

            //Act
            item.Password = passwordTester;

            //Assert
            Assert.AreEqual(passwordTester, item.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesPropertyThrowArgumentExceptionWhenEmptyPasswordStringIsSet()
        {
            //Arrange
            var item = new User();
            string passwordTester = string.Empty;

            //Act
            item.Password = passwordTester;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesPropertyThrowArgumentExceptionWhenNullPasswordIsSet()
        {
            //Arrange
            var item = new User();
            const string passwordTester = null;

            //Act
            item.Password = passwordTester;
        }
    }
}
