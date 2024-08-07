﻿using DataAccess.Data;
using DataAccess.Repositories.IRepositories;
using Model.Models;

namespace DataAccess.Repositories
{
    public class CategoryRepository : BaseTransactRepository<Category>
    {
        private readonly UdemyAssignmentDBContext _dbContext;
        public CategoryRepository(UdemyAssignmentDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public void Update(Category obj)
        {
            _dbContext.Update(obj);
        }
    }
}
