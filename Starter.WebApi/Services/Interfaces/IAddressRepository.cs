namespace Starter.WebApi.Services.Interfaces;

public interface IAddressRepository
{
    Task<Result<Address>> CreateOrUpdate(Address address);
    Task<Result<Address>> Read(string addressLine, string city, string zipCode, string country);
}