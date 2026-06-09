using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Weather;

namespace WebApplication1.Pages;

public class OutdoorGymModel : PageModel
{
    private readonly IGeocodingClient _geocodingClient;
    private readonly IWeatherClient _weatherClient;
    private readonly OutdoorGymRecommendationService _recommendationService;

    public OutdoorGymModel(
        IGeocodingClient geocodingClient,
        IWeatherClient weatherClient,
        OutdoorGymRecommendationService recommendationService)
    {
        _geocodingClient = geocodingClient;
        _weatherClient = weatherClient;
        _recommendationService = recommendationService;
    }

    [BindProperty(SupportsGet = true)]
    public string City { get; set; } = "Warszawa";

    public OutdoorGymRecommendation? Recommendation { get; private set; }

    public CityLocation? Location { get; private set; }

    public string? ErrorMessage { get; private set; }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(City))
        {
            ErrorMessage = "Wpisz nazwe miasta.";
            return;
        }

        try
        {
            Location = await _geocodingClient.FindCityAsync(City.Trim(), cancellationToken);
            if (Location is null)
            {
                ErrorMessage = "Nie znaleziono takiego miasta.";
                return;
            }

            var weather = await _weatherClient.GetCurrentWeatherAsync(
                Location.Latitude,
                Location.Longitude,
                cancellationToken);

            Recommendation = _recommendationService.Recommend(weather);
        }
        catch (TaskCanceledException)
        {
            ErrorMessage = "Przekroczono czas pobierania pogody. Sprobuj ponownie.";
        }
        catch (HttpRequestException)
        {
            ErrorMessage = "Nie udalo sie polaczyc z API pogodowym.";
        }
        catch (InvalidOperationException)
        {
            ErrorMessage = "API pogodowe zwrocilo niepelne dane.";
        }
    }
}
