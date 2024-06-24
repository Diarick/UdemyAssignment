using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace DataAccess.Data
{
    public class UdemyAssignmentDBContext : DbContext
    {
        public UdemyAssignmentDBContext(DbContextOptions<UdemyAssignmentDBContext> options) : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
    }
}
