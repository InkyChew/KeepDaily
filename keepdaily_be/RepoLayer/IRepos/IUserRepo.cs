using DomainLayer.Models;

namespace RepoLayer.IRepos
{
    public interface IUserRepo
    {
        public User? GetUser(int id);
        public User GetUser(string email);
        public User InsertUser(User user);
        public User FindUser(int id);
        public void SaveChanges();
        public void InActiveUser(int id);
    }
}
