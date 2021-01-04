using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task<TEntity> FindAsync(int id);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task CreateAsync(TEntity item);
        void Update(TEntity item);
        bool Remove(int id);
    }
}
