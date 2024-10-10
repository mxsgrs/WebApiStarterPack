namespace Starter.Store.WebApi.BuildingBlocks.Domain;

public class NotFoundException : Exception
{
    public NotFoundException() : base() { }
    public NotFoundException(string? message) : base(message) { }
}
