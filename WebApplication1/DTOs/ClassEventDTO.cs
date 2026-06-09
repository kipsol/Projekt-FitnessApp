using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class ClassEventDto
{
    public int Id { get; set; }

    [Display(Name = "Nazwa zajęć")]
    [Required(ErrorMessage = "Nazwa zajęć jest wymagana.")]
    [StringLength(100, ErrorMessage = "Nazwa może mieć maksymalnie 100 znaków.")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Dzień tygodnia")]
    [Required(ErrorMessage = "Wybór dnia tygodnia jest wymagany.")]
    public DayOfWeek Day { get; set; }

    [Display(Name = "Prowadzący")]
    [Required(ErrorMessage = "Imię i nazwisko prowadzącego jest wymagane.")]
    [StringLength(100, ErrorMessage = "Nazwa może mieć maksymalnie 100 znaków.")]
    public string Trainer { get; set; } = string.Empty;

    [Display(Name = "Godzina rozpoczęcia")]
    [Required(ErrorMessage = "Godzina rozpoczęcia jest wymagana.")]
    [DataType(DataType.Time)]
    public TimeSpan StartTime { get; set; }

    [Display(Name = "Czas trwania (min)")]
    [Required(ErrorMessage = "Czas trwania jest wymagany.")]
    [Range(1, 480, ErrorMessage = "Czas trwania musi wynosić od 1 do 480 minut.")]
    public int Duration { get; set; }

    [Display(Name = "Opis zajęć")]
    [StringLength(1000, ErrorMessage = "Opis może mieć maksymalnie 1000 znaków.")]
    public string? Description { get; set; }
}
