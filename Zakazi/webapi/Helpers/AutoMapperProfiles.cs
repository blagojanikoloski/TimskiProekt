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
            //CreateMap<UserDto, User>()
            //    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<RegisterDto, ZakaziUser>();
            CreateMap<Request, RequestDto>()
                .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.Business.BusinessName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Posts.Sum(post => post.Price)));
            CreateMap<BusinessCreationDto, Business>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
        }
    }
}
