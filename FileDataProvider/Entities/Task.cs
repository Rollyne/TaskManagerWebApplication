using System;
using FileDataProvider.Tools;

namespace FileDataProvider.Entities
{
    public class Task : IIdentificatable
    {
        private int _id;
        private string _header;
        private int _requiredHours;
        private int _executitiveId;
        private int _creatorId;

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
        public string Header
        {
            get { return _header; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ErrorMessages.CannotBeNullOrEmpty("Header"));
                _header = value;
            }
        }
        public string Description { get; set; }
        public int RequiredHours
        {
            get { return _requiredHours; }
            set
            {
                if (value < 0)
                    throw new ArgumentException(ErrorMessages.CannotBeNegative("Required hours"));
                _requiredHours = value;
            }
        }
        public int ExecutitiveId
        {
            get { return _executitiveId; }
            set
            {
                if (value < 0)
                    throw new ArgumentException(ErrorMessages.CannotBeNegative("Required hours"));
                _executitiveId = value;
            }
        }
        public int CreatorId
        {
            get
            {
                return _creatorId;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException(ErrorMessages.CannotBeNegative("Required hours"));
                _creatorId = value;
            }
        }
        public DateTime CreatedOn { get; set; }
        public DateTime LastEditedOn { get; set; }
        public bool IsCompleted { get; set; }

    }
}
