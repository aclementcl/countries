using Global.Access.Data;
using Global.Access.Repositories;
using Global.Host.Auth;
using Global.Manager.Interfaces;
using Global.Manager.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Countries", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter a valid JWT token.",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme { Reference = securityScheme.Reference }, Array.Empty<string>() }
    });
});
if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = TestAuthHandler.SchemeName;
        options.DefaultChallengeScheme = TestAuthHandler.SchemeName;
    }).AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions, TestAuthHandler>(
        TestAuthHandler.SchemeName, _ => { });
}
else
{
    var jwtKey = builder.Configuration["Jwt:Key"] ?? "dev-key-change-me";
    var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "Global.Api";
    var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "Global.Api";

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
}

builder.Services.AddAuthorization();

builder.Services.AddDbContext<GlobalDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()));
builder.Services.AddScoped<ICountryAccess, CountryAccess>();
builder.Services.AddScoped<ICountryManager, CountryManager>();
builder.Services.AddScoped<ICityAccess, CityAccess>();
builder.Services.AddScoped<ICityManager, CityManager>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GlobalDbContext>();
    if (db.Database.IsRelational())
    {
        await db.Database.MigrateAsync();
    }
    else
    {
        await db.Database.EnsureCreatedAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => Results.Redirect("/swagger"))
        .ExcludeFromDescription();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
