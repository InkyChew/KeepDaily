using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepoLayer.IRepos;
using System;

namespace RepoLayer.Repos
{
    public class DayRepo : IDayRepo
    {
        private readonly KeepDailyContext _db;
        private readonly DbSet<Day> _days;
        private readonly DbSet<Plan> _plans;

        public DayRepo(KeepDailyContext db)
        {
            _db = db;
            _days = db.Day;
            _plans = db.Plan;
        }

        public IEnumerable<Day> GetAllDay()
        {
            return _days;
        }

        public IEnumerable<Day> GetDaysByPlan(int planId)
        {
            return _days.Where(_ => _.PlanId == planId);
        }

        public Day FindDay(int id)
        {
            return _days.Find(id) ?? throw new KeyNotFoundException($"Day(id:{id}) not found.");
        }

        public Day InsertDay(Day day)
        {
            _days.Add(day);
            _db.SaveChanges();
            return day;
        }

        public Day UpdateDay(Day day)
        {
            using var transaction =  _db.Database.BeginTransaction();
            try
            {
                _days.Update(day);
                var plan = _plans.Find(day.PlanId) ?? throw new KeyNotFoundException($"Plan(Id:{day.PlanId}) not found.");
                plan.UpdateTime = DateTime.Now;
                _db.SaveChanges();
                transaction.Commit();
                return day;
            }
            catch
            {
                throw;
            }
        }

        public Day DeleteDay(int id)
        {
            var transaction = _db.Database.BeginTransaction();
            try
            {
                var day = FindDay(id);
                _db.Remove(day);
                var plan = _plans.Find(day.PlanId) ?? throw new KeyNotFoundException($"Plan(Id:{day.PlanId}) not found.");
                plan.UpdateTime = DateTime.Now;
                _db.SaveChanges();
                transaction.Commit();
                return day;
            }
            catch
            {
                throw;
            }
        }
    }
}
