using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.Data
{
    public class KeepDailyContext : DbContext
    {
        public KeepDailyContext(DbContextOptions<KeepDailyContext> options) : base(options) { }

        public DbSet<Plan> Plan { get; set; } = null!;
        public DbSet<Day> Day { get; set; } = null!;
        public DbSet<User> AppUser { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var admin = new User { Id = 1, Name = "Inky", Email = "a@a", Password = "123" };
            admin.Password = new PasswordHasher<User>().HashPassword(admin, admin.Password);
            builder.Entity<User>().HasData(admin);
        }
    }
}
