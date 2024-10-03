using Aspire.Sample.Models;
using Aspire.Sample.Worker.Api;
using AutoMapper;

namespace Aspire.Sample.Worker.Mapper;

internal class ForecastProfile: Profile
{
    public ForecastProfile()
    {
        CreateMap<GridpointForecastPeriod, WeatherForecast>()
            .ForMember(dest => dest.TemperatureF, opt => opt.MapFrom(src => src.Temperature))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.StartTime.DateTime)))
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.ShortForecast));
    }
}
