using JetBrains.Annotations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MiniInternetMagazin.Models;
using MiniInternetMagazin.Models.GroceryStoreViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniInternetMagazin.Db
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData
                (
                new Category() { CategoryName = "Молочьное", CategoryId = 1 },
                new Category() { CategoryName = "Мясное", CategoryId = 2 },
                new Category() { CategoryName = "Фрукты", CategoryId = 3 },
                new Category() { CategoryName = "Яйцы", CategoryId = 4 },
                new Category() { CategoryName = "Рыбы", CategoryId = 5 },
                new Category() { CategoryName = "Зерно-мучное", CategoryId = 6 }
                );

            modelBuilder.Entity<User>().HasData
                (
                new User() { Id = 1, Roll = "Admin", Login = 1234, Password = "1234" }
                );
        }
    }
}
