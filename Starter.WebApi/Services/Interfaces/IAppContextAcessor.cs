using Starter.WebApi.Models.Authentication;

namespace Starter.WebApi.Services.Interfaces;

public interface IAppContextAcessor
{
    UserClaims UserClaims { get; }
}