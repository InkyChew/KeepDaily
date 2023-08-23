using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RepoLayer.IRepos;

namespace RepoLayer.Repos
{
    public class DayRepo : IDayRepo
    {
        private readonly KeepDailyContext _db;
        private readonly DbSet<Day> _days;

        public DayRepo(KeepDailyContext db)
        {
            _db = db;
            _days = db.Day;
        }

        public Day InsertDay(Day day)
        {
            _days.Add(day);
            _db.SaveChanges();
            return day;
        }

        public Day UpdateDay(Day day)
        {
            _days.Update(day);
            _db.SaveChanges();
            return day;
        }

        public Day FindDay(int id)
        {
            return _days.Find(id) ?? throw new KeyNotFoundException($"Day(id:{id}) not found.");
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public Day DeleteDay(int id)
        {
            var day = FindDay(id);
            _db.Remove(day);
            _db.SaveChanges();
            return day;
        }

        public IDbContextTransaction BeginTransaction()
        {
            using var transaction = _db.Database.BeginTransaction();
            return transaction;
        }
    }
}
