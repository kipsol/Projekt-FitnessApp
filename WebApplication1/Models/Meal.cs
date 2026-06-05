using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Meal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa posiłku jest wymagana")]
        [Display(Name = "Nazwa posiłku")]
        public string Name { get; set; } = string.Empty;

        [Range(0, 5000)]
        [Display(Name = "Kalorie (kcal)")]
        public int Calories { get; set; }

        public string? Description { get; set; }

        public ICollection<DietPlanDay> PlanDays { get; set; } = new List<DietPlanDay>();
    }
}