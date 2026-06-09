using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class DietDto
{
    public int Id { get; set; }

    [Display(Name = "Nazwa diety")]
    [Required(ErrorMessage = "Nazwa diety jest wymagana.")]
    [MaxLength(150, ErrorMessage = "Nazwa może mieć maksymalnie 150 znaków.")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Opis diety")]
    [MaxLength(500, ErrorMessage = "Opis może mieć maksymalnie 500 znaków.")]
    public string? Description { get; set; }

    [Display(Name = "Cel kaloryczny (kcal)")]
    [Range(0, 10000, ErrorMessage = "Cel kaloryczny musi być między 0 a 10000.")]
    public int TargetCalories { get; set; }
}
