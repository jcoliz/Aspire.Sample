using Aspire.Sample.Models;
using Aspire.Sample.Providers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aspire.Sample.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IDataProvider 
{
    #region Data

    public DbSet<WeatherForecast> WeatherForecasts
    {
        get; set;
    }

    #endregion

    #region Query Builders

    IQueryable<T> IDataProvider.Get<T>() 
        => base.Set<T>();

    #endregion

    #region Modifiers

    void IDataProvider.Add(object item) 
        => base.Add(item);
    
    #endregion

    #region Query Runners

    Task<List<T>> IDataProvider.ToListNoTrackingAsync<T>(IQueryable<T> query)    
        => query.AsNoTracking().ToListAsync(); 

    Task<List<T>> IDataProvider.ToListAsync<T>(IQueryable<T> query) 
        => query.ToListAsync();

    #endregion

}
