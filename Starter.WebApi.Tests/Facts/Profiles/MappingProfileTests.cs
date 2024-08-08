namespace Starter.WebApi.Tests.Facts.Mapping;

public class MappingProfileTests
{
    private readonly IMapper _mapper;

    public MappingProfileTests()
    {
        MapperConfiguration mappingConfig = new(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = mappingConfig.CreateMapper();
    }

    [Fact]
    public void MappingConfiguration_ShouldBeValid()
    {
        // Act & Assert
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Fact]
    public void Should_MapUserCredentialsToUserCredentialsDto()
    {
        // Arrange
        UserCredentials userCredentials = new()
        {
            Id = 1,
            EmailAddress = "test@example.com",
            HashedPassword = "hashedpassword",
            UserRole = "admin"
        };

        // Act
        UserCredentialsDto userCredentialsDto = _mapper.Map<UserCredentialsDto>(userCredentials);

        // Assert
        Assert.Equal(userCredentials.Id, userCredentialsDto.Id);
        Assert.Equal(userCredentials.EmailAddress, userCredentialsDto.EmailAddress);
        Assert.Equal(userCredentials.HashedPassword, userCredentialsDto.HashedPassword);
        Assert.Equal(userCredentials.UserRole, userCredentialsDto.UserRole);
    }

    [Fact]
    public void Should_MapUserProfileToUserProfileDto()
    {
        // Arrange
        UserProfile userProfile = new()
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Birthday = new DateOnly(1990, 5, 20),
            Gender = "Male",
            Position = "Software Engineer",
            PersonalPhone = "+1234567890",
            ProfessionalPhone = "+1987654321",
            PostalAddress = "123 Main St",
            AddressSupplement = "Apt 4B",
            City = "New York",
            ZipCode = "10001",
            StateProvince = "NY",
            Country = "USA",
            UserCredentialsId = 123,
        };

        // Act
        UserProfileDto dto = _mapper.Map<UserProfileDto>(userProfile);

        // Assert
        Assert.Equal(userProfile.Id, dto.Id);
        Assert.Equal(userProfile.FirstName, dto.FirstName);
        Assert.Equal(userProfile.LastName, dto.LastName);
        Assert.Equal(userProfile.Birthday, dto.Birthday);
        Assert.Equal(userProfile.Gender, dto.Gender);
        Assert.Equal(userProfile.Position, dto.Position);
        Assert.Equal(userProfile.PersonalPhone, dto.PersonalPhone);
        Assert.Equal(userProfile.ProfessionalPhone, dto.ProfessionalPhone);
        Assert.Equal(userProfile.PostalAddress, dto.PostalAddress);
        Assert.Equal(userProfile.AddressSupplement, dto.AddressSupplement);
        Assert.Equal(userProfile.City, dto.City);
        Assert.Equal(userProfile.ZipCode, dto.ZipCode);
        Assert.Equal(userProfile.StateProvince, dto.StateProvince);
        Assert.Equal(userProfile.Country, dto.Country);
        Assert.Equal(userProfile.UserCredentialsId, dto.UserCredentialsId);
    }
}

