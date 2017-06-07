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
        public void AuthenticateUser(string userName, string password)
        {
            if(_repo == default(IRepository<User>))
                _repo = new RepositoryClient().GetRepositoryProvider().GetRepository<User>();
            LoggedUser = _repo.FirstOrDefault(where: u => u.UserName == userName && u.Password == password);
        }

        public void RegisterUser(User item)
        {
            if (_repo == default(IRepository<User>))
                _repo = new RepositoryClient().GetRepositoryProvider().GetRepository<User>();
            
            _repo.Add(item);
            _repo.Save();
        }
    }
}
