using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class ProgressEntry
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    public IdentityUser User { get; set; } = null!;

    public DateTime EntryDate { get; set; } = DateTime.UtcNow;

    [Range(20, 400)]
    public decimal WeightKg { get; set; }

    [Range(0, 300)]
    public decimal? ChestCm { get; set; }

    [Range(0, 300)]
    public decimal? WaistCm { get; set; }

    [Range(0, 300)]
    public decimal? HipCm { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }
}
