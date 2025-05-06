namespace PersonnelExpenseManagement.Domain.Entities;

public class Payment : BaseEntity
{
    public int ExpenseId { get; set; }
    public string TransactionId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public DateTime PaymentDate { get; set; }

   
    public Expense Expense { get; set; } = null!;
}

public enum PaymentStatus
{
    Pending,
    Processing,
    Completed,
    Failed
}