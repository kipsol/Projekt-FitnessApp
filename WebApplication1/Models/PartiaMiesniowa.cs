namespace WebApplication1.Models;

public class PartiaMiesniowa
{
    public int Id { get; set; }

    public string Nazwa { get; set; } = string.Empty;

    public ICollection<Cwiczenie> Cwiczenia { get; set; } = new List<Cwiczenie>();
}
