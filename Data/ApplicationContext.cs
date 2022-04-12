using MainLABAPI.Data.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace MainLABAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Models.DB.Task> Tasks { get; set; }

        public DbSet<Solution> Solutions { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
