using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class ClassEvent
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nazwa zajęć jest wymagana.")]
    [Display(Name = "Nazwa zajęć")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Wybór dnia tygodnia jest wymagany.")]
    [Display(Name = "Dzień tygodnia")]
    public DayOfWeek Day { get; set; }

    [Required(ErrorMessage = "Imię i nazwisko prowadzącego jest wymagane.")]
    [Display(Name = "Prowadzący")]
    [StringLength(100)]
    public string Trainer { get; set; } = string.Empty;

    [Required(ErrorMessage = "Godzina rozpoczęcia jest wymagana.")]
    [Display(Name = "Godzina rozpoczęcia")]
    [DataType(DataType.Time)]
    public TimeSpan StartTime { get; set; }

    [Required(ErrorMessage = "Czas trwania jest wymagany.")]
    [Display(Name = "Czas trwania (min)")]
    [Range(1, 480, ErrorMessage = "Czas trwania musi wynosić od 1 do 480 minut.")]
    public int Duration { get; set; }

    [Display(Name = "Opis zajęć")]
    [StringLength(1000)]
    public string? Description { get; set; }

    public ICollection<ClassSchedule> Schedules { get; set; } = new List<ClassSchedule>();
}