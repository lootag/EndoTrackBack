using Microsoft.EntityFrameworkCore;
using Persistence.ORMEntities;

namespace Persistence.Contexts
{
    public class EndoscopesTrackingContext : DbContext
    {
        private readonly string _connectionString;
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Process> Processes { get; set; }

        public EndoscopesTrackingContext(string connectionString)
        {
            this._connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(this._connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Customer>()
                        .Property(c => c.CustomerId)
                        .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Machine>()
                        .Property(m => m.MachineId)
                        .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Process>()
                        .Property(p => p.ProcessId)
                        .ValueGeneratedOnAdd();
        }
    }
}