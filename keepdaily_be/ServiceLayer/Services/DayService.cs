using DomainLayer.Models;
using RepoLayer.IRepos;
using ServiceLayer.IServices;
using System.Linq;

namespace ServiceLayer.Services
{
    public class DayService : IDayService
    {
        private readonly IDayRepo _repo;

        public DayService(IDayRepo repo)
        {
            _repo = repo;
        }

        public List<Day> GetDays(int planId, string start, string end)
        {
            return _repo.GetAllDay().Where(_ =>
            {
                var day = DateTime.Parse($"{_.Year}-{_.Month}-{_.Date}");
                var s = DateTime.Parse(start);
                var e = DateTime.Parse(end);
                return _.PlanId == planId
                        && day.CompareTo(s) >= 0 && day.CompareTo(e) <= 0;
            }).ToList();
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
