using DataLayer.Entities;
using DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Server.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IUnitOfWork<Device> _unitOfWork;

        public DeviceService(IUnitOfWork<Device> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Device>> FindAllAsync()
        {
            IEnumerable<Device> devices = await _unitOfWork.Entities.FindAllAsync();
            return devices.ToList();
        }

        public async Task<bool> AddAsync(Device device)
        {
            // Check if the device exists with this Id

            Device currDevice = await _unitOfWork.Entities.FindAsync(d => d.Id == device.Id);

            if (currDevice != null)
                return false;

            // Device adding DB transaction
            using (var dbContextTransaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    await _unitOfWork.Entities.CreateAsync(device);
                    await _unitOfWork.SaveAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback(); // Rollbacking DB      
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }

            return true;
        }

        public async Task UpdateAsync(Device device)
        {            
            // Device updating DB transaction
            using (var dbContextTransaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    Device currDevice = await _unitOfWork.Entities.FindAsync(device.Id);
                    currDevice.Name = device.Name;
                    currDevice.SerialNumber = device.SerialNumber;
                    _unitOfWork.Entities.Update(currDevice);
                    await _unitOfWork.SaveAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback(); // Rollbacking DB       
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }
        }

        public async Task RemoveAsync(int id)
        {
            // User deleting DB transaction
            using (var dbContextTransaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    _unitOfWork.Entities.Remove(id);
                    await _unitOfWork.SaveAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback(); // Rollbacking DB       
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }
        }
    }
}
