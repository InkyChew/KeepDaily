using DomainLayer.Models;

namespace ServiceLayer.IServices
{
    public interface IUserService
    {
        public Task<User> RegisterAsync(string name, string email, string password);
        public User Login(string email, string password);
        public User? GetUser(int id);
        public User FindUser(int id);
        public User UpdateUserInfo(User user);
        public void UpdateUserLineToken(string email, string token);
        public void InActiveUser(int id);
        public void SaveChanges();
    }
}
