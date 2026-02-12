using Global.Manager.Entities;
using Global.Manager.Interfaces;
using Global.Manager.Services;
using Moq;
using Xunit;

namespace Global.UnitTests.Managers;

public class CountryManagerTests
{
    [Fact]
    public async Task GetAll_ReturnsCountriesFromAccess()
    {
        var access = new Mock<ICountryAccess>();
        var expected = new List<Country>
        {
            new() { Id = 1, Name = "Chile" },
            new() { Id = 2, Name = "Argentina" }
        };
        access.Setup(a => a.GetAll()).ReturnsAsync(expected);

        var manager = new CountryManager(access.Object);

        var result = await manager.GetAll();

        Assert.Same(expected, result);
        access.Verify(a => a.GetAll(), Times.Once);
    }

    [Fact]
    public async Task Create_DelegatesToAccess()
    {
        var access = new Mock<ICountryAccess>();
        var input = new Country { Name = "Peru" };
        var created = new Country { Id = 10, Name = "Peru" };
        access.Setup(a => a.Create(input)).ReturnsAsync(created);

        var manager = new CountryManager(access.Object);

        var result = await manager.Create(input);

        Assert.Equal(created.Id, result.Id);
        Assert.Equal(created.Name, result.Name);
        access.Verify(a => a.Create(input), Times.Once);
    }
}
