using System.Text.Json.Serialization;

namespace Weatherer.Server.DTOs;

public sealed class WeatherDto
{
    [JsonPropertyName("main")]
    public WeatherMainDto Main { get; set; } = default!;
    [JsonPropertyName("clouds")]
    public WeatherCloudsDto Coulds { get; set; } = default!;
    [JsonPropertyName("wind")]
    public WeatherWindDto Wind { get; set; } = default!;
}

public sealed class WeatherMainDto
{
    [JsonPropertyName("temp")]
    public float Temperature { get; set; }
}

public sealed class WeatherCloudsDto
{
    [JsonPropertyName("all")]
    public int Clouds { get; set; }
}

public sealed class WeatherWindDto
{
    [JsonPropertyName("speed")]
    public float WindSpeed { get; set; } = default!;
}