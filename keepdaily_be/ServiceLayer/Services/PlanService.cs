using DomainLayer.Dto;
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

        public Plan CreatePlan(Plan plan)
        {
            return _repo.InsertPlan(plan);
        }

        public Plan CreatePlanWithDetail(VMPlan vmPlan)
        {
            var plan = TransformToPlan(vmPlan);
            return _repo.InsertPlan(plan);
        }

        public void DeletePlan(int id)
        {
            _repo.DeletePlan(id);
        }

        public IEnumerable<Plan> GetAllPlan()
        {
            return _repo.GetAllPlan().OrderByDescending(_ => _.UpdateTime);
        }

        public List<VMPlan> GetAllPlanWithDetail()
        {
            var vmPlans = new List<VMPlan>();
            var plans = _repo.GetAllPlan();
            foreach (var plan in plans)
            {
                vmPlans.Add(TransformToVMPlan(plan));
            }
            return vmPlans;
        }

        public Plan? GetPlan(int id)
        {
            return _repo.GetPlan(id);
        }

        public Plan? GetPlanWithDetail(int id, int? year = null, int? month = null)
        {
            var plan = _repo.GetPlanWithDetail(id);
            if(plan != null)
            {
                var sf = plan.StartFrom.Split('-');
                int y = year ?? int.Parse(sf[0]);
                int m = month ?? int.Parse(sf[1]);
                plan.Days = CreateDayList(plan, y, m);
            }
            return plan;
        }

        public Plan UpdatePlan(Plan plan)
        {
            return _repo.UpdatePlan(plan);
        }

        public Plan UpdatePlanWithDetail(VMPlan vmPlan)
        {
            var plan = TransformToPlan(vmPlan);
            return _repo.UpdatePlan(plan);
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

        private VMPlan TransformToVMPlan(Plan plan)
        {
            return new VMPlan
            {
                Id = plan.Id,
                Title = plan.Title,
                Description = plan.Description,
                StartFrom = plan.StartFrom,
                Calendars = plan.Days.GroupBy(c => new { c.Year, c.Month })
                                .Select(g => new VMPlanCalendar
                                {
                                    Year = g.Key.Year,
                                    Month = g.Key.Month,
                                    Days = g.Select(d => new VMPlanDay
                                    {
                                        DayId = d.Id,
                                        Date = d.Date,
                                        ImgUrl = d.ImgName
                                    }).ToList()
                                }).ToList()
            };
        }

        private Plan TransformToPlan(VMPlan vmPlan)
        {
            var plan = new Plan
            {
                Id = vmPlan.Id,
                Title = vmPlan.Title,
                Description = vmPlan.Description
            };
            foreach (var c in vmPlan.Calendars)
            {
                foreach(var d in c.Days)
                {
                    plan.Days.Add(new Day()
                    {
                        Id = d.DayId,
                        Year = c.Year,
                        Month = c.Month,
                        Date = d.Date,
                        ImgName = d.ImgUrl
                    });
                }
            }
            return plan;
        }
    }
}
