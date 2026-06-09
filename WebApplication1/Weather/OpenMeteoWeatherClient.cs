using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApplication1.Weather;

public sealed class OpenMeteoWeatherClient : IWeatherClient
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly HttpClient _httpClient;

    public OpenMeteoWeatherClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherSnapshot> GetCurrentWeatherAsync(
        double latitude,
        double longitude,
        CancellationToken cancellationToken)
    {
        var path = string.Create(
            CultureInfo.InvariantCulture,
            $"/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,apparent_temperature,precipitation,rain,showers,snowfall,weather_code,wind_speed_10m,wind_gusts_10m,is_day&timezone=auto");

        using var response = await _httpClient.GetAsync(path, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Open-Meteo zwrocilo status {(int)response.StatusCode}.");
        }

        await using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var weatherResponse = await JsonSerializer.DeserializeAsync<OpenMeteoResponse>(
            responseStream,
            JsonOptions,
            cancellationToken);

        var current = weatherResponse?.Current
            ?? throw new InvalidOperationException("Brak aktualnych danych pogodowych w odpowiedzi Open-Meteo.");

        return new WeatherSnapshot(
            current.Time ?? string.Empty,
            current.TemperatureC,
            current.ApparentTemperatureC,
            current.PrecipitationMm,
            current.RainMm,
            current.ShowersMm,
            current.SnowfallCm,
            current.WeatherCode,
            WeatherCodeDescriptions.GetDescription(current.WeatherCode),
            current.WindSpeedKmh,
            current.WindGustsKmh,
            current.IsDay == 1);
    }

    private sealed class OpenMeteoResponse
    {
        [JsonPropertyName("current")]
        public OpenMeteoCurrent? Current { get; set; }
    }

    private sealed class OpenMeteoCurrent
    {
        [JsonPropertyName("time")]
        public string? Time { get; set; }

        [JsonPropertyName("temperature_2m")]
        public double TemperatureC { get; set; }

        [JsonPropertyName("apparent_temperature")]
        public double ApparentTemperatureC { get; set; }

        [JsonPropertyName("precipitation")]
        public double PrecipitationMm { get; set; }

        [JsonPropertyName("rain")]
        public double RainMm { get; set; }

        [JsonPropertyName("showers")]
        public double ShowersMm { get; set; }

        [JsonPropertyName("snowfall")]
        public double SnowfallCm { get; set; }

        [JsonPropertyName("weather_code")]
        public int WeatherCode { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public double WindSpeedKmh { get; set; }

        [JsonPropertyName("wind_gusts_10m")]
        public double WindGustsKmh { get; set; }

        [JsonPropertyName("is_day")]
        public int IsDay { get; set; }
    }
}
