using FileDataProvider.Entities;
using FileDataProvider.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TaskManagerASP.Configuration;

namespace UnitTests.FileDataProviderTests.RepositoriesTests.Mocks
{
    internal static class CommentRepositoryMocks
    {
        public static string FakeCommentDataFileProvider(params Comment[] entities)
        {
            const string mockFileName = "mockComments.dat";
            var repoProvider = new RepositoryProvider();
            var repository = repoProvider.GetCommentRepository(mockFileName);
            string filePath = $"{repoProvider.DataPath}\\{mockFileName}";
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    foreach (var comment in entities)
                    {
                        typeof(CommentRepository)
                            .GetMethod("writeItem", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                            .Invoke(repository, new object[] { comment, sw });
                    }
                }
            }
            return filePath;
        }

        public static string FakeCommentDataFileProvider(int commentsAmount = 3)
            => FakeCommentDataFileProvider(GetFakeComments(commentsAmount));


        public static Comment[] GetFakeComments(int commentsAmount = 3)
        {
            var comments = new List<Comment>();
            for (int i = 1; i <= commentsAmount; i++)
            {
                comments.Add(new Comment()
                {
                    Id = i,
                    TaskId = i * i,
                    AuthorId = i + 1,
                    Body = Guid.NewGuid().ToString()
                });
            }

            return comments.ToArray();
        }
    }
}
