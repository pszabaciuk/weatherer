namespace Weatherer.Server.DTOs;

public sealed record TemperatureDto(string Country, string City, double Temperature, long LastUpdate);
