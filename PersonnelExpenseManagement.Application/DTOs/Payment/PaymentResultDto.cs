namespace PersonnelExpenseManagement.Application.DTOs.Payment;

public class PaymentResultDto
{
    public string PaymentId { get; set; } = string.Empty;
    public string ExpenseId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string TransactionReference { get; set; } = string.Empty;
    public DateTime ProcessedDate { get; set; }
    public string? ErrorMessage { get; set; }
} 