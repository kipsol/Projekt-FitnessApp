namespace WebApplication1.Weather;

public interface IGeocodingClient
{
    Task<CityLocation?> FindCityAsync(string city, CancellationToken cancellationToken);
}
