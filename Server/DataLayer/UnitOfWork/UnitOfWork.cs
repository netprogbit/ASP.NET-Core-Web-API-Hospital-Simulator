using DataLayer.DbContexts;
using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace DataLayer.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly HospitalDbContext _db;

        public UnitOfWork(HospitalDbContext db)
        {
            _db = db;
        }

        private IGenericRepository<Patient> _patientRepository;
        public IGenericRepository<Patient> Patients => _patientRepository ?? (_patientRepository = new EFGenericRepository<Patient>(_db));

        private IGenericRepository<Device> _deviceRepository;
        public IGenericRepository<Device> Devices => _deviceRepository ?? (_deviceRepository = new EFGenericRepository<Device>(_db));

        private IGenericRepository<Measurement> _measurementRepository;
        public IGenericRepository<Measurement> Measurements => _measurementRepository ?? (_measurementRepository = new EFGenericRepository<Measurement>(_db));

        private ICustomRepository _customRepository;
        public ICustomRepository CustomRepository => _customRepository ?? (_customRepository = new CustomRepository(_db));

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
