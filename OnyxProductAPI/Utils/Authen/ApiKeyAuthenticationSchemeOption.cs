using Microsoft.AspNetCore.Authentication;
using System;

namespace OnyxProductAPI.Utils.Authen;

public class ApiKeyAuthenticationSchemeOption : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "ApiKeyScheme";

    public string TokenHeaderName { get; set; } = "X-API-KEY";

    public TimeProvider TimeProvider { get; set; } = TimeProvider.System;
}

