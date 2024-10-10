namespace Starter.Store.WebApi.BuildingBlocks.Domain;

public class BadRequestException : Exception
{
    public BadRequestException() : base() { }
    public BadRequestException(string? message) : base(message) { }
}
