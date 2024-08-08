using System.ComponentModel.DataAnnotations;

namespace Starter.WebApi.Models.DataTransferObjects;

public class UserProfileDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateOnly Birthday { get; set; }

    [Required]
    public string Gender { get; set; } = string.Empty;

    public string? Position { get; set; }

    [Required]
    public string PersonalPhone { get; set; } = string.Empty;

    public string? ProfessionalPhone { get; set; }

    [Required]
    public string PostalAddress { get; set; } = string.Empty;

    public string? AddressSupplement { get; set; }

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string ZipCode { get; set; } = string.Empty;

    public string? StateProvince { get; set; }

    [Required]
    public string Country { get; set; } = string.Empty;
}
