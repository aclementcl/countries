using Global.Manager.Entities;
using Global.Manager.Interfaces;

namespace Global.Manager.Services;

public class CityManager : ICityManager
{
    private readonly ICityAccess _cityAccess;

    public CityManager(ICityAccess cityAccess)
    {
        _cityAccess = cityAccess;
    }

    public Task<IReadOnlyList<City>> GetAll()
    {
        return _cityAccess.GetAll();
    }

    public Task<City?> GetById(int id)
    {
        return _cityAccess.GetById(id);
    }

    public Task<City> Create(City city)
    {
        return _cityAccess.Create(city);
    }

    public Task<bool> Update(int id, City city)
    {
        return _cityAccess.Update(id, city);
    }

    public Task<bool> Delete(int id)
    {
        return _cityAccess.Delete(id);
    }
}
