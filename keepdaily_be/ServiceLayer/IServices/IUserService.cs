using DomainLayer.Dto;
using DomainLayer.Models;

namespace ServiceLayer.IServices
{
    public interface IUserService
    {
        public Task<User> RegisterAsync(string name, string email, string password);
        public User Login(string email, string password);
        public User? GetUser(int id);
        public User FindUser(int id);
        public User? GetUser(string email);
        public User UpdateUserInfo(VUser user);
        public void UpdatePassword(int id, string password);
        public void UpdateUserLineToken(string email, string token);
        public void InActiveUser(int id);
        public void SaveChanges();
    }
}
