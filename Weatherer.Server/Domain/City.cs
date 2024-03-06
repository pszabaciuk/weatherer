namespace Weatherer.Server.Domain;

public sealed class City
{
    public int CityId { get; set; }
    public string Name { get; set; } = default!;
    public string Country { get; set; } = default!;
    public double Lat { get; set; }
    public double Lon { get; set; }
}
