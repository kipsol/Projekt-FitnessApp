namespace WebApplication1.Weather;

public sealed record CityLocation(
    string Name,
    string? Country,
    string? Admin1,
    double Latitude,
    double Longitude);
