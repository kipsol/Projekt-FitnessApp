using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApplication1.Weather;

public sealed class OpenMeteoGeocodingClient : IGeocodingClient
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly HttpClient _httpClient;

    public OpenMeteoGeocodingClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CityLocation?> FindCityAsync(string city, CancellationToken cancellationToken)
    {
        var path = $"/v1/search?name={Uri.EscapeDataString(city)}&count=1&language=pl&format=json";
        using var response = await _httpClient.GetAsync(path, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Open-Meteo Geocoding zwrocilo status {(int)response.StatusCode}.");
        }

        await using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var geocodingResponse = await JsonSerializer.DeserializeAsync<GeocodingResponse>(
            responseStream,
            JsonOptions,
            cancellationToken);

        var result = geocodingResponse?.Results?.FirstOrDefault();

        return result is null
            ? null
            : new CityLocation(
                result.Name ?? city,
                result.Country,
                result.Admin1,
                result.Latitude,
                result.Longitude);
    }

    private sealed class GeocodingResponse
    {
        [JsonPropertyName("results")]
        public List<GeocodingResult>? Results { get; set; }
    }

    private sealed class GeocodingResult
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("admin1")]
        public string? Admin1 { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
}
