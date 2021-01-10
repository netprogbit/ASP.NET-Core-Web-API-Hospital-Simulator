using DataLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface ICustomRepository
    {
        Task<IEnumerable<Summary>> GetSummariesAsync();
    }
}