using Global.Access.Data;
using Global.Manager.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Global.AcceptanceTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _databaseName = $"AcceptanceDb_{Guid.NewGuid()}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            var dbContextOptions = services
                .Where(d => d.ServiceType == typeof(DbContextOptions<GlobalDbContext>))
                .ToList();
            foreach (var descriptor in dbContextOptions)
            {
                services.Remove(descriptor);
            }

            var dbContextConfigs = services
                .Where(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<GlobalDbContext>))
                .ToList();
            foreach (var descriptor in dbContextConfigs)
            {
                services.Remove(descriptor);
            }

            var dbContexts = services
                .Where(d => d.ServiceType == typeof(GlobalDbContext))
                .ToList();
            foreach (var descriptor in dbContexts)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<GlobalDbContext>(options =>
                options.UseInMemoryDatabase(_databaseName));
        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = builder.Build();

        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<GlobalDbContext>();
        db.Database.EnsureCreated();

        if (!db.Countries.Any())
        {
            db.Countries.Add(new Country { Id = 1, Name = "Paraguay" });
            db.Cities.Add(new City { Id = 1, Name = "Asuncion", CountryId = 1 });
            db.SaveChanges();
        }

        host.Start();
        return host;
    }
}
