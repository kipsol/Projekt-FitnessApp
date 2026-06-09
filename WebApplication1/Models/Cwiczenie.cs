namespace WebApplication1.Models;

public class Cwiczenie
{
    public int Id { get; set; }

    public string Nazwa { get; set; } = string.Empty;

    public string OpisWykonania { get; set; } = string.Empty;

    public int PartiaMiesniowaId { get; set; }

    public PartiaMiesniowa PartiaMiesniowa { get; set; } = null!;

    public int? MaszynaId { get; set; }

    public Maszyna? Maszyna { get; set; }

    public int LiczbaSerii { get; set; }

    public string LiczbaPowtorzen { get; set; } = string.Empty;

    public int PrzerwaSekundy { get; set; }

    public string? PlikSciezka { get; set; }

    public ICollection<PozycjaPlanu> PozycjePlanu { get; set; } = new List<PozycjaPlanu>();
}
