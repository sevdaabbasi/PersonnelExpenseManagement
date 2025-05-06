using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Application.Interfaces;

public interface IExpenseRepository
{
    Task<Expense> GetByIdAsync(int id);
    Task<IEnumerable<Expense>> GetByUserIdAsync(string userId);
    Task<IEnumerable<Expense>> GetByStatusAsync(ExpenseStatus status);
    Task<Expense> AddAsync(Expense expense);
    Task<Expense> UpdateAsync(Expense expense);
    Task DeleteAsync(int id);
} 