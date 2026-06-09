using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class PartiaMiesniowaDto
{
    public int Id { get; set; }

    [Display(Name = "Nazwa")]
    [Required(ErrorMessage = "Podaj nazwę partii mięśniowej.")]
    [StringLength(80, ErrorMessage = "Nazwa może mieć maksymalnie 80 znaków.")]
    public string Nazwa { get; set; } = string.Empty;
}
