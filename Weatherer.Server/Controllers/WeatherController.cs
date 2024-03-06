using Microsoft.AspNetCore.Mvc;
using Weatherer.Server.DTOs;
using Weatherer.Server.Persistence;

namespace Weatherer.Server.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class WeatherController : ControllerBase
{
    private readonly IWeatherRepository _weatherRepository;

    public WeatherController(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }

    [HttpGet("GetWeatherData")]
    public async Task<IActionResult> GetWeatherData(CancellationToken ct)
    {
        Task<IEnumerable<WeatherDataItem>> tempTask = _weatherRepository.GetMinTemperaturesAsync(ct);
        Task<IEnumerable<WeatherDataItem>> windTask = _weatherRepository.GetMaxWindAsync(ct);

        await Task.WhenAll(new Task[] { tempTask, windTask });

        IEnumerable<WeatherDataItem> tempResult = tempTask.Result;
        IEnumerable<WeatherDataItem> windResult = windTask.Result;

        return Ok(new WeatherData(tempResult, windResult));
    }

    [HttpGet("GetTempTrend/{city}")]
    public async Task<IActionResult> GetTempTrend(string city, CancellationToken ct)
    {
        var result = await _weatherRepository.GetTempTrendAsync(city, ct);

        return Ok(result);
    }

    [HttpGet("GetWindTrend/{city}")]
    public async Task<IActionResult> GetWindTrend(string city, CancellationToken ct)
    {
        var result = await _weatherRepository.GetWindTrendAsync(city, ct);

        return Ok(result);
    }
}
