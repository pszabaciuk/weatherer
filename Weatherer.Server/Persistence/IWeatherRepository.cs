using Weatherer.Server.Domain;
using Weatherer.Server.DTOs;

namespace Weatherer.Server.Persistence;

public interface IWeatherRepository
{
    void Add(Weather weather);
    Task<IEnumerable<WeatherDataItem>> GetMaxWindAsync(CancellationToken ct);
    Task<IEnumerable<WeatherDataItem>> GetMinTemperaturesAsync(CancellationToken ct);
    Task<IEnumerable<WeatherDataItem>> GetTempTrendAsync(string city, CancellationToken ct);
    Task<IEnumerable<WeatherDataItem>> GetWindTrendAsync(string city, CancellationToken ct);
}