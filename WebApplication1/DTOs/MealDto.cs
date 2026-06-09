using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class MealDto
{
    public int Id { get; set; }

    [Display(Name = "Nazwa posiłku")]
    [Required(ErrorMessage = "Nazwa posiłku jest wymagana.")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Kalorie (kcal)")]
    [Range(0, 5000, ErrorMessage = "Kalorie muszą być między 0 a 5000.")]
    public int Calories { get; set; }

    [Display(Name = "Opis")]
    public string? Description { get; set; }
}
