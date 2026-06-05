using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class TrainingSession
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    public IdentityUser User { get; set; } = null!;

    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(80)]
    public string? TrainingType { get; set; }

    public DateTime SessionDate { get; set; } = DateTime.UtcNow;

    [Range(1, 600)]
    public int DurationMinutes { get; set; }

    [Range(0, 5000)]
    public int CaloriesBurned { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }
}
