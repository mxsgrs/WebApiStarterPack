namespace Starter.WebApi.Models.Authentication;

public class HashedLoginRequest
{
    public string EmailAddress { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
}

public class PlainLoginRequest
{
    public string EmailAddress { get; set; } = string.Empty;
    public string PlainPassword { get; set; } = string.Empty;
    public bool RememberMe { get; set; } = false;
}

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
}
