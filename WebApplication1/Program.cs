using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<OutdoorGymRecommendationService>();
builder.Services.AddHttpClient<IWeatherClient, OpenMeteoWeatherClient>(client =>
{
    client.BaseAddress = new Uri("https://api.open-meteo.com");
    client.Timeout = TimeSpan.FromSeconds(10);
});
builder.Services.AddHttpClient<IGeocodingClient, OpenMeteoGeocodingClient>(client =>
{
    client.BaseAddress = new Uri("https://geocoding-api.open-meteo.com");
    client.Timeout = TimeSpan.FromSeconds(10);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Klient", "Trener" };

        foreach (var roleName in roleNames)
        {
            bool roleExists = await roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
    catch (Exception exception) when (app.Environment.IsDevelopment())
    {
        Console.WriteLine($"Nie udalo sie przygotowac bazy Identity: {exception.Message}");
        Console.WriteLine("Aplikacja zostanie uruchomiona, ale logowanie/rejestracja moga nie dzialac do czasu naprawy bazy danych.");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/weather/outdoor-gym", async (
    double? latitude,
    double? longitude,
    IWeatherClient weatherClient,
    OutdoorGymRecommendationService recommendationService,
    CancellationToken cancellationToken) =>
{
    if (latitude is null || longitude is null)
    {
        return Results.BadRequest(new
        {
            error = "Podaj latitude i longitude.",
            example = "/api/weather/outdoor-gym?latitude=52.2297&longitude=21.0122"
        });
    }

    if (latitude is < -90 or > 90 || longitude is < -180 or > 180)
    {
        return Results.BadRequest(new
        {
            error = "Nieprawidlowe wspolrzedne. Latitude musi byc od -90 do 90, longitude od -180 do 180."
        });
    }

    try
    {
        var weather = await weatherClient.GetCurrentWeatherAsync(
            latitude.Value,
            longitude.Value,
            cancellationToken);

        var recommendation = recommendationService.Recommend(weather);
        return Results.Ok(recommendation);
    }
    catch (TaskCanceledException)
    {
        return Results.Problem(
            title: "Nie udalo sie pobrac pogody.",
            detail: "Przekroczono limit czasu polaczenia z API pogodowym.",
            statusCode: StatusCodes.Status504GatewayTimeout);
    }
    catch (HttpRequestException)
    {
        return Results.Problem(
            title: "Nie udalo sie pobrac pogody.",
            detail: "Zewnetrzne API pogodowe zwrocilo blad.",
            statusCode: StatusCodes.Status502BadGateway);
    }
    catch (InvalidOperationException)
    {
        return Results.Problem(
            title: "Nie udalo sie przetworzyc pogody.",
            detail: "Odpowiedz API pogodowego nie zawierala wymaganych danych.",
            statusCode: StatusCodes.Status502BadGateway);
    }
})
.WithName("GetOutdoorGymWeatherRecommendation");

app.MapRazorPages();

app.Run();
