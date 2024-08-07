namespace Starter.WebApi.Models.DataTransferObjects;

public class UserCredentialsDto
{
    public long Id { get; set; }
    public string EmailAddress { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public string UserRole { get; set; } = string.Empty;
}
