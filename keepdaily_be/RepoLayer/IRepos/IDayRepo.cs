using DomainLayer.Models;

namespace RepoLayer.IRepos
{
    public interface IDayRepo
    {
        public IEnumerable<Day> GetAllDay();
        public Day InsertDay(Day day);
        public Day UpdateDay(Day day);
        public Day FindDay(int id);
        public Day DeleteDay(int id);
    }
}
