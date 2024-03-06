namespace Weatherer.Server.Domain;

public sealed class Weather
{
    public int ForecastId { get; set; }
    public long Timestamp { get; set; }
    public string Country { get; set; } = default!;
    public int CityId { get; set; }
    public double Temperature { get; set; }
    public int Clouds { get; set; }
    public double WindSpeed { get; set; }
}
