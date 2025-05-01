using Microsoft.AspNetCore.Identity;

namespace PersonnelExpenseManagement.Domain.Entities;

public class Role : IdentityRole
{
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
} 