using E4_Project.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E4_Project.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<UserLogin> userLogins { get; set; }

    protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            // building the database
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=;database=e4-Project",
                ServerVersion.Parse("8.0.30"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserLogin>().HasData(
                new UserLogin
                {
                    Id = 1,
                    Username = "Admin",
                    Email = "Admin@admin.nl",
                    Password = "Root"
                    

                });
        }
    }
}

