using DomainLayer.Models;

namespace ServiceLayer.IServices
{
    public interface IUserService
    {
        public void Register(User user);
        public void Login(User user);
        public void SendConfirmEmail(string email);
        public void ConfirmEmail(string email);
        public User? GetUser(int id);
        public User UpdateUser(User user);
        public void UpdateUserLineToken(string email, string token);
        public void InActiveUser(int id);
    }
}
