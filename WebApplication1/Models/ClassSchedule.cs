using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models;

public class ClassSchedule
{
    public int Id { get; set; }

    public int ClassEventId { get; set; }

    public ClassEvent? ClassEvent { get; set; }

    public string? UserId { get; set; }

    public IdentityUser? User { get; set; }
}
