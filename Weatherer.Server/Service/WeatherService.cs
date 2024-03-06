using Microsoft.Extensions.Options;
using Weatherer.Config;
using Weatherer.Server.DTOs;

namespace Weatherer.Server.Service;

internal sealed class WeatherService : IWeatherService
{
    private static string WEATHER_PATH = "/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric&lang=en";

    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherService> _logger;
    private readonly WeatherServiceOptions _options;

    public WeatherService(HttpClient httpClient, IOptions<WeatherServiceOptions> options, ILogger<WeatherService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<Result<WeatherDto>> GetWeatherAsync(double lat, double lon, CancellationToken ct)
    {
        WeatherDto? result = await _httpClient.GetFromJsonAsync<WeatherDto>(UpdatePath(WEATHER_PATH, lat, lon, _options.ApiKey), cancellationToken: ct);

        if (result == null)
        {
            _logger.LogError($"Could not get weather for coordinates: {lat}, {lon}");

            return Result<WeatherDto>.Failure("Could not get weather");
        }

        return Result<WeatherDto>.Success(result);
    }

    private static string UpdatePath(string weatherPath, double lat, double lon, string apiKey)
    {
        return weatherPath.Replace("{lat}", lat.ToString()).Replace("{lon}", lon.ToString()).Replace("{apiKey}", apiKey);
    }
}
