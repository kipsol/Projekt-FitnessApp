using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class ClassEvent
{
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public DayOfWeek Day { get; set; }

    [StringLength(100)]
    public string Trainer { get; set; } = string.Empty;

    public TimeSpan StartTime { get; set; }

    public int Duration { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    public ICollection<ClassSchedule> Schedules { get; set; } = new List<ClassSchedule>();
}