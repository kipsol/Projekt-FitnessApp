using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa ćwiczenia jest wymagana.")]
        [StringLength(100)]
        [Display(Name = "Nazwa ćwiczenia")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Partia mięśniowa jest wymagana.")]
        [StringLength(100)]
        [Display(Name = "Partia mięśniowa")]
        public string MuscleGroup { get; set; } = string.Empty;

        [StringLength(1000)]
        [Display(Name = "Opis")]
        public string? Description { get; set; }

        [Range(1, 5, ErrorMessage = "Poziom trudności musi być od 1 do 5.")]
        [Display(Name = "Poziom trudności")]
        public int DifficultyLevel { get; set; } = 1;

        [Display(Name = "Data dodania")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}