using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class ClassScheduleDto
{
    public int Id { get; set; }

    [Display(Name = "Zajęcia")]
    [Required(ErrorMessage = "Wybierz zajęcia.")]
    public int ClassEventId { get; set; }
}
