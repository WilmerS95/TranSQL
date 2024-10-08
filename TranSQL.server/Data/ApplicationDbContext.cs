using TranSQL.Shared;
using Microsoft.EntityFrameworkCore;

namespace TranSQL.Server.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }


        public DbSet<Departamento> Departamento { get; set; }
    }
}
