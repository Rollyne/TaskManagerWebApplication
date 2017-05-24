using System;
using Data.Entities.Entities;
using Data.Entities.Repositories;
using DbDataProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskManagerASP.Tools;

namespace TaskManagerASP.Services
{
    public class AuthenticationService
    {
        private IRepository<User> _repo;
        public AuthenticationService(IRepository<User> repository)
        {
            _repo = repository;
        }
        public AuthenticationService()
        {
        }

        public User LoggedUser { get; set; }
        public void AuthenticateUser(string userName, string password, IConfiguration config)
        {
            if(_repo == default(IRepository<User>))
                _repo = new RepositoryClient().GetRepositoryProvider().GetUserRepository();
            LoggedUser = _repo.FirstOrDefault(u => u.UserName == userName && u.Password == password);
        }

        public void GetBy(string username, string password, object p)
        {
            throw new NotImplementedException();
        }
    }
}
