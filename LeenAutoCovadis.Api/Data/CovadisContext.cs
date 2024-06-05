using LeenAutoCovadis.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LeenAutoCovadis.Api.Data
{
    public class CovadisContext : DbContext
    {
        public CovadisContext(DbContextOptions<CovadisContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Role> Roles { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "admin@example.com",
                    Password = "string"
                },
                new User
                {
                    Id = 2,
                    Name = "User",
                    Email = "user@example.com",
                    Password = "string"
                });

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin"
                },
                new Role
                {
                    Id = 2,
                    Name = "User"
                });
        }
    }
}
