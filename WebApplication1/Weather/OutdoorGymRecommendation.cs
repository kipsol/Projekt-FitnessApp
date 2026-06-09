namespace WebApplication1.Weather;

public sealed record OutdoorGymRecommendation(
    string Decision,
    bool CanGoOutsideGym,
    string Message,
    IReadOnlyList<string> Reasons,
    WeatherSnapshot Weather);
