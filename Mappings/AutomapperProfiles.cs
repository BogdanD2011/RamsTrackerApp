using AutoMapper;
using RamsTrackerAPI.Models.Domain;
using RamsTrackerAPI.Models.DTO;

namespace RamsTrackerAPI.Mappings
{
    // Automater package to automatic mapping in between objects (Domain model - DTOs)
    public class AutomapperProfiles : Profile
    {
        
        public AutomapperProfiles()
        {
           
            CreateMap<MS, MSDTO>().ReverseMap();
            CreateMap<AddMSRequestDto, MS>().ReverseMap();
            CreateMap<UpdateMSRequestDto, MS>().ReverseMap();
            CreateMap<AddRaDTO, RA>().ReverseMap();
            CreateMap<RA, RADTO>().ReverseMap();
            CreateMap<Contractor, AddContractorDTO>().ReverseMap();
            CreateMap<Contractor, ContractorDTO>().ReverseMap();
            CreateMap<HsPersonContact, HsPersonContactDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            
        }
    }
}
