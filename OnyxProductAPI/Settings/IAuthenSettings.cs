namespace OnyxProductAPI.Settings;

public interface IAuthenSettings
{
    string APIKey { get; }
    string JWTAuthority { get; }
    string JWTAudience { get; }
    string MsIdInstance { get; }
    string MsIdAuthority { get; }
    string MsIdTenantId { get; }
    string MsIdClientId { get; }
    string[] CorsDomains { get; }

}