using Global.Manager.Entities;

namespace Global.Manager.Interfaces;

public interface ICountryManager
{
    Task<IReadOnlyList<Country>> GetAll();
}
