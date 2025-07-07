using AutoMapper;
using EmployeeListApplication.Core.Models;
using EmployeeListApplication.Server.Dto;

namespace EmployeeListApplication.Server
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeForCreateDto, Employee>().ReverseMap();
            CreateMap<EmployeeForGetDto, Employee>().ReverseMap();
            CreateMap<EmployeeForPatchDto, Employee>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
