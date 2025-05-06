using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Application.Interfaces;

public interface IPaymentSimulationService
{
    Task<bool> SimulatePaymentAsync(Expense expense);
    Task<PaymentStatus> GetPaymentStatusAsync(string expenseId);
    Task<string> GetPaymentDetailsAsync(string expenseId);
} 