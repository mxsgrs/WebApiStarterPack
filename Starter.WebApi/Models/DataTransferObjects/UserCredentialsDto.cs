using System.ComponentModel.DataAnnotations;

namespace Starter.WebApi.Models.DataTransferObjects;

public class UserCredentialsDto
{
    public long Id { get; set; }

    [Required]
    public string EmailAddress { get; set; }

    [Required]
    public string HashedPassword { get; set; }

    public string UserRole { get; set; }
}
