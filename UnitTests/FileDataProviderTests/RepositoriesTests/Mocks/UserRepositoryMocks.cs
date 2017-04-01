using FileDataProvider.Entities;
using FileDataProvider.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TaskManagerASP.Configuration;

namespace UnitTests.FileDataProviderTests.RepositoriesTests.Mocks
{
    internal static class UserRepositoryMocks
    {
        public static string FakeUserDataFileProvider(params User[] entities)
        {
            const string mockFileName = "mockUsers.dat";
            var repoProvider = new RepositoryProvider();
            var repository = repoProvider.GetUserRepository(mockFileName);
            string filePath = $"{repoProvider.DataPath}\\{mockFileName}";
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    foreach (var user in entities)
                    {
                        typeof(UserRepository)
                            .GetMethod("writeItem", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                            .Invoke(repository, new object[] { user, sw });
                    }
                }
            }
            return filePath;
        }

        public static string FakeUserDataFileProvider(int usersAmount = 3) 
            => FakeUserDataFileProvider(GetFakeUsers(usersAmount));
        

        public static User[] GetFakeUsers(int usersAmount = 3)
        {
            var users = new List<User>();
            for (int i = 1; i <= usersAmount; i++)
            {
                users.Add(new User()
                {
                    Id = i,
                    UserName = Guid.NewGuid().ToString(),
                    Password = Guid.NewGuid().ToString(),
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString(),
                    IsAdmin = i % 2 == 0
                });
            }

            return users.ToArray();
        }
    }
}
