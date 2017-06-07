using System;
using Data.Entities.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.FileDataProviderTests.EntitiesTests
{
    [TestClass]
    public class CommentTests
    {
        [TestMethod]
        public void IsTheIdSetProperly()
        {
            //Arrange
            var item = new Comment();
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
            var item = new Comment();
            int idTester = -56;

            //Act
            item.Id = idTester;
        }

        [TestMethod]
        public void IsTheTaskIdSetProperly()
        {
            //Arrange
            var item = new Comment();
            const int idTester = 56;

            //Act
            item.TaskId = idTester;

            //Assert
            Assert.AreEqual(idTester, item.TaskId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesPropertyThrowArgumentExceptionWhenNegativeTaskIdIsSet()
        {
            //Arrange
            var item = new Comment();
            int idTester = -56;

            //Act
            item.TaskId = idTester;
        }

        [TestMethod]
        public void IsTheAuthorIdSetProperly()
        {
            //Arrange
            var item = new Comment();
            const int idTester = 56;

            //Act
            item.AuthorId = idTester;

            //Assert
            Assert.AreEqual(idTester, item.AuthorId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesPropertyThrowArgumentExceptionWhenNegativeAuthorIdIsSet()
        {
            //Arrange
            var item = new Comment();
            int idTester = -56;

            //Act
            item.AuthorId = idTester;
        }

        [TestMethod]
        public void IsTheBodySetProperly()
        {
            //Arrange
            var item = new Comment();
            const string bodyTester = "BSoD is often caused by bad RAM sticks.";

            //Act
            item.Body = bodyTester;

            //Assert
            Assert.AreEqual(bodyTester, item.Body);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesPropertyThrowArgumentExceptionWhenEmptyBodyStringIsSet()
        {
            //Arrange
            var item = new Comment();
            var bodyTester = string.Empty;

            //Act
            item.Body = bodyTester;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesPropertyThrowArgumentExceptionWhenNullIsSet()
        {
            //Arrange
            var item = new Comment();
            string bodyTester = null;

            //Act
            item.Body = bodyTester;
        }

    }
}