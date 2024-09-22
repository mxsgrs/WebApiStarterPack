namespace Starter.WebApi.Models.Entities;

public class Address
{
    [Key]
    public long Id { get; set; }
    public string AddressLine { get; set; } = "";
    public string? AddressSupplement { get; set; }
    public string City { get; set; } = "";
    public string ZipCode { get; set; } = "";
    public string? StateProvince { get; set; }
    public string Country { get; set; } = "";

    [InverseProperty("Address")]
    public virtual ICollection<User> Users { get; set; } = [];
}
