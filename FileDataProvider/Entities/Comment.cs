using System;
using FileDataProvider.Tools;

namespace FileDataProvider.Entities
{
    public class Comment : IIdentificatable
    {
        private int _id;
        private int _taskId;
        private int _authorId;
        private string _body;

        public int Id
        {
            get { return _id; }
            set
            {
                if (value < 0)
                    throw new ArgumentException(ErrorMessages.CannotBeNegative("Id"));
                _id = value;
            }
        }
        public int TaskId
        {
            get
            {
                return _taskId;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException(ErrorMessages.CannotBeNegative("Task id"));
                _taskId = value;
            }
        }
        public int AuthorId
        {
            get
            {
                return _authorId;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException(ErrorMessages.CannotBeNegative("Author id"));
                _authorId = value;
            }
        }
        public string Body
        {
            get { return _body; }

            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ErrorMessages.CannotBeNullOrEmpty("Body"));
                _body = value;
            }
        }
    }
}
