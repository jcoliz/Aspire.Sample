using Aspire.Sample.Data;
using Aspire.Sample.Providers;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions for configuring a data provider from this data source
/// </summary>
public static class ApplicationFeaturesExtensions
{
    /// <summary>
    /// Add data provider services from this data source
    /// </summary>
    /// <param name="services">Target to add into</param>
    /// <returns></returns>
    public static IServiceCollection AddDataProvider(this IServiceCollection services)
    {
        services.AddScoped<IDataProvider, ApplicationDbContext>();
        
        return services;
    }
}
