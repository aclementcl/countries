using Global.Access.Data;
using Global.Manager.Entities;
using Global.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Global.Access.Repositories;

public class CityAccess : ICityAccess
{
    private readonly GlobalDbContext _dbContext;
    private readonly ILogger<CityAccess> _logger;

    public CityAccess(GlobalDbContext dbContext, ILogger<CityAccess> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IReadOnlyList<City>> GetAll()
    {
        try
        {
            return await _dbContext.Cities
                .AsNoTracking()
                .OrderBy(city => city.Id)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get all cities.");
            throw;
        }
    }

    public async Task<City?> GetById(int id)
    {
        try
        {
            return await _dbContext.Cities
                .AsNoTracking()
                .FirstOrDefaultAsync(city => city.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get city by id {CityId}.", id);
            throw;
        }
    }

    public async Task<City> Create(City city)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create city.");
            throw;
        }
    }

    public async Task<bool> Update(int id, City city)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update city {CityId}.", id);
            throw;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete city {CityId}.", id);
            throw;
        }
    }
}
