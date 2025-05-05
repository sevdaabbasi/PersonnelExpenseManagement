using Microsoft.EntityFrameworkCore;
using PersonnelExpenseManagement.Application.Interfaces;
using PersonnelExpenseManagement.Domain.Entities;
using PersonnelExpenseManagement.Persistence.Contexts;

namespace PersonnelExpenseManagement.Persistence.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly ApplicationDbContext _context;

    public ExpenseRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Expense?> GetByIdAsync(string id)
    {
        return await _context.Expenses
            .Include(e => e.User)
            .Include(e => e.ExpenseCategory)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Expense>> GetByUserIdAsync(string userId)
    {
        return await _context.Expenses
            .Include(e => e.User)
            .Include(e => e.ExpenseCategory)
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Expense>> GetAllAsync()
    {
        return await _context.Expenses
            .Include(e => e.User)
            .Include(e => e.ExpenseCategory)
            .ToListAsync();
    }

    public async Task<Expense> AddAsync(Expense expense)
    {
        await _context.Expenses.AddAsync(expense);
        await _context.SaveChangesAsync();
        return expense;
    }

    public async Task UpdateAsync(Expense expense)
    {
        _context.Expenses.Update(expense);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Expense expense)
    {
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Expense>> GetByStatusAsync(ExpenseStatus status)
    {
        return await _context.Expenses
            .Include(e => e.User)
            .Include(e => e.ExpenseCategory)
            .Where(e => e.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<Expense>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Expenses
            .Include(e => e.User)
            .Include(e => e.ExpenseCategory)
            .Where(e => e.ExpenseDate >= startDate && e.ExpenseDate <= endDate)
            .ToListAsync();
    }
} 