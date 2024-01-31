using AutoMapper;

namespace Time.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //mapping exaple with null values ignored
            //CreateMap<UserDto, User>()
            //    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
