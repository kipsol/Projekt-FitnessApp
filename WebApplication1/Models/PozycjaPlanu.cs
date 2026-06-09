namespace WebApplication1.Models;

public class PozycjaPlanu
{
    public int Id { get; set; }

    public int PlanTreningowyId { get; set; }

    public PlanTreningowy PlanTreningowy { get; set; } = null!;

    public int CwiczenieId { get; set; }

    public Cwiczenie Cwiczenie { get; set; } = null!;

    public string DzienTreningowy { get; set; } = string.Empty;

    public int LiczbaSerii { get; set; }

    public string LiczbaPowtorzen { get; set; } = string.Empty;

    public int PrzerwaSekundy { get; set; }
}
