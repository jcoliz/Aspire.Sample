namespace Aspire.Sample.Models;

public record WeatherForecast
{
    public int Id { get; init; }
    public DateOnly Date { get; init; }
    public int TemperatureF {  get; init; }
    public string? Summary { get; init; }
    public int TemperatureC => (int)((TemperatureF - 32) * 0.5556);
}
