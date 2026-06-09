using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class ClassSchedule
{
    public int Id { get; set; }


    [Required]
    [Display(Name = "Zajęcia")]
    public int ClassEventId { get; set; }

    public ClassEvent? ClassEvent { get; set; }
}