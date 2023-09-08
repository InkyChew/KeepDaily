using DomainLayer.Models;
using RepoLayer.IRepos;
using ServiceLayer.IServices;

namespace ServiceLayer.Services
{
    public class DayService : IDayService
    {
        private readonly IDayRepo _repo;

        public DayService(IDayRepo repo)
        {
            _repo = repo;
        }

        public Day UpdateDay(Day day)
        {
            return _repo.UpdateDay(day);
        }

        public Day DeleteDay(int id)
        {
            return _repo.DeleteDay(id);
        }
    }
}
