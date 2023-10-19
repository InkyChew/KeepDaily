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
        public DbSet<Message> Message { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var admin = new User { Id = 1, Name = "Inky", Email = "a@a", Password = "123@456", UserLevel = UserLevel.Admin, IsActive = true, EmailConfirmed = true };
            admin.Password = new PasswordHasher<User>().HashPassword(admin, admin.Password);
            builder.Entity<User>().HasData(admin);

            builder.Entity<Friend>().HasKey(t => new { t.UserId, t.FriendId });

            builder.Entity<Category>().HasData(new List<Category>
            {
                new Category { Id = 1, Name = "Sports", Name_zh = "運動" },
                new Category { Id = 2, Name = "Diet", Name_zh = "健康飲食" },
                new Category { Id = 3, Name = "Cooking", Name_zh = "烹飪" },
                new Category { Id = 4, Name = "Baking", Name_zh = "烘焙" },
                new Category { Id = 5, Name = "Planting", Name_zh = "植物" },
                new Category { Id = 6, Name = "Painting", Name_zh = "繪畫" },
                new Category { Id = 7, Name = "Traveling", Name_zh = "旅行" },
                new Category { Id = 8, Name = "Learning", Name_zh = "學習" },
                new Category { Id = 9, Name = "Music", Name_zh = "音樂" },
                new Category { Id = 10, Name = "Daily", Name_zh = "日常" }
            });
        }
    }
}
