using BeastBattler.Backend.Database.DataModels;
using Microsoft.EntityFrameworkCore;

namespace BeastBattler.Backend.Database
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
                Password = "1234"
            });
        }

        #region DbSets
        public DbSet<Cage> Cages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Creature> Creatures { get; set; }
        #endregion
    }
}