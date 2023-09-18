using DomainLayer.Models;
using RepoLayer.IRepos;
using ServiceLayer.IServices;

namespace ServiceLayer.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepo _repo;

        public PlanService(IPlanRepo repo)
        {
            _repo = repo;
        }

        public IEnumerable<Plan> GetAllPlan()
        {
            return _repo.GetAllPlan().OrderByDescending(_ => _.UpdateTime);
        }

        public Plan GetPlan(int id)
        {
            return _repo.GetPlan(id) ?? throw new KeyNotFoundException($"Plan(Id:{id}) does not exist.");
        }

        public Plan? GetPlanWithDetail(int id, int? year = null, int? month = null)
        {
            var plan = _repo.GetPlanWithDetail(id);
            if(plan != null)
            {
                int y = year ?? plan.UpdateTime.Year;
                int m = month ?? plan.UpdateTime.Month;
                plan.Days = CreateDayList(plan, y, m);
            }
            return plan;
        }

        public Plan CreatePlan(Plan plan)
        {
            _repo.InsertPlan(plan);
            return GetPlan(plan.Id);
        }

        public Plan UpdatePlan(Plan plan)
        {
            _repo.UpdatePlan(plan);
            return GetPlan(plan.Id);
        }

        public void DeletePlan(int id)
        {
            _repo.DeletePlan(id);
        }

        private List<Day> CreateDayList(Plan plan, int year, int month)
        {
            var days = new List<Day>();
            int lastDate = DateTime.DaysInMonth(year, month);
            int preLastDate = (month == 1) ? DateTime.DaysInMonth(year - 1, 12)  : DateTime.DaysInMonth(year, month - 1);
            int firstDay = new DateTime(year, month, 1).DayOfWeek.GetHashCode();
            int lastDay = new DateTime(year, month, lastDate).DayOfWeek.GetHashCode();

            for (int i = firstDay; i > 0; i--)
            {
                days.Add(NewDay(plan, year, month - 1, preLastDate - i + 1));
            }

            for (int i = 1; i <= lastDate; i++)
            {
                days.Add(NewDay(plan, year, month, i));
            }

            for (int i = lastDay; i < 6; i++)
            {
                days.Add(NewDay(plan, year, month + 1, i - lastDay + 1));
            }

            return days;
        }

        private static Day NewDay(Plan plan, int year, int month, int date)
        {
            return plan.Days.SingleOrDefault(_ => _.Year == year && _.Month == month && _.Date == date)
                        ?? new() { Year = year, Month = month, Date = date, PlanId = plan.Id };
        }
    }
}
