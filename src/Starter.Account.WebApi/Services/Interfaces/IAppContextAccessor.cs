namespace Starter.Account.WebApi.Services.Interfaces;

public interface IAppContextAccessor
{
    UserClaims UserClaims { get; }
}