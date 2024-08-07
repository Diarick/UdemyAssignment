﻿using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly UdemyAssignmentDBContext _dbContext;
        internal DbSet<T> _dbSet;
        public BaseRepository(UdemyAssignmentDBContext db)
        {
            _dbContext = db;
            this._dbSet = _dbContext.Set<T>();
        }
        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            IQueryable<T> Query = _dbSet;
            Query = Query.Where(filter);
            return Query.FirstOrDefault();
        }
        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> Query = _dbSet;
            return Query.ToList();
        }
    }
}
