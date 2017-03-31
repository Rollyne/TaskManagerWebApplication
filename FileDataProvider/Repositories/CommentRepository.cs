using System;
using System.IO;
using FileDataProvider.Entities;

namespace FileDataProvider.Repositories
{
    public class CommentRepository : BaseRepository<Comment>
    {
        public CommentRepository(string filePath) : base(filePath)
        {
        }

        protected override Comment readItem(StreamReader sr)
        {
            var item = new Comment();
            item.Id = int.Parse(sr.ReadLine());
            item.TaskId = int.Parse(sr.ReadLine());
            item.AuthorId = int.Parse(sr.ReadLine());
            item.Body = sr.ReadLine();
            return item;

        }

        protected override void writeItem(Comment item, StreamWriter sw)
        {
            sw.WriteLine(item.Id);
            sw.WriteLine(item.TaskId);
            sw.WriteLine(item.AuthorId);
            sw.WriteLine(item.Body);
        }

        protected override bool hasAccess(Comment item, int parentId)
        {
            if (parentId == -1 || item.AuthorId == parentId)
                return true;
            return false;
        }
    }
}
