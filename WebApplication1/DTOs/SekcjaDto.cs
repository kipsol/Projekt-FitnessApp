using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class SekcjaDto
{
    public int Id { get; set; }

    [Display(Name = "Nazwa")]
    [Required(ErrorMessage = "Podaj nazwę sekcji.")]
    [StringLength(100, ErrorMessage = "Nazwa może mieć maksymalnie 100 znaków.")]
    public string Nazwa { get; set; } = string.Empty;

    [Display(Name = "Piętro")]
    [Required(ErrorMessage = "Podaj numer piętra.")]
    [Range(0, 20, ErrorMessage = "Piętro musi być między 0 a 20.")]
    public int Pietro { get; set; }
}
