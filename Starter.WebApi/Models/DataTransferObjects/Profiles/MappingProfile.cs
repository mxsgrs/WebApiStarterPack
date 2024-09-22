namespace Starter.WebApi.Models.DataTransferObjects.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();

        CreateMap<Address, AddressDto>();
        CreateMap<AddressDto, Address>();
    }
}