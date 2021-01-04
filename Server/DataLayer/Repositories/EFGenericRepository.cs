using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        DbContext _db;
        DbSet<TEntity> _dbSet;

        public EFGenericRepository(DbContext db)
        {
            _db = db;
            _dbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task CreateAsync(TEntity item)
        {
            await _dbSet.AddAsync(item);
        }
        public void Update(TEntity item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
        public bool Remove(int id)
        {
            var product = _dbSet.Find(id);

            if (product == null)
                return false;

            _dbSet.Remove(product);
            return true;
        }
    }
}
