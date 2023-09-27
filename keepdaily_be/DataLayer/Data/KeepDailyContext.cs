using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.Data
{
    public class KeepDailyContext : DbContext
    {
        public KeepDailyContext(DbContextOptions<KeepDailyContext> options) : base(options) { }

        public DbSet<Plan> Plan { get; set; } = null!;
        public DbSet<Category> Category { get; set; } = null!;
        public DbSet<Day> Day { get; set; } = null!;
        public DbSet<User> AppUser { get; set; } = null!;
        public DbSet<Friend> Friend { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var admin = new User { Id = 1, Name = "Inky", Email = "a@a", Password = "123@456", UserLevel = UserLevel.Admin, IsActive = true, EmailConfirmed = true };
            admin.Password = new PasswordHasher<User>().HashPassword(admin, admin.Password);
            builder.Entity<User>().HasData(admin);

            builder.Entity<Friend>().HasKey(t => new { t.UserId, t.FriendId });

            builder.Entity<Category>().HasData(new List<Category>
            {
                new Category { Id = 1, Name = "Sports" },
                new Category { Id = 2, Name = "Diet" },
                new Category { Id = 3, Name = "Cooking" },
                new Category { Id = 4, Name = "Baking" },
                new Category { Id = 5, Name = "Planting" },
                new Category { Id = 6, Name = "Painting" },
                new Category { Id = 7, Name = "Traveling" },
                new Category { Id = 8, Name = "Learning" }
            });
        }
    }
}
