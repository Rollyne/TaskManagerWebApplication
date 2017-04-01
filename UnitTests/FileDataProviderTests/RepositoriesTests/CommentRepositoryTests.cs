using FileDataProvider.Entities;
using FileDataProvider.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using UnitTests.FileDataProviderTests.RepositoriesTests.Mocks;

namespace UnitTests.FileDataProviderTests.RepositoryTests
{
    [TestClass]
    public class CommentRepositoryTests
    {
        [TestMethod]
        public void DoTheReadItemAndWriteItemFollowTheSamePattern()
        {
            //Arrange
            var fakeComments = CommentRepositoryMocks.GetFakeComments();
            string filePath = CommentRepositoryMocks.FakeCommentDataFileProvider(fakeComments);
            var repository = new CommentRepository(filePath);


            try
            {
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        foreach (var item in fakeComments)
                        {
                            //Act
                            var other = (Comment)typeof(CommentRepository)
                                       .GetMethod("readItem", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                                       .Invoke(repository, new object[] { sr });

                            //Assert
                            Assert.AreEqual(item.Id, other.Id);
                            Assert.AreEqual(item.TaskId, other.TaskId);
                            Assert.AreEqual(item.AuthorId, other.AuthorId);
                            Assert.AreEqual(item.Body, other.Body);
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
