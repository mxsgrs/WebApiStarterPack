namespace Starter.WebApi.Models.DataTransferObjects;

public class UserProfileDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly Birthday { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string? Position { get; set; }
    public string PersonalPhone { get; set; } = string.Empty;
    public string? ProfessionalPhone { get; set; }
    public string PostalAddress { get; set; } = string.Empty;
    public string? AddressSupplement { get; set; }
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string? StateProvince { get; set; }
    public string Country { get; set; } = string.Empty;
    public long UserCredentialsId { get; set; }
}
