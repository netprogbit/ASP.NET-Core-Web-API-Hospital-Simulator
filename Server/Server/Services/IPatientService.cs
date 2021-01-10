using DataLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> FindAllAsync();
        Task<IEnumerable<Summary>> GetSummariesAsync();
    }
}