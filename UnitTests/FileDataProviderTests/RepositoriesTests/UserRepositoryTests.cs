using System.IO;
using System.Reflection;
using Data.Entities.Entities;
using FileDataProvider.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.FileDataProviderTests.RepositoriesTests.Mocks;

namespace UnitTests.FileDataProviderTests.RepositoriesTests
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void DoTheReadItemAndWriteItemFollowTheSamePattern()
        {
            //Arrange
            var fakeUsers = UserRepositoryMocks.GetFakeUsers();
            string filePath = UserRepositoryMocks.FakeUserDataFileProvider(fakeUsers);
            var repository = new UserRepository(filePath);

            
            try
            {
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        foreach (var item in fakeUsers)
                        {
                            //Act
                            var other = (User)typeof(UserRepository)
                                       .GetMethod("readItem", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                                       .Invoke(repository, new object[] { sr });

                            //Assert
                            Assert.AreEqual(item.Id, other.Id);
                            Assert.AreEqual(item.UserName, other.UserName);
                            Assert.AreEqual(item.Password, other.Password);
                            Assert.AreEqual(item.FirstName, other.FirstName);
                            Assert.AreEqual(item.LastName, other.LastName);
                            Assert.AreEqual(item.IsAdmin, other.IsAdmin);
                        }
                    }
                }
            }
            finally
            {
                File.Delete(filePath);
            }

        }
        

        //TODO: GetByUserNameAndPassword Tests
    }

}
