using Microsoft.EntityFrameworkCore;
using PersonnelExpenseManagement.Application.DTOs.Expense;
using PersonnelExpenseManagement.Application.Interfaces;
using PersonnelExpenseManagement.Domain.Entities;
using PersonnelExpenseManagement.Domain.Exceptions;
using PersonnelExpenseManagement.Persistence.Contexts;

namespace PersonnelExpenseManagement.Infrastructure.Services;

public class ExpenseCategoryService : IExpenseCategoryService
{
    private readonly ApplicationDbContext _context;

    public ExpenseCategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExpenseCategoryDto>> GetAllAsync()
    {
        var categories = await _context.ExpenseCategories.ToListAsync();
        return categories.Select(c => new ExpenseCategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description ?? string.Empty
        });
    }

    public async Task<ExpenseCategoryDto> GetByIdAsync(int id)
    {
        var category = await _context.ExpenseCategories.FindAsync(id);
        if (category == null)
        {
            throw new NotFoundException("Category not found");
        }

        return new ExpenseCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description ?? string.Empty
        };
    }

    public async Task<ExpenseCategoryDto> CreateAsync(ExpenseCategoryDto categoryDto)
    {
        var category = new ExpenseCategory
        {
            Name = categoryDto.Name,
            Description = categoryDto.Description
        };

        _context.ExpenseCategories.Add(category);
        await _context.SaveChangesAsync();

        return new ExpenseCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description ?? string.Empty
        };
    }

    public async Task<ExpenseCategoryDto> UpdateAsync(int id, ExpenseCategoryDto categoryDto)
    {
        var category = await _context.ExpenseCategories.FindAsync(id);
        if (category == null)
        {
            throw new NotFoundException("Category not found");
        }

        category.Name = categoryDto.Name;
        category.Description = categoryDto.Description;

        await _context.SaveChangesAsync();

        return new ExpenseCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description ?? string.Empty
        };
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _context.ExpenseCategories.FindAsync(id);
        if (category == null)
        {
            throw new NotFoundException("Category not found");
        }

        _context.ExpenseCategories.Remove(category);
        await _context.SaveChangesAsync();
    }
} 