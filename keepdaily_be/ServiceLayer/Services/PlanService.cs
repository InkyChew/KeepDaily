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

        public Plan CreatePlan(VMPlan vmPlan)
        {
            var plan = TransformToPlan(vmPlan);
            return _repo.CreatePlan(plan);
        }

        public void DeletePlan(int id)
        {
            _repo.DeletePlan(id);
        }

        public List<VMPlan> GetAllPlan()
        {
            var vmPlans = new List<VMPlan>();
            var plans = _repo.GetAllPlan();
            foreach (var plan in plans)
            {
                vmPlans.Add(TransformToVMPlan(plan));
            }
            return vmPlans;
        }

        public VMPlan? GetPlan(int id)
        {
            var plan = _repo.GetPlan(id);
            return plan == null ? null : TransformToVMPlan(plan);
        }

        public Plan UpdatePlan(VMPlan vmPlan)
        {
            var plan = TransformToPlan(vmPlan);
            return _repo.UpdatePlan(plan);
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
                                        ImgUrl = d.ImgUrl
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
                        ImgUrl = d.ImgUrl
                    });
                }
            }
            return plan;
        }
    }
}
