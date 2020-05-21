using System.Data.Entity;
using System.Configuration;
using System.Linq;
using Database.Entity;
using System.Data.Entity.Infrastructure;

namespace Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TestEntity> TestTable { get; set; }
        public DbSet<Trigger> Triggers { get; set; }
        public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext Create()
            {
                var cs = ConfigurationManager.ConnectionStrings["ApplicationDbContext"];
                return new ApplicationDbContext(cs.ConnectionString);
            }
        }
        public ApplicationDbContext(string connectionString = "")
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                Database.Connection.ConnectionString = connectionString;
            }
            else
            {
                var cs = ConfigurationManager.ConnectionStrings["ApplicationDbContext"];
                Database.Connection.ConnectionString = cs.ConnectionString;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
