using DataAccess.Repository.IRepository;
using Model.Models;

namespace DataAccess.Repositories.IRepositories
{
    public interface ITransactRepository<T> : IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);
        public void Update(T obj);
    }
}
