namespace Weatherer.Server.DTOs;

public sealed record WeatherData(IEnumerable<WeatherDataItem> Temp, IEnumerable<WeatherDataItem> Wind);

public sealed record WeatherDataItem(string Country, string City, double Value, long Timestamp)
{
    public string LastUpdate
    {
        get
        {
            return DateTimeOffset.FromUnixTimeSeconds(Timestamp).DateTime.ToString("yyyy/MM/dd HH:mm");
        }
    }
}