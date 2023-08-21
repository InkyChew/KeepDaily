using DomainLayer.Models;

namespace RepoLayer.IRepos
{
    public interface IUserRepo
    {
        public User? GetUser(int id);
        public User GetUser(string email);
        public User FindUser(int id);
        public User InsertUser(User user);
        public void InActiveUser(int id);
        public void SaveChanges();
    }
}
