using Global.Manager.Entities;
using Global.Manager.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Global.Manager.Services;

public class CityManager : ICityManager
{
    private readonly ICityAccess _cityAccess;
    private readonly IMemoryCache _cache;
    private const string AllCitiesCacheKey = "cities:all";
    private static string CountryCitiesCacheKey(int countryId) => $"cities:country:{countryId}";

    public CityManager(ICityAccess cityAccess, IMemoryCache cache)
    {
        _cityAccess = cityAccess;
        _cache = cache;
    }

    public Task<IReadOnlyList<City>> GetAll()
    {
        return _cache.GetOrCreateAsync(AllCitiesCacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
            return await _cityAccess.GetAll();
        })!;
    }

    public Task<City?> GetById(int id)
    {
        var cacheKey = $"cities:{id}";
        return _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
            return await _cityAccess.GetById(id);
        });
    }

    public Task<IReadOnlyList<City>> GetByCountryId(int countryId)
    {
        var cacheKey = CountryCitiesCacheKey(countryId);
        return _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
            return await _cityAccess.GetByCountryId(countryId);
        })!;
    }

    public async Task<City> Create(City city)
    {
        var created = await _cityAccess.Create(city);
        _cache.Remove(AllCitiesCacheKey);
        _cache.Remove(CountryCitiesCacheKey(created.CountryId));
        _cache.Set($"cities:{created.Id}", created, TimeSpan.FromSeconds(60));
        return created;
    }

    public async Task<bool> Update(int id, City city)
    {
        var existing = await _cityAccess.GetById(id);
        var updated = await _cityAccess.Update(id, city);
        _cache.Remove(AllCitiesCacheKey);
        _cache.Remove($"cities:{id}");
        if (existing is not null)
        {
            _cache.Remove(CountryCitiesCacheKey(existing.CountryId));
        }
        _cache.Remove(CountryCitiesCacheKey(city.CountryId));
        return updated;
    }

    public async Task<bool> Delete(int id)
    {
        var existing = await _cityAccess.GetById(id);
        var deleted = await _cityAccess.Delete(id);
        _cache.Remove(AllCitiesCacheKey);
        _cache.Remove($"cities:{id}");
        if (existing is not null)
        {
            _cache.Remove(CountryCitiesCacheKey(existing.CountryId));
        }
        return deleted;
    }
}
