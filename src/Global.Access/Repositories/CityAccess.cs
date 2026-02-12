using Global.Access.Data;
using Global.Manager.Entities;
using Global.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Global.Access.Repositories;

public class CityAccess : ICityAccess
{
    private readonly GlobalDbContext _dbContext;

    public CityAccess(GlobalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<City>> GetAll()
    {
        return await _dbContext.Cities
            .AsNoTracking()
            .OrderBy(city => city.Id)
            .ToListAsync();
    }

    public async Task<City?> GetById(int id)
    {
        return await _dbContext.Cities
            .AsNoTracking()
            .FirstOrDefaultAsync(city => city.Id == id);
    }

    public async Task<City> Create(City city)
    {
        var entity = new City
        {
            Name = city.Name,
            CountryId = city.CountryId
        };

        _dbContext.Cities.Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> Update(int id, City city)
    {
        var existing = await _dbContext.Cities.FirstOrDefaultAsync(c => c.Id == id);
        if (existing is null)
        {
            return false;
        }

        existing.Name = city.Name;
        existing.CountryId = city.CountryId;
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var existing = await _dbContext.Cities.FirstOrDefaultAsync(c => c.Id == id);
        if (existing is null)
        {
            return false;
        }

        _dbContext.Cities.Remove(existing);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
