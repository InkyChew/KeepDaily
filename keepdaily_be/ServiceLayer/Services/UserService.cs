using DomainLayer.Models;
using RepoLayer.IRepos;
using ServiceLayer.IServices;

namespace ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;

        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }

        public void ConfirmEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User? GetUser(int id)
        {
            return _repo.GetUser(id);
        }

        public void InActiveUser(int id)
        {
            throw new NotImplementedException();
        }

        public void Login(User user)
        {
            throw new NotImplementedException();
        }

        public void Register(User user)
        {
            throw new NotImplementedException();
        }

        public void SendConfirmEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(User user)
        {
            var dbUser = _repo.FindUser(user.Id);
            dbUser.Name = user.Name;
            dbUser.Password = user.Password;
            _repo.SaveChanges();
            return dbUser;
        }

        public void UpdateUserLineToken(string email, string token)
        {
            var user = _repo.GetUser(email);
            user.LineAccessToken = token;
            _repo.SaveChanges();
        }
    }
}
