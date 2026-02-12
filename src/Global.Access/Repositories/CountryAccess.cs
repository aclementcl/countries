using Global.Access.Data;
using Global.Manager.Entities;
using Global.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Global.Access.Repositories;

public class CountryAccess : ICountryAccess
{
    private readonly GlobalDbContext _dbContext;
    private readonly ILogger<CountryAccess> _logger;

    public CountryAccess(GlobalDbContext dbContext, ILogger<CountryAccess> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IReadOnlyList<Country>> GetAll()
    {
        try
        {
            return await _dbContext.Countries
                .AsNoTracking()
                .OrderBy(country => country.Id)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get all countries.");
            throw;
        }
    }

    public async Task<Country?> GetById(int id)
    {
        try
        {
            return await _dbContext.Countries
                .AsNoTracking()
                .FirstOrDefaultAsync(country => country.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get country by id {CountryId}.", id);
            throw;
        }
    }

    public async Task<Country> Create(Country country)
    {
        try
        {
            var entity = new Country
            {
                Name = country.Name
            };

            _dbContext.Countries.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create country.");
            throw;
        }
    }

    public async Task<bool> Update(int id, Country country)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update country {CountryId}.", id);
            throw;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete country {CountryId}.", id);
            throw;
        }
    }
}
