namespace Weatherer.Config;

internal sealed class WeatherServiceOptions
{
    public string BaseAddress { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
}