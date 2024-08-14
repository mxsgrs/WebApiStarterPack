using System.Net.Http.Json;

namespace Starter.WebApi.IntegrationTests.Facts.Controllers;

public class UserProfileControllerTests
{
    [Fact]
    public async Task CreateOrUpdate_ShouldReturnOk_WhenExistingUserProfileIsUpdated()
    {
        // Arrange
        static void AddUserProfile(StarterContext dbContext)
        {
            UserCredentials userCredentials = new()
            {
                EmailAddress = "testuser@gmail.com",
                HashedPassword = "password123",
                UserRole = "admin"
            };
            dbContext.UserCredentials.Add(userCredentials);
            dbContext.SaveChanges();

            UserProfile userProfile = new()
            {
                FirstName = "Jane",
                LastName = "Doe",
                Birthday = new DateOnly(1992, 2, 2),
                Gender = "Female",
                PersonalPhone = "+1234567891",
                PostalAddress = "456 Main St",
                City = "Othertown",
                ZipCode = "67890",
                Country = "Canada",
                UserCredentialsId = 1
            };
            dbContext.UserProfile.Add(userProfile);
            dbContext.SaveChanges();
        }
        StarterWebApplicationFactory factory = new(AddUserProfile);
        HttpClient client = factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new("Bearer", Auth.Jwt);

        UserProfileDto userProfileDto = new()
        {
            FirstName = "Jane Updated",
            LastName = "Doe Updated",
            Birthday = new DateOnly(1992, 2, 2),
            Gender = "Female",
            PersonalPhone = "+1234567891",
            PostalAddress = "456 Main St",
            City = "Newtown",
            ZipCode = "67890",
            Country = "Canada"
        };

        // Act
        HttpResponseMessage response = await client.PostAsJsonAsync("/api/user-profile/create-or-update", userProfileDto);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrUpdate_ShouldReturnOk_WhenNewUserProfileIsCreated()
    {
        // Arrange
        static void AddUserCredentials(StarterContext dbContext)
        {
            UserCredentials userCredentials = new()
            {
                EmailAddress = "testuser@gmail.com",
                HashedPassword = "password123",
                UserRole = "admin"
            };
            dbContext.UserCredentials.Add(userCredentials);
            dbContext.SaveChanges();
        }
        StarterWebApplicationFactory factory = new(AddUserCredentials);
        HttpClient client = factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new("Bearer", Auth.Jwt);

        UserProfileDto userProfileDto = new()
        {
            FirstName = "Jane",
            LastName = "Doe",
            Birthday = new DateOnly(1992, 2, 2),
            Gender = "Female",
            PersonalPhone = "+1234567891",
            PostalAddress = "456 Main St",
            City = "Newtown",
            ZipCode = "67890",
            Country = "Canada"
        };

        // Act
        HttpResponseMessage response = await client.PostAsJsonAsync("/api/user-profile/create-or-update", userProfileDto);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Read_ShouldReturnNotFound_WhenUserProfileDoesNotExist()
    {
        // Arrange
        static void RemoveUserProfile(StarterContext dbContext)
        {
            ICollection<UserProfile> profiles = [.. dbContext.UserProfile];
            dbContext.UserProfile.RemoveRange(profiles);
            dbContext.SaveChanges();
        }
        StarterWebApplicationFactory factory = new(RemoveUserProfile);
        HttpClient client = factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new("Bearer", Auth.Jwt);

        // Act
        HttpResponseMessage response = await client.GetAsync("/api/user-profile/read");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Read_ShouldReturnOk_WhenUserProfileExists()
    {
        // Arrange
        static void AddUserProfile(StarterContext dbContext)
        {

            UserCredentials userCredentials = new()
            {
                EmailAddress = "testuser@gmail.com",
                HashedPassword = "password123",
                UserRole = "admin"
            };
            dbContext.UserCredentials.Add(userCredentials);
            dbContext.SaveChanges();

            UserProfile userProfile = new()
            {
                FirstName = "Jane",
                LastName = "Doe",
                Birthday = new DateOnly(1992, 2, 2),
                Gender = "Female",
                PersonalPhone = "+1234567891",
                PostalAddress = "456 Main St",
                City = "Othertown",
                ZipCode = "67890",
                Country = "Canada",
                UserCredentialsId = 1
            };
            dbContext.UserProfile.Add(userProfile);
            dbContext.SaveChanges();
        }
        StarterWebApplicationFactory factory = new(AddUserProfile);
        HttpClient client = factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new("Bearer", Auth.Jwt);

        // Act
        HttpResponseMessage response = await client.GetAsync("/api/user-profile/read");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
