using Microsoft.AspNetCore.Identity;

namespace PersonnelExpenseManagement.Domain.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IBAN { get; set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; private set; }
    
  
    public virtual ICollection<Expense> Expenses { get; private set; } = new List<Expense>();
    public virtual ICollection<IdentityUserRole<string>> UserRoles { get; private set; } = new List<IdentityUserRole<string>>();

  
    public User() { }

    public User(string firstName, string lastName, string email, string iban)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = email;
        IBAN = iban;
    }

   
    public void Update(string firstName, string lastName, string iban)
    {
        FirstName = firstName;
        LastName = lastName;
        IBAN = iban;
        UpdatedDate = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedDate = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedDate = DateTime.UtcNow;
    }
} 
 