using System;
using FileDataProvider.Entities;
using FileDataProvider.Repositories;
using Microsoft.Extensions.Configuration;

namespace FileDataProvider.Services
{
    public class AuthenticationService
    {
        private UserRepository _repo;
        public AuthenticationService(UserRepository repository)
        {
            _repo = repository;
        }
        public AuthenticationService()
        {
        }

        public User LoggedUser { get; set; }
        public void AuthenticateUser(string userName, string password, IConfiguration config)
        {
            if(_repo == default(UserRepository))
                _repo = new RepositoryProvider(config).GetUserRepository();
            LoggedUser = _repo.GetByUserNameAndPassword(userName, password);
        }

        public void GetBy(string username, string password, object p)
        {
            throw new NotImplementedException();
        }
    }
}
