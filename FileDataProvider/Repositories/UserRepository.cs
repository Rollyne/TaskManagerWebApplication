using System;
using System.IO;
using FileDataProvider.Entities;

namespace FileDataProvider.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(string filePath) : base(filePath)
        {
        }

        protected override User readItem(StreamReader sr)
        {
            var item = new User();
            item.Id = int.Parse(sr.ReadLine());
            item.UserName = sr.ReadLine();
            item.Password = sr.ReadLine();
            item.FirstName = sr.ReadLine();
            item.LastName = sr.ReadLine();
            item.IsAdmin = bool.Parse(sr.ReadLine());
            return item;
        }

        protected override void writeItem(User item, StreamWriter sw)
        {
            sw.WriteLine(item.Id);
            sw.WriteLine(item.UserName);
            sw.WriteLine(item.Password);
            sw.WriteLine(item.FirstName);
            sw.WriteLine(item.LastName);
            sw.WriteLine(item.IsAdmin);
        }

        public User GetByUserNameAndPassword(string userName, string password)
        {
            using (var fs = new FileStream(this.filePath, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamReader(fs))
                {
                    while (sr.EndOfStream)
                    {
                        User item = readItem(sr);
                        if (item.UserName == userName && item.Password == password)
                            return item;
                    }
                }
            }
            return null;
        }
    }
}
