using PersonnelExpenseManagement.Application.DTOs.Payment;
using PersonnelExpenseManagement.Application.Interfaces;
using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUserRepository _userRepository;

    public PaymentService(IExpenseRepository expenseRepository, IUserRepository userRepository)
    {
        _expenseRepository = expenseRepository;
        _userRepository = userRepository;
    }

    public async Task<PaymentResultDto> ProcessPaymentAsync(int expenseId)
    {
        var expense = await _expenseRepository.GetByIdAsync(expenseId);
        if (expense == null)
            throw new ArgumentException("Expense not found", nameof(expenseId));

        if (expense.Status != ExpenseStatus.Approved)
            throw new InvalidOperationException("Only approved expenses can be processed for payment");

        var user = await _userRepository.GetByIdAsync(expense.UserId);
        if (user == null)
            throw new ArgumentException("User not found", nameof(expense.UserId));

       
        var paymentResult = new PaymentResultDto
        {
            PaymentId = Guid.NewGuid().ToString(),
            ExpenseId = expenseId.ToString(),
            Amount = expense.Amount,
            Status = "Completed",
            TransactionReference = $"TRX-{DateTime.UtcNow:yyyyMMddHHmmss}",
            ProcessedDate = DateTime.UtcNow
        };

       
        expense.Status = ExpenseStatus.Paid;
        await _expenseRepository.UpdateAsync(expense);

        return paymentResult;
    }

    public async Task<PaymentStatusDto> GetPaymentStatusAsync(int expenseId)
    {
        var expense = await _expenseRepository.GetByIdAsync(expenseId);
        if (expense == null)
            throw new ArgumentException("Expense not found", nameof(expenseId));

        return new PaymentStatusDto
        {
            PaymentId = expense.Id.ToString(),
            Status = expense.Status.ToString(),
            LastUpdated = expense.UpdatedDate ?? expense.CreatedDate,
            StatusDescription = GetStatusDescription(expense.Status)
        };
    }

    public async Task<PaymentValidationDto> ValidatePaymentAsync(int expenseId)
    {
        var expense = await _expenseRepository.GetByIdAsync(expenseId);
        if (expense == null)
            throw new ArgumentException("Expense not found", nameof(expenseId));

        var user = await _userRepository.GetByIdAsync(expense.UserId);
        if (user == null)
            throw new ArgumentException("User not found", nameof(expense.UserId));

        var validation = new PaymentValidationDto
        {
            IsValid = true,
            IbanNumber = user.IbanNumber,
            BankName = user.BankName,
            AccountHolderName = user.FullName
        };

        if (string.IsNullOrEmpty(user.IbanNumber))
        {
            validation.IsValid = false;
            validation.ValidationErrors.Add("IBAN number is not provided");
        }

        if (string.IsNullOrEmpty(user.BankName))
        {
            validation.IsValid = false;
            validation.ValidationErrors.Add("Bank name is not provided");
        }

        return validation;
    }

    public async Task<PaymentSimulationDto> SimulatePaymentAsync(int expenseId)
    {
        var expense = await _expenseRepository.GetByIdAsync(expenseId);
        if (expense == null)
            throw new ArgumentException("Expense not found", nameof(expenseId));

        var user = await _userRepository.GetByIdAsync(expense.UserId);
        if (user == null)
            throw new ArgumentException("User not found", nameof(expense.UserId));

        return new PaymentSimulationDto
        {
            PaymentId = Guid.NewGuid().ToString(),
            ExpenseId = expenseId.ToString(),
            Amount = expense.Amount,
            Status = "Simulated",
            TransactionReference = $"SIM-{DateTime.UtcNow:yyyyMMddHHmmss}",
            SimulatedDate = DateTime.UtcNow,
            SimulationNotes = $"Simulated payment to {user.FullName} ({user.IbanNumber})"
        };
    }

    private string GetStatusDescription(ExpenseStatus status) => status switch
    {
        ExpenseStatus.Pending => "Payment is pending approval",
        ExpenseStatus.Approved => "Payment is approved and ready for processing",
        ExpenseStatus.Rejected => "Payment request has been rejected",
        ExpenseStatus.Paid => "Payment has been processed successfully",
        _ => "Unknown status"
    };
} 