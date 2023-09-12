using DomainLayer.Models;

namespace ServiceLayer.IServices
{
    public interface IUserService
    {
        public Task<User> RegisterAsync(string name, string email, string password);
        public AuthenticateUser Login(string email, string password);
        public AuthenticateUser RefreshToken(User user);
        public IEnumerable<User> GetAllUser();
        public User? GetUser(int id);
        public User FindUser(int id);
        public User? GetUser(string email);
        public User UpdateUserInfo(User user);
        public void UpdatePassword(int id, string password);
        public void UpdatePhoto(int id, string imgName, string imgType);
        public void UpdateUserLineToken(string email, string token);
        public void InActiveUser(int id);
        public void SaveChanges();
        public AuthenticateUser LoginWithGoogle(User user);
        public AuthenticateUser RegisterWithGoogle(User user);
    }
}
