using System.ComponentModel.DataAnnotations;

namespace Starter.WebApi.Models.DataTransferObjects;

public class UserProfileDto
{
    [Required]
    public string FirstName { get; set; } = "";

    [Required]
    public string LastName { get; set; } = "";

    [Required]
    public DateOnly Birthday { get; set; }

    [Required]
    public string Gender { get; set; } = "";

    public string? Position { get; set; }

    [Required]
    public string PersonalPhone { get; set; } = "";

    public string? ProfessionalPhone { get; set; }

    [Required]
    public string PostalAddress { get; set; } = "";

    public string? AddressSupplement { get; set; }

    [Required]
    public string City { get; set; } = "";

    [Required]
    public string ZipCode { get; set; } = "";

    public string? StateProvince { get; set; }

    [Required]
    public string Country { get; set; } = "";
}
