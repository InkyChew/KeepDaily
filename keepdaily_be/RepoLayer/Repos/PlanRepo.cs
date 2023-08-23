using DomainLayer.Data;
using DomainLayer.Models;
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

        public IEnumerable<Plan> GetAllPlan()
        {
            return _plans;
        }

        public IEnumerable<Plan> GetAllPlanWithDetail()
        {
            return _plans.Include(_ => _.Days);
        }

        public Plan? GetPlan(int id)
        {
            return _plans.SingleOrDefault(_ => _.Id == id);
        }

        public Plan? GetPlanWithDetail(int id)
        {
            return _plans.Include(_ => _.Days).SingleOrDefault(_ => _.Id == id);
        }

        public Plan InsertPlan(Plan plan)
        {
            _plans.Add(plan);
            _db.SaveChanges();
            return plan;
        }

        public Plan UpdatePlan(Plan plan)
        {
            _db.Update(plan);
            _db.SaveChanges();
            return plan;
        }

        public void DeletePlan(int id)
        {
            var plan = FindPlan(id);
            _plans.Remove(plan);
            _db.SaveChanges();
        }

        public Plan FindPlan(int id)
        {
            return _plans.Find(id) ?? throw new KeyNotFoundException($"Plan(id:{id}) not found.");
        }
    }
}
