using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace RepoLayer.IRepos
{
    public interface IDayRepo
    {
        public Day InsertDay(Day day);
        public Day UpdateDay(Day day);
        public Day FindDay(int id);
        public void SaveChanges();
        public Day DeleteDay(int id);
        public IDbContextTransaction BeginTransaction();
    }
}
