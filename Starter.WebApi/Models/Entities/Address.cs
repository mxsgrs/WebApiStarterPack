namespace Starter.WebApi.Models.Entities;

[PrimaryKey(nameof(AddressLine), nameof(City), nameof(ZipCode), nameof(Country))]
public class Address
{
    public string AddressLine { get; set; } = "";
    public string? AddressSupplement { get; set; }
    public string City { get; set; } = "";
    public string ZipCode { get; set; } = "";
    public string? StateProvince { get; set; }
    public string Country { get; set; } = "";
    public long UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Address")]
    public virtual ICollection<User> Users { get; set; } = [];
}
