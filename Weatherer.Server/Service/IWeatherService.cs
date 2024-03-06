using Weatherer.Server.DTOs;

namespace Weatherer.Server.Service;
public interface IWeatherService
{
    Task<Result<WeatherDto>> GetWeatherAsync(double lat, double lon, CancellationToken ct);
}