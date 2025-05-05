namespace PersonnelExpenseManagement.Domain.Entities;

public class ExpenseCategory : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

 
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}