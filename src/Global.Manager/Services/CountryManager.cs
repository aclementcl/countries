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
}
