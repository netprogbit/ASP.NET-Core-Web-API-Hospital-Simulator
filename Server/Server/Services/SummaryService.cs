using DataLayer.Entities;
using DataLayer.UnitOfWork;
using Server.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly IUnitOfWork<Patient> _patientUnitOfWork;
        private readonly IUnitOfWork<Device> _deviceUnitOfWork;
        private readonly IUnitOfWork<Measurement> _measurementUnitOfWork;

        public SummaryService(IUnitOfWork<Patient> patientUnitOfWork, IUnitOfWork<Device> deviceUnitOfWork, IUnitOfWork<Measurement> measurementUnitOfWork)
        {
            _patientUnitOfWork = patientUnitOfWork;
            _deviceUnitOfWork = deviceUnitOfWork;
            _measurementUnitOfWork = measurementUnitOfWork;
        }

        public async Task<List<SummaryDto>> FindAllAsync()
        {
            var patients = await _patientUnitOfWork.Entities.FindAllAsync();
            return patients.Select(p => new SummaryDto { PatientId = p.Id, PatientName = p.Name }).ToList();
        }
    }
}
