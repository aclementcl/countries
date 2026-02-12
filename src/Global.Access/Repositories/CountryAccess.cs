using Global.Manager.Entities;
using Global.Manager.Interfaces;

namespace Global.Access.Repositories;

public class CountryAccess : ICountryAccess
{
    private static readonly IReadOnlyList<Country> Countries =
    [
        new Country { Id = 1, Name = "Chile" },
        new Country { Id = 2, Name = "Argentina" }
    ];

    public Task<IReadOnlyList<Country>> GetAll()
    {
        return Task.FromResult(Countries);
    }
}
