using Global.Access.Data;
using Global.Manager.Entities;
using Global.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Global.Access.Repositories;

public class CountryAccess : ICountryAccess
{
    private readonly GlobalDbContext _dbContext;

    public CountryAccess(GlobalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Country>> GetAll()
    {
        return await _dbContext.Countries
            .AsNoTracking()
            .OrderBy(country => country.Id)
            .ToListAsync();
    }
}
