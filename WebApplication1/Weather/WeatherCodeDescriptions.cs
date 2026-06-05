namespace WebApplication1.Weather;

public static class WeatherCodeDescriptions
{
    public static string GetDescription(int weatherCode)
    {
        return weatherCode switch
        {
            0 => "Bezchmurnie",
            1 => "Przewaznie bezchmurnie",
            2 => "Czesciowe zachmurzenie",
            3 => "Zachmurzenie",
            45 => "Mgla",
            48 => "Mgla osadzajaca szadz",
            51 => "Lekka mzawka",
            53 => "Umiarkowana mzawka",
            55 => "Intensywna mzawka",
            56 => "Lekka marznaca mzawka",
            57 => "Intensywna marznaca mzawka",
            61 => "Lekki deszcz",
            63 => "Umiarkowany deszcz",
            65 => "Intensywny deszcz",
            66 => "Lekki marznacy deszcz",
            67 => "Intensywny marznacy deszcz",
            71 => "Lekki snieg",
            73 => "Umiarkowany snieg",
            75 => "Intensywny snieg",
            77 => "Snieg ziarnisty",
            80 => "Lekkie przelotne opady",
            81 => "Umiarkowane przelotne opady",
            82 => "Gwaltowne przelotne opady",
            85 => "Lekkie opady sniegu",
            86 => "Intensywne opady sniegu",
            95 => "Burza",
            96 => "Burza z lekkim gradem",
            99 => "Burza z silnym gradem",
            _ => "Nieznany kod pogody"
        };
    }
}
