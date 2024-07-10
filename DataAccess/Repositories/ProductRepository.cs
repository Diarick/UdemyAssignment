using DataAccess.Data;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace DataAccess.Repositories
{
    public class ProductRepository : BaseTransactRepository<Product>
    {
        private readonly UdemyAssignmentDBContext _dbContext;
        public ProductRepository(UdemyAssignmentDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
        public IEnumerable<Product> GetAll(string IncludeProperties)
        {
            IEnumerable<Product> Query = _dbSet.Include(IncludeProperties);
            return Query.ToList();
        }
    }
}
