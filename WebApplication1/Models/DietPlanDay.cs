using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class DietPlanDay
{
    public int Id { get; set; }
    public int DietId { get; set; }
    public Diet? Diet { get; set; }
    public int MealId { get; set; }
    public Meal? Meal { get; set; }
    public DayOfWeek Day { get; set; }

    [MaxLength(50)]
    public string MealType { get; set; } = "Śniadanie";
}