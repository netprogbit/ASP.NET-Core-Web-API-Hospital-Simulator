using DataLayer.DbContexts;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace DataLayer.UnitOfWork
{
    public class UnitOfWork<TEntity> : IDisposable, IUnitOfWork<TEntity> where TEntity : class
    {
        private readonly HospitalDbContext _db;

        public UnitOfWork(HospitalDbContext db)
        {
            _db = db;
        }

        private IGenericRepository<TEntity> _genericRepository;
        public IGenericRepository<TEntity> Entities => _genericRepository ?? (_genericRepository = new EFGenericRepository<TEntity>(_db));

        public IDbContextTransaction BeginTransaction()
        {
            return _db.Database.BeginTransaction();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }        

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _db.Dispose();
            }

            _disposed = true;
        }
    }
}
