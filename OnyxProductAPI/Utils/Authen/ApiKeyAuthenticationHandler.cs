using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using OnyxProductAPI.Settings;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace OnyxProductAPI.Utils.Authen;

public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationSchemeOption>
{
    private IHttpContextAccessor _httpContextAccessor;
    private IAuthenSettings _authenSettings;
    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<ApiKeyAuthenticationSchemeOption> options,
        ILoggerFactory logger, UrlEncoder encoder,
        IHttpContextAccessor httpContextAccessor,
        IAuthenSettings authenSettings)
        : base(options, logger, encoder)
    {
        _httpContextAccessor = httpContextAccessor;
        _authenSettings = authenSettings;
    }
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        //check header first
        if (!Request.Headers
            .Any(h => string.Equals(h.Key, Options.TokenHeaderName, StringComparison.OrdinalIgnoreCase)))
        {
            return AuthenticateResult.Fail($"Missing header: {Options.TokenHeaderName}");
        }

        //get the header and validate
        string token = Request
            .Headers[Options.TokenHeaderName]!;

        //usually, this is where you decrypt a token and/or lookup a database.
        if (token != _authenSettings.APIKey)
        {
            return AuthenticateResult
                .Fail($"Invalid token.");
        }

        var claimsIdentity = new ClaimsIdentity
            (new List<Claim>
            {
                    new Claim("Name", "SuperUser", ClaimValueTypes.String)
            }, Scheme.Name);
        var claimsPrincipal = new ClaimsPrincipal
            (claimsIdentity);

        _httpContextAccessor.HttpContext.User = claimsPrincipal;

        return AuthenticateResult.Success
            (new AuthenticationTicket(claimsPrincipal,
            Scheme.Name));

    }
}

