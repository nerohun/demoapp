using Microsoft.EntityFrameworkCore;
using NIDemo.Models;
using System.Data.Common;

namespace NIDemo.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Patient> Patient { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasKey(p => p.Id);
        }
    }
}
