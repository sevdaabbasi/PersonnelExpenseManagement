using PersonnelExpenseManagement.Application.DTOs.Expense;
using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Application.Interfaces;

public interface IExpenseService
{
    Task<Expense> CreateExpenseAsync(string userId, CreateExpenseDto dto);
    Task<IEnumerable<Expense>> GetUserExpensesAsync(string userId);
    Task<Expense> GetExpenseByIdAsync(string id);
    Task ApproveExpenseAsync(string id, string approvedBy);
    Task RejectExpenseAsync(string id, string rejectionReason);
    Task<IEnumerable<Expense>> GetPendingExpensesAsync();
}