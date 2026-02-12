using Global.Manager.Entities;

namespace Global.Manager.Interfaces;

public interface ICityAccess
{
    Task<IReadOnlyList<City>> GetAll();
    Task<City?> GetById(int id);
    Task<City> Create(City city);
    Task<bool> Update(int id, City city);
    Task<bool> Delete(int id);
}
