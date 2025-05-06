namespace PersonnelExpenseManagement.Application.DTOs.Payment;

public class PaymentValidationDto
{
    public bool IsValid { get; set; }
    public List<string> ValidationErrors { get; set; } = new();
    public string? IbanNumber { get; set; }
    public string? BankName { get; set; }
    public string? AccountHolderName { get; set; }
} 