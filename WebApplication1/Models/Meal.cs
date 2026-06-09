namespace WebApplication1.Models;

public class Meal
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Calories { get; set; }
    public string? Description { get; set; }
    public ICollection<DietPlanDay> PlanDays { get; set; } = new List<DietPlanDay>();
}