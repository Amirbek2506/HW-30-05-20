using JetBrains.Annotations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MiniInternetMagazin.Models.GroceryStoreViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniInternetMagazin.Db
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData
                (
                new Category() { CategoryName="Молочьное",CategoryId=1},
                new Category() { CategoryName="Мясное", CategoryId = 2 },
                new Category() { CategoryName="Фрукты", CategoryId = 3},
                new Category() { CategoryName="Яйцы", CategoryId = 4 },
                new Category() { CategoryName="Рыбы", CategoryId = 5 },
                new Category() { CategoryName= "Зерно-мучное", CategoryId = 6 }
                );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqliteConn = new SqliteConnection(@"DataSource = DataBaseGroceryStore.db");
            optionsBuilder.UseSqlite(sqliteConn);
        }
    }
}
