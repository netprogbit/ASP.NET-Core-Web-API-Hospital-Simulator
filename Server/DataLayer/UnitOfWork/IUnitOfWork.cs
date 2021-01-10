using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace DataLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Patient> Patients { get; }
        IGenericRepository<Device> Devices { get; }
        IGenericRepository<Measurement> Measurements { get; }
        ICustomRepository CustomRepository { get; }
        IDbContextTransaction BeginTransaction();
        void Dispose();
        Task SaveAsync();
    }
}
