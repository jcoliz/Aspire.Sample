using Aspire.Sample.Models;
using Aspire.Sample.Providers;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Aspire.Sample.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IDataProvider 
{
    #region Data

    public DbSet<WeatherForecast> WeatherForecasts
    {
        get; set;
    }

    #endregion

    #region Model Building

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
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
