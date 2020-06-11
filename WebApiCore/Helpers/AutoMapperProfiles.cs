using AutoMapper;
using WebApiCore.Dtos;
using WebApiCore.Models;

namespace WebApiCore.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<FileDescriptionDto, FileDescription>();
            CreateMap<FileDescriptionDto, ImportFileDescription>();
            CreateMap<FileDescription, FileDescriptionForResultDto>().ForMember(dest => dest.Name, src => src.MapFrom(opt => opt.FileName));
            CreateMap<UserForRegisterDto, User>().ReverseMap();
            CreateMap<User, UserForListDto>().ForMember(dest => dest.Age, src => src.MapFrom(opt => opt.DateOfBirth.CalculateAge())).ReverseMap();
        }
    }
}
