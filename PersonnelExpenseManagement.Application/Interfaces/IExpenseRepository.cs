using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Application.Interfaces;

public interface IExpenseRepository
{
    Task<Expense?> GetByIdAsync(string id);
    Task<IEnumerable<Expense>> GetByUserIdAsync(string userId);
    Task<IEnumerable<Expense>> GetAllAsync();
    Task<Expense> AddAsync(Expense expense);
    Task UpdateAsync(Expense expense);
    Task DeleteAsync(Expense expense);
    Task<IEnumerable<Expense>> GetByStatusAsync(ExpenseStatus status);
    Task<IEnumerable<Expense>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
} 