using Microsoft.AspNetCore.Identity;

namespace PersonnelExpenseManagement.Domain.Entities;

public class Role : IdentityRole
{
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
} 