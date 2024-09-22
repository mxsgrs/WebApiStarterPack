namespace Starter.WebApi.Models.DataTransferObjects;

public class AddressDto
{
    public string AddressLine { get; set; } = "";
    public string? AddressSupplement { get; set; }
    public string City { get; set; } = "";
    public string ZipCode { get; set; } = "";
    public string? StateProvince { get; set; }
    public string Country { get; set; } = "";
}

