using FluentValidation;

namespace PersonnelExpenseManagement.Application.DTOs.Expense;

public class CreateExpenseDto
{
    public int ExpenseCategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string? DocumentPath { get; set; }
    public DateTime ExpenseDate { get; set; }
} 



