using DataLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface IDeviceService
    {
        Task<IEnumerable<Device>> FindAllAsync(int patientId);
        Task<bool> AddAsync(Device device);
        Task UpdateAsync(Device device);        
        Task RemoveAsync(int id);
    }
}