using FileDataProvider.Entities;
using FileDataProvider.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TaskManagerASP.Configuration;

namespace UnitTests.FileDataProviderTests.RepositoriesTests.Mocks
{
    internal static class TaskRepositoryMocks
    {
        public static string FakeTaskDataFileProvider(params Task[] entities)
        {
            const string mockFileName = "mockTasks.dat";
            var repoProvider = new RepositoryProvider();
            var repository = repoProvider.GetTaskRepository(mockFileName);
            string filePath = $"{repoProvider.DataPath}\\{mockFileName}";
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    foreach (var task in entities)
                    {
                        typeof(TaskRepository)
                            .GetMethod("writeItem", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                            .Invoke(repository, new object[] { task, sw });
                    }
                }
            }
            return filePath;
        }

        public static string FakeTaskDataFileProvider(int tasksAmount = 3)
            => FakeTaskDataFileProvider(GetFakeTasks(tasksAmount));


        public static Task[] GetFakeTasks(int tasksAmount = 3)
        {
            var users = new List<Task>();
            for (int i = 1; i <= tasksAmount; i++)
            {
                users.Add(new Task()
                {
                    Id = i,
                    Header = Guid.NewGuid().ToString(),
                    Description = Guid.NewGuid().ToString(),
                    RequiredHours = i * i,
                    ExecutitiveId = i + 3,
                    CreatedOn = DateTime.Now.AddDays(i),
                    LastEditedOn = DateTime.Now.AddDays(i * i),
                    CreatorId = i+2,
                    IsCompleted = i % 2 == 0
                });
            }

            return users.ToArray();
        }
    }
}
