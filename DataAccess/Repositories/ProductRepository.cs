using DataAccess.Data;
using DataAccess.Repositories.IRepositories;
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
    }
}
