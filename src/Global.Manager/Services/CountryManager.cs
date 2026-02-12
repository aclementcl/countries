using Global.Manager.Entities;
using Global.Manager.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Global.Manager.Services;

public class CountryManager : ICountryManager
{
    private readonly ICountryAccess _countryAccess;
    private readonly IMemoryCache _cache;
    private const string AllCountriesCacheKey = "countries:all";

    public CountryManager(ICountryAccess countryAccess, IMemoryCache cache)
    {
        _countryAccess = countryAccess;
        _cache = cache;
    }

    public Task<IReadOnlyList<Country>> GetAll()
    {
        return _cache.GetOrCreateAsync(AllCountriesCacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
            return await _countryAccess.GetAll();
        })!;
    }

    public Task<Country?> GetById(int id)
    {
        var cacheKey = $"countries:{id}";
        return _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
            return await _countryAccess.GetById(id);
        });
    }

    public async Task<Country> Create(Country country)
    {
        var created = await _countryAccess.Create(country);
        _cache.Remove(AllCountriesCacheKey);
        _cache.Set($"countries:{created.Id}", created, TimeSpan.FromSeconds(60));
        return created;
    }

    public async Task<bool> Update(int id, Country country)
    {
        var updated = await _countryAccess.Update(id, country);
        _cache.Remove(AllCountriesCacheKey);
        _cache.Remove($"countries:{id}");
        return updated;
    }

    public async Task<bool> Delete(int id)
    {
        var deleted = await _countryAccess.Delete(id);
        _cache.Remove(AllCountriesCacheKey);
        _cache.Remove($"countries:{id}");
        return deleted;
    }
}
