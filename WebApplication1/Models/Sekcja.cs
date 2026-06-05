namespace WebApplication1.Models;

public class Sekcja
{
    public int Id { get; set; }

    public string Nazwa { get; set; } = string.Empty;

    public int Pietro { get; set; }

    public ICollection<Maszyna> Maszyny { get; set; } = new List<Maszyna>();
}
