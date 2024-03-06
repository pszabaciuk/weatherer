using Weatherer.Server.Domain;
using Weatherer.Server.DTOs;
using Weatherer.Server.Persistence;
using Weatherer.Server.Service;

namespace Weatherer.Server.Workers;

public sealed class WeatherDownloadWorker : BackgroundService
{
    private readonly IWeatherService _weatherService;
    private readonly ICityRepository _cityRepository;
    private readonly IWeatherRepository _weatherRepository;
    private readonly ILogger<WeatherDownloadWorker> _logger;
    private int _executionCount;

    public WeatherDownloadWorker(IWeatherService weatherService, ICityRepository cityRepository, IWeatherRepository weatherRepository, ILogger<WeatherDownloadWorker> logger)
    {
        _weatherService = weatherService;
        _cityRepository = cityRepository;
        _weatherRepository = weatherRepository;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        await DoWorkAsync(ct);

        using PeriodicTimer timer = new(TimeSpan.FromMinutes(1));

        try
        {
            while (await timer.WaitForNextTickAsync(ct))
            {
                await DoWorkAsync(ct);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");
        }
    }

    private async Task DoWorkAsync(CancellationToken ct)
    {
        int count = Interlocked.Increment(ref _executionCount);

        _logger.LogInformation("Timed Hosted Service is working. Count: {Count}", count);

        IEnumerable<City> cities = _cityRepository.GetAll();

        foreach (City city in cities)
        {
            _logger.LogDebug($"Gathering weather for city: {city.Name} in {city.Country}");

            Result<WeatherDto> weatherResult = await _weatherService.GetWeatherAsync(city.Lat, city.Lon, ct);

            if (weatherResult.IsSuccess == false)
            {
                _logger.LogError($"Could not get weather for city: {city.Name}");
                continue;
            }

            WeatherDto weather = weatherResult.Value;

            Weather weatherDb = new Weather
            {
                CityId = city.CityId,
                Clouds = weather.Coulds.Clouds,
                Country = city.Country,
                Temperature = weather.Main.Temperature,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                WindSpeed = weather.Wind.WindSpeed
            };

            _weatherRepository.Add(weatherDb);
        }
    }
}