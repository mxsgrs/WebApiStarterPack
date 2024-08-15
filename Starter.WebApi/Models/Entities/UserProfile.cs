using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Starter.WebApi.Models.Entities;

public partial class UserProfile
{
    [Key]
    public long Id { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = "";

    [StringLength(100)]
    public string LastName { get; set; } = "";

    public DateOnly Birthday { get; set; }

    [StringLength(20)]
    public string Gender { get; set; } = "";

    [StringLength(255)]
    public string? Position { get; set; }

    [StringLength(100)]
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "The phone number must be between 10 and 15 digits and may include a leading +.")]
    public string PersonalPhone { get; set; } = "";

    [StringLength(100)]
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "The phone number must be between 10 and 15 digits and may include a leading +.")]
    public string? ProfessionalPhone { get; set; }

    [StringLength(255)]
    public string PostalAddress { get; set; } = "";

    [StringLength(255)]
    public string? AddressSupplement { get; set; }

    [StringLength(100)]
    public string City { get; set; } = "";

    [StringLength(20)]
    public string ZipCode { get; set; } = "";

    [StringLength(100)]
    public string? StateProvince { get; set; }

    [StringLength(100)]
    public string Country { get; set; } = "";

    public long UserCredentialsId { get; set; }

    [ForeignKey("UserCredentialsId")]
    [InverseProperty("UserProfile")]
    public virtual UserCredentials UserCredentials { get; set; } = null!;
}
