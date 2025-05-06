using PersonnelExpenseManagement.Application.DTOs.Expense;

namespace PersonnelExpenseManagement.Application.Interfaces;

public interface IReportRepository
{
    Task<IEnumerable<ExpenseDto>> GetUserExpensesReportAsync(string userId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<ExpenseDto>> GetCompanyDailyExpensesReportAsync(DateTime date);
    Task<IEnumerable<ExpenseDto>> GetCompanyWeeklyExpensesReportAsync(DateTime startDate);
    Task<IEnumerable<ExpenseDto>> GetCompanyMonthlyExpensesReportAsync(int year, int month);
    Task<IEnumerable<ExpenseDto>> GetApprovedExpensesReportAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<ExpenseDto>> GetRejectedExpensesReportAsync(DateTime startDate, DateTime endDate);
} 