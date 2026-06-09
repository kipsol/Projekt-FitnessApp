namespace WebApplication1.Weather;

public interface IWeatherClient
{
    Task<WeatherSnapshot> GetCurrentWeatherAsync(
        double latitude,
        double longitude,
        CancellationToken cancellationToken);
}
