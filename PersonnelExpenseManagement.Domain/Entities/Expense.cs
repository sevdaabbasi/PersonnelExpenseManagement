namespace PersonnelExpenseManagement.Domain.Entities;

public class Expense
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int ExpenseCategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string DocumentPath { get; set; } = string.Empty;
    public ExpenseStatus Status { get; set; } = ExpenseStatus.Pending;
    public string? RejectionReason { get; set; }
    public DateTime ExpenseDate { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
    
   
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