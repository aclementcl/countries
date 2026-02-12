using Global.Access.Data;
using Global.Access.Repositories;
using Global.Manager.Interfaces;
using Global.Manager.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Countries", Version = "v1" });
});
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
    await db.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => Results.Redirect("/swagger"))
        .ExcludeFromDescription();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
