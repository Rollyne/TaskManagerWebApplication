using System;
using FileDataProvider.Tools;

namespace FileDataProvider.Entities
{
    public class User : IIdentificatable, IEquatable<User>
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

        public bool Equals(User other)
        {
            return
                Id.Equals(other.Id) &&
                UserName.Equals(other.UserName) &&
                Password.Equals(other.Password) &&
                FirstName.Equals(other.FirstName) &&
                LastName.Equals(other.LastName) &&
                IsAdmin.Equals(other.IsAdmin);
        }
    }
}
