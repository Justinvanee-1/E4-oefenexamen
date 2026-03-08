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
        public DbSet<Inventory> Inventories { get; set; }

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
            modelBuilder.Entity<Inventory>().HasData(
        new Inventory
        {
            ItemId = 1,
            UserId = 1,
            ItemName = "Dragon Slayer Sword",
            Description = "A legendary sword used to slay dragons.",
            Type = "Weapon",
            Rarity = "Legendary",
            Power = "95",
            Speed = "70",
            Durability = "90",
            MagicalAddons = "Fire Damage"
        },
        new Inventory
        {
            ItemId = 2,
            UserId = 1,
            ItemName = "Shadow Dagger",
            Description = "A small dagger infused with dark magic.",
            Type = "Weapon",
            Rarity = "Epic",
            Power = "60",
            Speed = "95",
            Durability = "50",
            MagicalAddons = "Shadow Strike"
        },
        new Inventory
        {
            ItemId = 3,
            UserId = 1,
            ItemName = "Knight Armor",
            Description = "Heavy armor worn by royal knights.",
            Type = "Armor",
            Rarity = "Rare",
            Power = "20",
            Speed = "30",
            Durability = "95",
            MagicalAddons = "Damage Resistance"
        },
        new Inventory
        {
            ItemId = 4,
            UserId = 1,
            ItemName = "Wizard Staff",
            Description = "A magical staff used by ancient wizards.",
            Type = "Weapon",
            Rarity = "Epic",
            Power = "85",
            Speed = "40",
            Durability = "70",
            MagicalAddons = "Mana Boost"
        }
    );

        }
    }
}

