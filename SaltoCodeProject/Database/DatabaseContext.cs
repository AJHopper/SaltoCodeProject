using Microsoft.EntityFrameworkCore;
using SaltoCodeProject.Entities;

namespace SaltoCodeProject.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new LockConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Lock> Locks { get; set; }
    }
}
