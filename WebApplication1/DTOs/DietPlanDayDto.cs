using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class DietPlanDayDto
{
    public int Id { get; set; }

    [Display(Name = "Dieta")]
    [Required(ErrorMessage = "Wybierz dietę.")]
    public int DietId { get; set; }

    [Display(Name = "Posiłek")]
    [Required(ErrorMessage = "Wybierz posiłek.")]
    public int MealId { get; set; }

    [Display(Name = "Dzień tygodnia")]
    [Required(ErrorMessage = "Wybierz dzień tygodnia.")]
    public DayOfWeek Day { get; set; }

    [Display(Name = "Typ posiłku")]
    [Required(ErrorMessage = "Podaj typ posiłku.")]
    [MaxLength(50, ErrorMessage = "Maksymalnie 50 znaków.")]
    public string MealType { get; set; } = "Śniadanie";
}
