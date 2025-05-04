using Microsoft.AspNetCore.Identity;

namespace PersonnelExpenseManagement.Domain.Entities;

public class Role : IdentityRole
{
    public string Description { get; private set; }
    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; private set; }

    
    public virtual ICollection<User> Users { get; private set; } = new List<User>();

   
    protected Role() { }

    public Role(string name, string description) : base(name)
    {
        Description = description;
    }

   
    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
        UpdatedDate = DateTime.UtcNow;
    }
} 