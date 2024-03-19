using AutoMapper;
using webapi.Domain.DTOs;
using webapi.Domain.Models;

namespace Time.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //mapping exaple with null values ignored
            CreateMap<RegisterDto, ZakaziUser>();
            //CreateMap<UserDto, User>()
            //    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
