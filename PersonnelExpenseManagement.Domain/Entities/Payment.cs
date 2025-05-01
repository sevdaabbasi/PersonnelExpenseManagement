namespace PersonnelExpenseManagement.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public int ExpenseId { get; set; }
    public string TransactionId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public DateTime PaymentDate { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
    
    // Navigation properties
    public Expense Expense { get; set; } = null!;
}

public enum PaymentStatus
{
    Pending,
    Completed,
    Failed
} 