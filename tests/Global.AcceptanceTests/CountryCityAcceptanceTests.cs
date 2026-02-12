using System.Net;
using System.Net.Http.Json;
using Global.Manager.Dtos;
using Xunit;

namespace Global.AcceptanceTests;

public class CountryCityAcceptanceTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CountryCityAcceptanceTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task FullFlow_CreateCountry_CreateCity_Update_Delete()
    {
        var createCountry = new CreateCountryDto { Name = "Testland" };
        var createCountryResponse = await _client.PostAsJsonAsync("/api/v1/countries", createCountry);
        Assert.Equal(HttpStatusCode.Created, createCountryResponse.StatusCode);
        var createdCountry = await createCountryResponse.Content.ReadFromJsonAsync<CountryDto>();
        Assert.NotNull(createdCountry);

        var createCity = new CreateCityDto { Name = "Test City", CountryId = createdCountry!.Id };
        var createCityResponse = await _client.PostAsJsonAsync("/api/v1/cities", createCity);
        Assert.Equal(HttpStatusCode.Created, createCityResponse.StatusCode);
        var createdCity = await createCityResponse.Content.ReadFromJsonAsync<CityDto>();
        Assert.NotNull(createdCity);

        var updateCity = new UpdateCityDto { Name = "Test City Updated", CountryId = createdCountry.Id };
        var updateResponse = await _client.PutAsJsonAsync($"/api/v1/cities/{createdCity!.Id}", updateCity);
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

        var deleteResponse = await _client.DeleteAsync($"/api/v1/cities/{createdCity.Id}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }
}
