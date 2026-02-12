using System.Net;
using System.Net.Http.Json;
using Global.Manager.Dtos;
using Xunit;

namespace Global.IntegrationTests;

public class CitiesApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CitiesApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ReturnsOkWithCities()
    {
        var response = await _client.GetAsync("/api/v1/cities");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var cities = await response.Content.ReadFromJsonAsync<List<CityDto>>();

        Assert.NotNull(cities);
        Assert.NotEmpty(cities!);
    }

    [Fact]
    public async Task Update_NonExisting_ReturnsNotFound()
    {
        var update = new UpdateCityDto { Name = "Test City", CountryId = 1 };

        var response = await _client.PutAsJsonAsync("/api/v1/cities/999", update);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
