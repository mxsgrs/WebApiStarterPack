namespace Starter.WebApi.Models.DataTransferObjects.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserCredentials, UserCredentialsDto>();
        CreateMap<UserCredentialsDto, UserCredentials>()
            .ForMember(dest => dest.UserProfile, opt => opt.Ignore());

        CreateMap<UserProfile, UserProfileDto>();
        CreateMap<UserProfileDto, UserProfile>()
            .ForMember(dest => dest.UserCredentials, opt => opt.Ignore());
    }
}
