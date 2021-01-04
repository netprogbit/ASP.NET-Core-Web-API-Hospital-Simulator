using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace DataLayer.UnitOfWork
{
    public interface IUnitOfWork<TEntity> where TEntity : class
    {
        IGenericRepository<TEntity> Entities { get; }
        IDbContextTransaction BeginTransaction();
        void Dispose();
        Task SaveAsync();
    }
}
