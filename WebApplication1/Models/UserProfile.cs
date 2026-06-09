using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class UserProfile
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    public IdentityUser User { get; set; } = null!;

    [Required]
    [MaxLength(80)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    public string LastName { get; set; } = string.Empty;

    [Range(1, 120)]
    public int Age { get; set; }

    [Range(50, 250)]
    public decimal HeightCm { get; set; }

    [Range(20, 400)]
    public decimal WeightKg { get; set; }

    [MaxLength(300)]
    public string? Goal { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
