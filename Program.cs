using Microsoft.OpenApi.Models;
using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1", new OpenApiInfo
{
Title = "UserManagementAPI",
Version = "v1",
Description = "A simple User Management API for TechHive Solutions."
});
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "bearer",
    BearerFormat = "Token",
    In = ParameterLocation.Header,
    Description = "Enter the token: techhive-secret-token"
});

options.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>()
    }
});

});

var app = builder.Build();

// Enable Swagger for API testing.
app.UseSwagger();
app.UseSwaggerUI();

// Middleware pipeline
// 1. Error handling
// 2. Authentication
// 3. Request/response logging
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseMiddleware<TokenAuthenticationMiddleware>();

app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.MapGet("/", () => new
{
message = "User Management API is running.",
documentation = "/swagger"
});

app.Run();
