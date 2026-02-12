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

    public async Task<Country?> GetById(int id)
    {
        return await _dbContext.Countries
            .AsNoTracking()
            .FirstOrDefaultAsync(country => country.Id == id);
    }

    public async Task<Country> Create(Country country)
    {
        var entity = new Country
        {
            Name = country.Name
        };

        _dbContext.Countries.Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> Update(int id, Country country)
    {
        var existing = await _dbContext.Countries.FirstOrDefaultAsync(c => c.Id == id);
        if (existing is null)
        {
            return false;
        }

        existing.Name = country.Name;
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var existing = await _dbContext.Countries.FirstOrDefaultAsync(c => c.Id == id);
        if (existing is null)
        {
            return false;
        }

        _dbContext.Countries.Remove(existing);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
