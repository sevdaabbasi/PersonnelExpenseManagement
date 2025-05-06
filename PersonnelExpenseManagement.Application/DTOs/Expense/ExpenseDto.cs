using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Application.DTOs.Expense;

public class ExpenseDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int ExpenseCategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string? DocumentPath { get; set; }
    public ExpenseStatus Status { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime ExpenseDate { get; set; }
} 