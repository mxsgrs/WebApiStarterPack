using System.Text.Json.Serialization;

namespace Starter.WebApi.Models.DataTransferObjects;

public class UserDto
{
    public string EmailAddress { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly Birthday { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender Gender { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Role Role { get; set; }

    public string Phone { get; set; } = "";
    public AddressDto? Address { get; set; }
}
