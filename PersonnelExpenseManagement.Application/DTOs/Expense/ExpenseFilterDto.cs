using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Application.DTOs.Expense;

public class ExpenseFilterDto
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? CategoryId { get; set; }
    public ExpenseStatus? Status { get; set; }
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
} 