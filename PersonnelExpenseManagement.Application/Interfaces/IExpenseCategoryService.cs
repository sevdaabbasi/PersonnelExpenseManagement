using PersonnelExpenseManagement.Application.DTOs.Expense;

namespace PersonnelExpenseManagement.Application.Interfaces;

public interface IExpenseCategoryService
{
    Task<IEnumerable<ExpenseCategoryDto>> GetAllAsync();
    Task<ExpenseCategoryDto> GetByIdAsync(int id);
    Task<ExpenseCategoryDto> CreateAsync(ExpenseCategoryDto categoryDto);
    Task<ExpenseCategoryDto> UpdateAsync(int id, ExpenseCategoryDto categoryDto);
    Task DeleteAsync(int id);
} 