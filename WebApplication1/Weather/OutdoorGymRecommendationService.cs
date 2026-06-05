namespace WebApplication1.Weather;

public sealed class OutdoorGymRecommendationService
{
    public OutdoorGymRecommendation Recommend(WeatherSnapshot weather)
    {
        var blockingReasons = new List<string>();
        var cautionReasons = new List<string>();

        if (IsStorm(weather.WeatherCode))
        {
            blockingReasons.Add("Burza lub ryzyko burzy.");
        }

        if (IsHeavyRain(weather.WeatherCode) ||
            weather.PrecipitationMm >= 1.5 ||
            weather.RainMm >= 1.5 ||
            weather.ShowersMm >= 1.5)
        {
            blockingReasons.Add("Opady sa zbyt mocne na bezpieczny trening w plenerze.");
        }
        else if (IsLightRain(weather.WeatherCode) ||
            weather.PrecipitationMm > 0 ||
            weather.RainMm > 0 ||
            weather.ShowersMm > 0)
        {
            cautionReasons.Add("Wystepuja lekkie opady, sprzet moze byc mokry i sliski.");
        }

        if (IsSnowOrFreezing(weather.WeatherCode) || weather.SnowfallCm > 0)
        {
            blockingReasons.Add("Snieg lub oblodzenie zwieksza ryzyko poslizgniecia.");
        }

        if (weather.WindSpeedKmh >= 35 || weather.WindGustsKmh >= 50)
        {
            blockingReasons.Add("Wiatr jest zbyt silny na komfortowy trening.");
        }
        else if (weather.WindSpeedKmh >= 25 || weather.WindGustsKmh >= 40)
        {
            cautionReasons.Add("Wiatr jest odczuwalny, wybierz spokojniejszy trening.");
        }

        if (weather.ApparentTemperatureC <= -8 || weather.TemperatureC <= -5)
        {
            blockingReasons.Add("Temperatura odczuwalna jest zbyt niska.");
        }
        else if (weather.ApparentTemperatureC <= 2 || weather.TemperatureC <= 5)
        {
            cautionReasons.Add("Jest chlodno, potrzebna jest rozgrzewka i cieple ubranie.");
        }

        if (weather.ApparentTemperatureC >= 35 || weather.TemperatureC >= 32)
        {
            blockingReasons.Add("Jest zbyt goraco na intensywny trening na zewnatrz.");
        }
        else if (weather.ApparentTemperatureC >= 28 || weather.TemperatureC >= 28)
        {
            cautionReasons.Add("Jest cieplo, wez wode i unikaj bardzo intensywnego wysilku.");
        }

        if (!weather.IsDay)
        {
            cautionReasons.Add("Jest po zmroku, wybierz tylko dobrze oswietlona silownie plenerowa.");
        }

        if (blockingReasons.Count > 0)
        {
            return new OutdoorGymRecommendation(
                "nie",
                false,
                "Nie zalecam treningu na silowni plenerowej przy tej pogodzie.",
                blockingReasons.Concat(cautionReasons).ToList(),
                weather);
        }

        if (cautionReasons.Count > 0)
        {
            return new OutdoorGymRecommendation(
                "ostroznie",
                true,
                "Mozna isc na silownie plenerowa, ale zachowaj ostroznosc.",
                cautionReasons,
                weather);
        }

        return new OutdoorGymRecommendation(
            "tak",
            true,
            "Warunki sa dobre na trening na silowni plenerowej.",
            ["Brak opadow, umiarkowany wiatr i bezpieczna temperatura."],
            weather);
    }

    private static bool IsStorm(int weatherCode) => weatherCode is 95 or 96 or 99;

    private static bool IsHeavyRain(int weatherCode) => weatherCode is 63 or 65 or 81 or 82;

    private static bool IsLightRain(int weatherCode) =>
        weatherCode is 51 or 53 or 55 or 61 or 80;

    private static bool IsSnowOrFreezing(int weatherCode) =>
        weatherCode is 56 or 57 or 66 or 67 or 71 or 73 or 75 or 77 or 85 or 86;
}
