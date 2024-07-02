using Microsoft.EntityFrameworkCore;
using Model.Models;
using System.Net.Http.Headers;

namespace DataAccess.Data
{
    public class UdemyAssignmentDBContext : DbContext
    {
        public UdemyAssignmentDBContext(DbContextOptions<UdemyAssignmentDBContext> options) : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Title = "Fortune of Time", Description = "Desc 1", Author = "DN", ISBN = "DN000201203", ListPrice = 99000, Price = 90000, Price50 = 85000, Price100 = 80000, CategoryID=1, ImageUrl=""},
                new Product { Id = 2, Title = "Dark Skies", Description = "Desc 2", Author = "Bukan DN", ISBN = "DN050241211", ListPrice = 80000, Price = 75000, Price50 = 70000, Price100 = 60000, CategoryID=1, ImageUrl="" }
            );
        }
    }
}
