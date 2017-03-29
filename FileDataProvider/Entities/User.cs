using System;
using FileDataProvider.Tools;

namespace FileDataProvider.Entities
{
    public class User
    {
        private int _id;
        private string _userName;
        private string _password;

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
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ErrorMessages.CannotBeNullOrEmpty("Username"));
                _userName = value;
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ErrorMessages.CannotBeNullOrEmpty("Password"));
                _password = value;
            }
        }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
