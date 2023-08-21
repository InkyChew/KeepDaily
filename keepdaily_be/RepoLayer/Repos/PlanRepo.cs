using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepoLayer.IRepos;

namespace RepoLayer.Repos
{
    public class PlanRepo : IPlanRepo
    {
        private readonly KeepDailyContext _db;
        private readonly DbSet<Plan> _plans;

        public PlanRepo(KeepDailyContext db)
        {
            _db = db;
            _plans = db.Plan;
        }

        public Plan CreatePlan(Plan plan)
        {
            _plans.Add(plan);
            _db.SaveChanges();
            return plan;
        }

        public void DeletePlan(int id)
        {
            var plan = FindPlan(id);
            _plans.Remove(plan);
            _db.SaveChanges();
        }

        public IEnumerable<Plan> GetAllPlan()
        {
            return _plans.Include(_ => _.Days);
        }

        public Plan? GetPlan(int id)
        {
            return _plans.Include(_ => _.Days).SingleOrDefault(_ => _.Id == id);
        }

        public Plan UpdatePlan(Plan plan)
        {
            var dbPlan = FindPlan(plan.Id);
            dbPlan.Title = plan.Title;
            dbPlan.Description = plan.Description;
            _db.SaveChanges();
            return plan;
        }

        public Plan FindPlan(int id)
        {
            var plan = _plans.Find(id);
            if (plan == null) throw new KeyNotFoundException($"Plan(id:{id}) not found.");
            return plan;
        }
    }
}
