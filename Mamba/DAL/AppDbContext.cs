using MambaProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MambaProject.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser,IdentityRole,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }
        public DbSet<Service> Services { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
            base.OnModelCreating(modelBuilder);
        }
    }
}
