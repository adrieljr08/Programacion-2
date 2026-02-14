using Microsoft.EntityFrameworkCore;
using System.Reflection;
using SystemAdminAPI.Entitties;

namespace AppUsersAPI.DbContextUsers
{
    public class AppDbContext : DbContext
    {
   
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Users> Users { get; set; }
    }
}