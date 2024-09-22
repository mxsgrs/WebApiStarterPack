namespace Starter.WebApi.Services;

public class AddressRepository(ILogger<AddressRepository> logger, StarterContext dbContext) : IAddressRepository
{
    private readonly ILogger<AddressRepository> _logger = logger;
    private readonly StarterContext _dbContext = dbContext;

    /// <summary>
    /// Create a new address
    /// </summary>
    public async Task<Result<Address>> CreateOrUpdate(Address address)
    {
        Address? existingAddress = await _dbContext.Addresses
            .FindAsync(address.AddressLine, address.City, 
                address.ZipCode, address.Country);

        if (existingAddress is null)
        {
            _logger.LogInformation("Creating a new address with {Address}", address);

            await _dbContext.Addresses.AddAsync(address);
        }
        else
        {
            _logger.LogInformation("Updating an address with {Address}", address);

            _dbContext.Entry(existingAddress).CurrentValues.SetValues(address);
        }

        await _dbContext.SaveChangesAsync();

        return Result.Ok(address);
    }

    /// <summary>
    /// Read an existing address
    /// </summary>
    public async Task<Result<Address>> Read(string addressLine, string city, 
        string zipCode, string country)
    {
        Address? address = await _dbContext.Addresses
            .FindAsync(addressLine, city, zipCode, country);

        if (address is null)
        {
            return Result.Fail("Address was not found");
        }

        return Result.Ok(address);
    }
}
