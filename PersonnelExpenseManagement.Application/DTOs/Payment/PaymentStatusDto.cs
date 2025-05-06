namespace PersonnelExpenseManagement.Application.DTOs.Payment;

public class PaymentStatusDto
{
    public string PaymentId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime LastUpdated { get; set; }
    public string? StatusDescription { get; set; }
} 