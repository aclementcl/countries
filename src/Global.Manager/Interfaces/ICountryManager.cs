using Global.Manager.Entities;

namespace Global.Manager.Interfaces;

public interface ICountryManager
{
    Task<IReadOnlyList<Country>> GetAll();
    Task<Country?> GetById(int id);
    Task<Country> Create(Country country);
    Task<bool> Update(int id, Country country);
    Task<bool> Delete(int id);
}
