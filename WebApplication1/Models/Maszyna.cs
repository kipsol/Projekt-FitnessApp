namespace WebApplication1.Models;

public class Maszyna
{
    public int Id { get; set; }

    public string Nazwa { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public int SekcjaId { get; set; }

    public Sekcja Sekcja { get; set; } = null!;

    public ICollection<Cwiczenie> Cwiczenia { get; set; } = new List<Cwiczenie>();
}
