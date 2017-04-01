using FileDataProvider.Entities;
using FileDataProvider.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using UnitTests.FileDataProviderTests.RepositoriesTests.Mocks;

namespace UnitTests.FileDataProviderTests.RepositoryTests
{
    [TestClass]
    public class TaskRepositoryTests
    {
        [TestMethod]
        public void DoTheReadItemAndWriteItemFollowTheSamePattern()
        {
            //Arrange
            var fakeTasks = TaskRepositoryMocks.GetFakeTasks();
            string filePath = TaskRepositoryMocks.FakeTaskDataFileProvider(fakeTasks);
            var repository = new TaskRepository(filePath);

            try
            {
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        foreach (var item in fakeTasks)
                        {
                            //Act
                            var other = (Task)typeof(TaskRepository)
                                       .GetMethod("readItem", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                                       .Invoke(repository, new object[] { sr });
                            //Assert
                            Assert.AreEqual(item.Id, other.Id);
                            Assert.AreEqual(item.Header,other.Header);
                            Assert.AreEqual(item.Description, other.Description);
                            Assert.AreEqual(item.RequiredHours, other.RequiredHours);
                            Assert.AreEqual(item.ExecutitiveId, other.ExecutitiveId);
                            Assert.AreEqual(item.CreatorId, other.CreatorId);
                            Assert.AreEqual(item.CreatedOn, other.CreatedOn);
                            Assert.AreEqual(item.LastEditedOn, other.LastEditedOn);
                            Assert.AreEqual(item.IsCompleted, other.IsCompleted);
                        }
                    }
                }
            }
            finally
            {
                File.Delete(filePath);
            }

        }
    }

}
