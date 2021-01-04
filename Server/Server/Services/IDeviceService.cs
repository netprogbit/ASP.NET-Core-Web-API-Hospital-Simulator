using DataLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface IDeviceService
    {
        Task<bool> AddAsync(Device device);
        Task UpdateAsync(Device device);
        Task<List<Device>> FindAllAsync();
        Task RemoveAsync(int id);
    }
}