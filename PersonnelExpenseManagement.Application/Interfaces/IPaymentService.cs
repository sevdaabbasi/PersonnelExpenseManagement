using PersonnelExpenseManagement.Application.DTOs.Payment;

namespace PersonnelExpenseManagement.Application.Interfaces;

public interface IPaymentService
{
    Task<PaymentResultDto> ProcessPaymentAsync(int expenseId);
    Task<PaymentStatusDto> GetPaymentStatusAsync(int expenseId);
    Task<PaymentValidationDto> ValidatePaymentAsync(int expenseId);
    Task<PaymentSimulationDto> SimulatePaymentAsync(int expenseId);
} 