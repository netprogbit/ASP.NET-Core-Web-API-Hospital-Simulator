using DataLayer.Entities;
using DataLayer.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Patient>> FindAllAsync()
        {
            return await _unitOfWork.Patients.FindAllAsync();            
        }

        public async Task<IEnumerable<Summary>> GetSummariesAsync()
        {
            return await _unitOfWork.CustomRepository.GetSummariesAsync();
        }
    }
}
