using Microsoft.EntityFrameworkCore;
using PersonnelExpenseManagement.Application.DTOs.Expense;
using PersonnelExpenseManagement.Application.Interfaces;
using PersonnelExpenseManagement.Domain.Entities;
using PersonnelExpenseManagement.Domain.Exceptions;
using PersonnelExpenseManagement.Persistence.Contexts;
using FluentValidation;

namespace PersonnelExpenseManagement.Infrastructure.Services;

public class ExpenseService : IExpenseService
{
    private readonly ApplicationDbContext _context;
    private readonly IValidator<CreateExpenseDto> _validator;

    public ExpenseService(ApplicationDbContext context, IValidator<CreateExpenseDto> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<ExpenseDto> CreateExpenseAsync(CreateExpenseDto dto, string userId)
    {
        await _validator.ValidateAsync(dto);
        
        var expense = new Expense
        {
            UserId = userId,
            ExpenseCategoryId = dto.ExpenseCategoryId,
            Amount = dto.Amount,
            Description = dto.Description,
            Location = dto.Location,
            PaymentMethod = dto.PaymentMethod,
            DocumentPath = dto.DocumentPath,
            ExpenseDate = dto.ExpenseDate,
            Status = ExpenseStatus.Pending
        };

        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
        return MapToDto(expense);
    }

    public async Task<ExpenseDto> GetExpenseByIdAsync(int id)
    {
        var expense = await _context.Expenses
            .Include(e => e.ExpenseCategory)
            .FirstOrDefaultAsync(e => e.Id == id) 
            ?? throw new NotFoundException($"Expense with ID {id} not found");

        return MapToDto(expense);
    }

    public async Task<IEnumerable<ExpenseDto>> GetExpensesByUserIdAsync(string userId)
    {
        var expenses = await _context.Expenses
            .Include(e => e.ExpenseCategory)
            .Where(e => e.UserId == userId)
            .ToListAsync();

        return expenses.Select(MapToDto);
    }

    public async Task<ExpenseDto> UpdateExpenseStatusAsync(int id, ExpenseStatus status, string? rejectionReason = null)
    {
        var expense = await _context.Expenses.FindAsync(id) 
            ?? throw new NotFoundException($"Expense with ID {id} not found");

        expense.Status = status;
        expense.RejectionReason = rejectionReason;
        
        if (status == ExpenseStatus.Approved)
        {
            // Simple payment simulation
            expense.Status = ExpenseStatus.Paid;
        }

        await _context.SaveChangesAsync();
        return MapToDto(expense);
    }

    private static ExpenseDto MapToDto(Expense expense) => new()
    {
        Id = expense.Id,
        UserId = expense.UserId,
        ExpenseCategoryId = expense.ExpenseCategoryId,
        Amount = expense.Amount,
        Description = expense.Description ?? string.Empty,
        Location = expense.Location ?? string.Empty,
        PaymentMethod = expense.PaymentMethod ?? string.Empty,
        DocumentPath = expense.DocumentPath,
        Status = expense.Status,
        RejectionReason = expense.RejectionReason,
        ExpenseDate = expense.ExpenseDate
    };
} 