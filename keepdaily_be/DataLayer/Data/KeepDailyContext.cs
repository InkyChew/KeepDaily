using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.Data
{
    public class KeepDailyContext : DbContext
    {
        public KeepDailyContext(DbContextOptions<KeepDailyContext> options) : base(options) { }

        public DbSet<Plan> Plan { get; set; }
        public DbSet<Day> Day { get; set; }
        public DbSet<User> AppUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(new User { Id = 1, Name = "Inky", Email = "a@a", Password = "123" });
        }
    }
}
