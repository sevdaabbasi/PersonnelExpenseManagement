using PersonnelExpenseManagement.Application.DTOs.Expense;
using PersonnelExpenseManagement.Application.Interfaces;
using PersonnelExpenseManagement.Domain.Entities;
using PersonnelExpenseManagement.Domain.Exceptions;

namespace PersonnelExpenseManagement.Infrastructure.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUserRepository _userRepository;

    public ExpenseService(IExpenseRepository expenseRepository, IUserRepository userRepository)
    {
        _expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<Expense> CreateExpenseAsync(string userId, CreateExpenseDto dto)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        var expense = new Expense
        {
            UserId = userId,
            Amount = dto.Amount,
            Description = dto.Description,
            Location = dto.Location,
            PaymentMethod = dto.PaymentMethod,
            ExpenseCategoryId = dto.ExpenseCategoryId,
            ExpenseDate = dto.ExpenseDate,
            Status = ExpenseStatus.Pending
        };

        return await _expenseRepository.AddAsync(expense);
    }

    public async Task<IEnumerable<Expense>> GetUserExpensesAsync(string userId)
    {
        return await _expenseRepository.GetByUserIdAsync(userId);
    }

    public async Task<Expense> GetExpenseByIdAsync(string id)
    {
        var expense = await _expenseRepository.GetByIdAsync(id);
        if (expense == null)
        {
            throw new NotFoundException("Expense not found");
        }
        return expense;
    }

    public async Task ApproveExpenseAsync(string id, string approvedBy)
    {
        var expense = await GetExpenseByIdAsync(id);
        expense.Status = ExpenseStatus.Approved;
        await _expenseRepository.UpdateAsync(expense);
    }

    public async Task RejectExpenseAsync(string id, string rejectionReason)
    {
        var expense = await GetExpenseByIdAsync(id);
        expense.Status = ExpenseStatus.Rejected;
        expense.RejectionReason = rejectionReason;
        await _expenseRepository.UpdateAsync(expense);
    }

    public async Task<IEnumerable<Expense>> GetPendingExpensesAsync()
    {
        return await _expenseRepository.GetByStatusAsync(ExpenseStatus.Pending);
    }
} 