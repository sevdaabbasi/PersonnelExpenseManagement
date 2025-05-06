using PersonnelExpenseManagement.Application.DTOs.Expense;
using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Application.Interfaces;

public interface IExpenseService
{
    Task<ExpenseDto> CreateExpenseAsync(CreateExpenseDto dto, string userId);
    Task<ExpenseDto> GetExpenseByIdAsync(int id);
    Task<IEnumerable<ExpenseDto>> GetExpensesByUserIdAsync(string userId);
    Task<ExpenseDto> UpdateExpenseStatusAsync(int id, ExpenseStatus status, string? rejectionReason = null);
}