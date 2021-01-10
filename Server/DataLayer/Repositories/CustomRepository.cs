using DataLayer.DbContexts;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    /// <summary>
    /// Complex queries repository
    /// </summary>
    public class CustomRepository : ICustomRepository
    {
        private readonly HospitalDbContext _db;

        public CustomRepository(HospitalDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Summary>> GetSummariesAsync()
        {
            var summaries = _db.Measurements
                .Join(_db.Devices,
                    m => m.DeviceId,
                    d => d.Id,
                    (m, d) => new { m, d }
                )
                .Join(_db.Patients,
                    md => md.d.PatientId,
                    p => p.Id,
                    (md, p) => new { md, p }
                );

            // !!! This premature materialization is needed in order to use these methods with operators, 
            // because the EF Core 3.1 framework stopped supporting FirstOrDefault and SingleOrDefault functions for IQuerable
            var matSummaries = await summaries.AsNoTracking().ToListAsync();            

            var groupSummaries = matSummaries.GroupBy(mdp => mdp.md.m.DeviceId)                  
                   .Select(g => new Summary
                   {                        
                       PatientId = g.FirstOrDefault().p.Id,
                       PatientName = g.FirstOrDefault().p.Name,
                       DeviceId = g.FirstOrDefault().md.d.Id,
                       DeviceSerialNumber = g.FirstOrDefault().md.d.SerialNumber,
                       DeviceName = g.FirstOrDefault().md.d.Name,
                       CurrentHR = g.SingleOrDefault(mdp => mdp.md.m.Id == mdp.md.d.BufferCurrentPtr).md.m.HR,
                       CurrentRR = g.SingleOrDefault(mdp => mdp.md.m.Id == mdp.md.d.BufferCurrentPtr).md.m.RR,
                       AvgHR = (int)g.Average(mdp => mdp.md.m.HR),
                       AvgRR = (int)g.Average(mdp => mdp.md.m.RR)
                   });

            return groupSummaries;
        }
    }
}
