using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Application.DTOs.Expense;

public class UpdateExpenseStatusDto
{
    public ExpenseStatus Status { get; set; }
    public string? RejectionReason { get; set; }
} 