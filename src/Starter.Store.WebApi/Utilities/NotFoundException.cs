namespace Starter.Store.WebApi.Utilities;

public class NotFoundException : Exception
{
    public NotFoundException() : base() { }
    public NotFoundException(string? message) : base(message) { }
}
