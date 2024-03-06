namespace Weatherer.Server.DTOs;

public sealed record WindDto(string Country, string City, double WindSpeed, long LastUpdate);