namespace WebApplication1.Models;

public class PlanTreningowy
{
    public int Id { get; set; }

    public string Nazwa { get; set; } = string.Empty;

    public string? Opis { get; set; }

    public string PoziomZaawansowania { get; set; } = string.Empty;

    public int CzasTrwaniaTygodnie { get; set; }

    public ICollection<Cwiczenie> Cwiczenia { get; set; } = new List<Cwiczenie>();
}
