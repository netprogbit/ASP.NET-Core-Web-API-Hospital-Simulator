using Server.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface ISummaryService
    {
        Task<List<SummaryDto>> FindAllAsync();
    }
}