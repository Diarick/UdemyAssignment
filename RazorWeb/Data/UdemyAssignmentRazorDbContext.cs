using Microsoft.EntityFrameworkCore;
using RazorWeb.Model;

namespace RazorWeb.Data
{
    public class UdemyAssignmentRazorDBContext : DbContext
    {
        public UdemyAssignmentRazorDBContext(DbContextOptions<UdemyAssignmentRazorDBContext> options) : base(options)
        {

        }
        public DbSet<Category> categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 1 },
                new Category { Id = 3, Name = "History", DisplayOrder = 1 }
                );
        }
    }
}
