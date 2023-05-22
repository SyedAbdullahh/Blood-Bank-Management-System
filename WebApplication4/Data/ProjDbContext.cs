using Microsoft.EntityFrameworkCore;
using WebApplication4.Configuration;
using WebApplication4.Models;

namespace WebApplication4.Data
{
    public class ProjDbContext:DbContext
    {
        public ProjDbContext(DbContextOptions<ProjDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CityConfig());

        }
        public DbSet<WebApplication4.Models.User> Users { get; set; }
        public DbSet<WebApplication4.Models.Admin> Admins { get; set; }
        public DbSet<WebApplication4.Models.Hospital> Hospitals { get; set; }
        public DbSet<WebApplication4.Models.Transaction> Transactions { get; set; }
        public DbSet<WebApplication4.Models.Blood_data> Bloods { get; set; }
        public DbSet<WebApplication4.Models.Employee> Employees { get; set; }
        public DbSet<WebApplication4.Models.City> Cities { get; set; }
        

    }
   
}
