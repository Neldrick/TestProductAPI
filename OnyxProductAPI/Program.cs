using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using OnyxProductAPI.Constants;
using OnyxProductAPI.Repositories;
using OnyxProductAPI.Services;
using OnyxProductAPI.Settings;
using OnyxProductAPI.Utils;
using OnyxProductAPI.Utils.Authen;

var builder = WebApplication.CreateBuilder(args);
#if DEBUG
EnvironmentVariableHelper settingEnvironmentVariableHelper = new("/envSettings.json");
settingEnvironmentVariableHelper.SetEnvironmentVariableFromFile();
#endif


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers(); // Add controllers
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); 
builder.Services.AddSingleton<IAuthenSettings, AuthenSettings>();

// Authentication
AuthenSettings authenSettings = new AuthenSettings();

builder.Services.AddCors(p => p.AddPolicy(ProductAPIConstants.CorsName, builder =>
{
    builder.WithOrigins(authenSettings.CorsDomains).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
}));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddScheme<ApiKeyAuthenticationSchemeOption, ApiKeyAuthenticationHandler>
(ApiKeyAuthenticationSchemeOption.DefaultScheme,
    options => { })
.AddMicrosoftIdentityWebApi(
options =>
{
    options.Authority = authenSettings.JWTAuthority;
    options.Audience = authenSettings.JWTAudience;
},
options =>
{
    options.Instance = authenSettings.MsIdInstance;
    options.Authority = authenSettings.MsIdAuthority;
    options.TenantId = authenSettings.MsIdTenantId;
    options.ClientId = authenSettings.MsIdClientId;
});


builder.Services.AddSingleton<IProductRepository, ProductRepository>(); // Register ProductRepository
builder.Services.AddScoped<IProductServices, ProductServices>(); // Register ProductServices

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger middleware
    app.UseSwaggerUI(); // Enable Swagger UI
}

app.UseAuthorization(); // Add authorization middleware

app.MapControllers(); // Map controllers

app.UseCors(ProductAPIConstants.CorsName);
app.UseAuthentication();
app.UseAuthorization();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
