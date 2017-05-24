using System;
using System.IO;
using Data.Entities.Entities;
using Data.Entities.Repositories;


namespace FileDataProvider.Repositories
{
    public class TaskRepository : BaseRepository<Task>
    {
        public TaskRepository(string filePath) : base(filePath)
        {
        }

        protected override Task readItem(StreamReader sr)
        {
            var item = new Task();

            item.Id = int.Parse(sr.ReadLine());
            item.Header = sr.ReadLine();
            item.Description = sr.ReadLine();
            item.RequiredHours = int.Parse(sr.ReadLine());
            item.ExecutitiveId = int.Parse(sr.ReadLine());
            item.CreatorId = int.Parse(sr.ReadLine());
            item.CreatedOn = new DateTime(long.Parse(sr.ReadLine()));
            item.LastEditedOn = new DateTime(long.Parse(sr.ReadLine()));
            item.IsCompleted = bool.Parse(sr.ReadLine());

            return item;
        }

        protected override void writeItem(Task item, StreamWriter sw)
        {
            sw.WriteLine(item.Id);
            sw.WriteLine(item.Header);
            sw.WriteLine(item.Description);
            sw.WriteLine(item.RequiredHours);
            sw.WriteLine(item.ExecutitiveId);
            sw.WriteLine(item.CreatorId);
            sw.WriteLine(item.CreatedOn.Ticks);
            sw.WriteLine(item.LastEditedOn.Ticks);
            sw.WriteLine(item.IsCompleted);  
        }
        protected override bool hasAccess(Task item, int parentId)
        {
            if (parentId == -1 || item.CreatorId == parentId || item.ExecutitiveId == parentId)
                return true;
            return false;
        }
    }
}
