using PersonnelExpenseManagement.Application.Interfaces;
using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Infrastructure.Services;

public class PaymentSimulationService : IPaymentSimulationService
{
    private readonly Random _random = new Random();

    public async Task<bool> SimulatePaymentAsync(Expense expense)
    {
       
        await Task.Delay(1000); 

       
        var isSuccessful = _random.Next(100) < 90;

        if (isSuccessful)
        {
           
            return true;
        }
        else
        {
           
            throw new Exception("Payment simulation failed");
        }
    }

    public async Task<PaymentStatus> GetPaymentStatusAsync(string expenseId)
    {
        
        await Task.Delay(500);

        var statuses = Enum.GetValues(typeof(PaymentStatus));
        return (PaymentStatus)statuses.GetValue(_random.Next(statuses.Length))!;
    }

    public async Task<string> GetPaymentDetailsAsync(string expenseId)
    {
       
        await Task.Delay(500);

        return $"Payment details for expense {expenseId}:\n" +
               $"Transaction ID: {Guid.NewGuid()}\n" +
               $"Date: {DateTime.UtcNow}\n" +
               $"Status: {await GetPaymentStatusAsync(expenseId)}";
    }
} 