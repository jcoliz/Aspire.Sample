using Aspire.Sample.Models;
using Aspire.Sample.Providers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        modelBuilder.Entity<WeatherForecast>().HasData(
            new WeatherForecast(Id: 1, TemperatureC: 101, Summary:"Seeded", Date: DateOnly.FromDateTime(DateTime.Now.AddDays(-1) )),
            new WeatherForecast(Id: 2, TemperatureC: 102, Summary: "Seeded", Date: DateOnly.FromDateTime(DateTime.Now.AddDays(-2))),
            new WeatherForecast(Id: 3, TemperatureC: 103, Summary: "Seeded", Date: DateOnly.FromDateTime(DateTime.Now.AddDays(-3)))
        );

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
