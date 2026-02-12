using Global.Manager.Entities;
using Global.Manager.Interfaces;
using Global.Manager.Services;
using Moq;
using Xunit;

namespace Global.UnitTests.Managers;

public class CityManagerTests
{
    [Fact]
    public async Task GetById_ReturnsCityFromAccess()
    {
        var access = new Mock<ICityAccess>();
        var expected = new City { Id = 5, Name = "Santiago", CountryId = 3 };
        access.Setup(a => a.GetById(5)).ReturnsAsync(expected);

        var manager = new CityManager(access.Object);

        var result = await manager.GetById(5);

        Assert.NotNull(result);
        Assert.Equal(expected.Id, result!.Id);
        access.Verify(a => a.GetById(5), Times.Once);
    }

    [Fact]
    public async Task Delete_DelegatesToAccess()
    {
        var access = new Mock<ICityAccess>();
        access.Setup(a => a.Delete(7)).ReturnsAsync(true);

        var manager = new CityManager(access.Object);

        var result = await manager.Delete(7);

        Assert.True(result);
        access.Verify(a => a.Delete(7), Times.Once);
    }
}
