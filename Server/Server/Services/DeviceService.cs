using DataLayer.Entities;
using DataLayer.UnitOfWork;
using Microsoft.Extensions.Options;
using Server.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Server.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;

        public DeviceService(IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Device>> FindAllAsync(int patientId)
        {
            return await _unitOfWork.Devices.FindAllAsync(d => d.PatientId == patientId);            
        }

        public async Task<bool> AddAsync(Device device)
        {
            // Check if the device exists with this Id

            Device currDevice = await _unitOfWork.Devices.FindAsync(d => d.Id == device.Id);

            if (currDevice != null)
                return false;

            // Device adding DB transaction
            using (var dbContextTransaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    await _unitOfWork.Devices.CreateAsync(device);
                    await _unitOfWork.SaveAsync();

                    // Create measurement buffer
                    for (int i = 0; i < _appSettings.NumberOfMeasurementInBuffer; i++)
                    {
                        await _unitOfWork.Measurements.CreateAsync(new Measurement { DeviceId = device.Id, HR = 0, RR = 0 });
                    }

                    await _unitOfWork.SaveAsync();

                    // Init buffer pointers
                    var measurements = await _unitOfWork.Measurements.FindAllAsync(m => m.DeviceId == device.Id);
                    device.BufferStartPtr = measurements.OrderBy(m => m.Id).FirstOrDefault().Id;
                    device.BufferCurrentPtr = device.BufferStartPtr + _appSettings.NumberOfMeasurementInBuffer - 1; // Set current pointer to buffer end

                    _unitOfWork.Devices.Update(device);
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
            Device currDevice = await _unitOfWork.Devices.FindAsync(device.Id);
            currDevice.Name = device.Name;
            currDevice.SerialNumber = device.SerialNumber;
            _unitOfWork.Devices.Update(currDevice);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _unitOfWork.Devices.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
