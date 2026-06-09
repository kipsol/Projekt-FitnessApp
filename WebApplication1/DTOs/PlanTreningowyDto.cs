using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class PlanTreningowyDto
{
    public int Id { get; set; }

    [Display(Name = "Nazwa")]
    [Required(ErrorMessage = "Podaj nazwę planu treningowego.")]
    [StringLength(120, ErrorMessage = "Nazwa może mieć maksymalnie 120 znaków.")]
    public string Nazwa { get; set; } = string.Empty;

    [Display(Name = "Opis")]
    [StringLength(500, ErrorMessage = "Opis może mieć maksymalnie 500 znaków.")]
    public string? Opis { get; set; }

    [Display(Name = "Poziom zaawansowania")]
    [Required(ErrorMessage = "Podaj poziom zaawansowania.")]
    [StringLength(50, ErrorMessage = "Maksymalnie 50 znaków.")]
    public string PoziomZaawansowania { get; set; } = string.Empty;

    [Display(Name = "Czas trwania (tygodnie)")]
    [Required(ErrorMessage = "Podaj czas trwania w tygodniach.")]
    [Range(1, 52, ErrorMessage = "Czas trwania musi być między 1 a 52 tygodnie.")]
    public int CzasTrwaniaTygodnie { get; set; }

    [Display(Name = "Ćwiczenia w planie")]
    public List<int>? CwiczenieIds { get; set; }
}
