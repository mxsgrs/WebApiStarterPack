namespace Starter.WebApi.Models.DataTransferObjects.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserCredentials, UserCredentialsDto>();
        CreateMap<UserCredentialsDto, UserCredentials>()
            .ForMember(dest => dest.UserProfile, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UserProfile, UserProfileDto>();
        CreateMap<UserProfileDto, UserProfile>()
            .ForMember(dest => dest.UserCredentials, opt => opt.Ignore())
            .ForMember(dest => dest.UserCredentialsId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
