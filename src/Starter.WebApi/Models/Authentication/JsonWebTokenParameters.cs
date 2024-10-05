namespace Starter.WebApi.Models.Authentication;

public class JsonWebTokenParameters
{
    public string Issuer { get; set; } = "";
    public string Audience { get; set; } = "";
    public string Key { get; set; } = "";
}
