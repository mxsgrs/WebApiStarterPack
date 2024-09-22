namespace Starter.WebApi.Models.DataTransferObjects;

public class UserDto
{
    public string EmailAddress { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly Birthday { get; set; }
    public Gender Gender { get; set; }
    public Role Role { get; set; }
    public string Phone { get; set; } = "";
    public AddressDto? Address { get; set; }
}
