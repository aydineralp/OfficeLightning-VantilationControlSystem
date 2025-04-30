using CRUDify_API.Repositories.Abstract;
using CRUDify_API.Repositories.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.Identity.Client;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Azure AD yapılandırmasını al
var azureAdConfig = builder.Configuration.GetSection("AzureAd");
string tenantId = azureAdConfig["TenantId"];
string clientId = azureAdConfig["ClientId"];
string authority = $"https://login.microsoftonline.com/{tenantId}";

// Public Client (Kullanıcı kimlik doğrulama için)
var publicAuthApp = PublicClientApplicationBuilder.Create(clientId)
    .WithAuthority(new Uri(authority))
    .WithRedirectUri("http://localhost")  // Public Client için gerekli
    .Build();

builder.Services.AddSingleton<IPublicClientApplication>(publicAuthApp);
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoomRepository, UserRoomRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LightControl API",
        Version = "v1",
        Description = "MSAL.NET ile email & şifre doğrulama API"
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();
app.UseCors("AllowAllOrigins");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LightControl API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapControllers();
app.MapGet("/", () => "API is running. Use '/swagger' to test API.");

app.Run();