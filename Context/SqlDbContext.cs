using Metricos.Models;
using Microsoft.EntityFrameworkCore;

namespace Metricos.Context
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }
        public DbSet<Course> Course { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Details_Course> Details_Course { get; set; }
        public DbSet<Phases_Course> Phases_Course { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }


    }
}
