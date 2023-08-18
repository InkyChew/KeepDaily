using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data
{
    public class KeepDailyContext : DbContext
    {
        public KeepDailyContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Plan> Plan { get; set; }
        public virtual DbSet<Day> Day { get; set; }
    }
}
