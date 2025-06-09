namespace OnyxProductAPI.Settings;

public class AuthenSettings : IAuthenSettings
{
    public string APIKey { get; } = string.Empty;
    public string JWTAuthority { get; }
    public string JWTAudience { get; }
    public string MsIdInstance { get; }
    public string MsIdAuthority { get; }
    public string MsIdTenantId { get; }
    public string MsIdClientId { get; }
    public string[] CorsDomains { get; }

    public AuthenSettings()
    {
        APIKey = Environment.GetEnvironmentVariable("APIKey") ?? throw new ArgumentNullException("APIKey, please read the README file for more information");
        JWTAuthority = Environment.GetEnvironmentVariable("JWTAuthority") ?? throw new ArgumentNullException("JWTAuthority, please read the README file for more information");
        JWTAudience = Environment.GetEnvironmentVariable("JWTAudience") ?? throw new ArgumentNullException("JWTAudience, please read the README file for more information");
        MsIdInstance = Environment.GetEnvironmentVariable("MsIdInstance") ?? throw new ArgumentNullException("MsIdInstance, please read the README file for more information");
        MsIdAuthority = Environment.GetEnvironmentVariable("MsIdAuthority") ?? throw new ArgumentNullException("MsIdAuthority, please read the README file for more information");
        MsIdTenantId = Environment.GetEnvironmentVariable("MsIdTenantId") ?? throw new ArgumentNullException("MsIdTenantId, please read the README file for more information");
        MsIdClientId = Environment.GetEnvironmentVariable("MsIdClientId") ?? throw new ArgumentNullException("MsIdClientId, please read the README file for more information");
        CorsDomains = (Environment.GetEnvironmentVariable("CorsDomain") ?? throw new ArgumentNullException("CorsDomain, please read the README file for more information")).Split(",");
    }

}