using Global.Manager.Entities;
using Global.Manager.Interfaces;

namespace Global.Manager.Services;

public class CountryManager : ICountryManager
{
    private readonly ICountryAccess _countryAccess;

    public CountryManager(ICountryAccess countryAccess)
    {
        _countryAccess = countryAccess;
    }

    public Task<IReadOnlyList<Country>> GetAll()
    {
        return _countryAccess.GetAll();
    }

    public Task<Country?> GetById(int id)
    {
        return _countryAccess.GetById(id);
    }

    public Task<Country> Create(Country country)
    {
        return _countryAccess.Create(country);
    }

    public Task<bool> Update(int id, Country country)
    {
        return _countryAccess.Update(id, country);
    }

    public Task<bool> Delete(int id)
    {
        return _countryAccess.Delete(id);
    }
}
