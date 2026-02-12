using Global.Manager.Entities;

namespace Global.Manager.Interfaces;

public interface ICountryAccess
{
    Task<IReadOnlyList<Country>> GetAll();
}
