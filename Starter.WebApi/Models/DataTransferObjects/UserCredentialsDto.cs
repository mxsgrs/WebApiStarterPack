using System.ComponentModel.DataAnnotations;

namespace Starter.WebApi.Models.DataTransferObjects;

public class UserCredentialsDto
{
    [Required]
    public string EmailAddress { get; set; } = string.Empty;

    [Required]
    public string HashedPassword { get; set; } = string.Empty;

    public string UserRole { get; set; } = string.Empty;
}
