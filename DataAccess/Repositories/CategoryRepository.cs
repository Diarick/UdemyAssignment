using DataAccess.Data;
using DataAccess.Repositories.IRepositories;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CategoryRepository : BaseRepository<Category>,ICategoryRepository
    {
        private readonly UdemyAssignmentDBContext _dbContext;
        public CategoryRepository(UdemyAssignmentDBContext dBContext) : base(dBContext) 
        {
            _dbContext = dBContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Category obj)
        {
            _dbContext.Update(obj);
        }
    }
}
