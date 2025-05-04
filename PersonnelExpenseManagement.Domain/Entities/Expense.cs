namespace PersonnelExpenseManagement.Domain.Entities;

public class Expense : BaseEntity
{
    public string UserId { get; set; } = null!;
    public int ExpenseCategoryId { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public string? PaymentMethod { get; set; }
    public string? DocumentPath { get; set; }
    public ExpenseStatus Status { get; set; } = ExpenseStatus.Pending;
    public string? RejectionReason { get; set; }
    public DateTime ExpenseDate { get; set; }

   
    public User User { get; set; } = null!;
    public ExpenseCategory ExpenseCategory { get; set; } = null!;
}

public enum ExpenseStatus
{
    Pending,
    Approved,
    Rejected,
    Paid
}