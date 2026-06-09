using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class MaszynaDto
{
    public int Id { get; set; }

    [Display(Name = "Nazwa")]
    [Required(ErrorMessage = "Podaj nazwę maszyny.")]
    [StringLength(100, ErrorMessage = "Nazwa może mieć maksymalnie 100 znaków.")]
    public string Nazwa { get; set; } = string.Empty;

    [Display(Name = "Status")]
    [Required(ErrorMessage = "Podaj status maszyny.")]
    [StringLength(50, ErrorMessage = "Status może mieć maksymalnie 50 znaków.")]
    public string Status { get; set; } = string.Empty;

    [Display(Name = "Sekcja")]
    [Required(ErrorMessage = "Wybierz sekcję.")]
    public int SekcjaId { get; set; }
}
