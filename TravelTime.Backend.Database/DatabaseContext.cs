using TravelTime.Backend.Database.DataModels;
using Microsoft.EntityFrameworkCore;

namespace TravelTime.Backend.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.SetCommandTimeout(300);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Email = "admin",
                Rank = 0,
                Password = "1234",
                CreatedAt = DateTime.Now,
            });
        }

        #region DbSets
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion
    }
}