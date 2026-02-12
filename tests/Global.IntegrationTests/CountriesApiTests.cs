using System.Net;
using System.Net.Http.Json;
using Global.Manager.Dtos;
using Xunit;

namespace Global.IntegrationTests;

public class CountriesApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CountriesApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ReturnsOkWithCountries()
    {
        var response = await _client.GetAsync("/api/v1/countries");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var countries = await response.Content.ReadFromJsonAsync<List<CountryDto>>();

        Assert.NotNull(countries);
        Assert.NotEmpty(countries!);
    }

    [Fact]
    public async Task Create_ThenGetById_ReturnsCreatedCountry()
    {
        var create = new CreateCountryDto { Name = "Peru" };

        var createResponse = await _client.PostAsJsonAsync("/api/v1/countries", create);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var created = await createResponse.Content.ReadFromJsonAsync<CountryDto>();
        Assert.NotNull(created);

        var getResponse = await _client.GetAsync($"/api/v1/countries/{created!.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }
}
