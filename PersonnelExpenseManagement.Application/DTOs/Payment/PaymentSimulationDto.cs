namespace PersonnelExpenseManagement.Application.DTOs.Payment;

public class PaymentSimulationDto
{
    public string PaymentId { get; set; } = string.Empty;
    public string ExpenseId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string TransactionReference { get; set; } = string.Empty;
    public DateTime SimulatedDate { get; set; }
    public string? SimulationNotes { get; set; }
} 