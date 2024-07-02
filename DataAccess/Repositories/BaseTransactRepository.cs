using DataAccess.Data;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class BaseTransactRepository<T> : ITransactRepository<T> where T : class
    {
        private readonly UdemyAssignmentDBContext _dbContext;
        internal DbSet<T> _dbSet;
        public BaseTransactRepository(UdemyAssignmentDBContext db)
        {
            _dbContext = db;
            this._dbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Update(T obj)
        {
            _dbSet.Update(obj);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
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

        public void Save()
        {
            _dbContext.SaveChanges();
        }

    }
}
