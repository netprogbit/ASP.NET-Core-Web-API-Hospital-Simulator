using AutoMapper;
using DataLayer.Entities;
using Server.DTOs;

namespace Server.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Device, DeviceDto>();
            CreateMap<DeviceDto, Device>();
            CreateMap<Patient, PatientDto>();
            CreateMap<PatientDto, Patient>();
        }
    }
}
