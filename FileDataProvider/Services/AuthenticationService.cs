using FileDataProvider.Entities;
using FileDataProvider.Repositories;

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
            _repo = new RepositoryProvider().GetUserRepository();
        }

        public User LoggedUser { get; set; }
        public void Authenticate(string userName, string password)
        {
            LoggedUser = _repo.GetByUserNameAndPassword(userName, password);
        }
    }
}
