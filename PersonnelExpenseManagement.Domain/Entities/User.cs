using Microsoft.AspNetCore.Identity;

namespace PersonnelExpenseManagement.Domain.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IBAN { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
    
    // Navigation properties
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    public virtual Role? Role { get; set; }
    public string? RoleId { get; set; }
} 
 