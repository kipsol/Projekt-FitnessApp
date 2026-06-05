namespace WebApplication1.Weather;

public sealed record WeatherSnapshot(
    string Time,
    double TemperatureC,
    double ApparentTemperatureC,
    double PrecipitationMm,
    double RainMm,
    double ShowersMm,
    double SnowfallCm,
    int WeatherCode,
    string WeatherDescription,
    double WindSpeedKmh,
    double WindGustsKmh,
    bool IsDay);
