using Global.Access.Repositories;
using Global.Manager.Interfaces;
using Global.Manager.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Countries", Version = "v1" });
});
builder.Services.AddScoped<ICountryAccess, CountryAccess>();
builder.Services.AddScoped<ICountryManager, CountryManager>();

var app = builder.Build();

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
