using PersonnelExpenseManagement.Application.DTOs.Report;

namespace PersonnelExpenseManagement.Application.Interfaces;

public interface IReportService
{
    Task<DailyReportDto> GetDailyReportAsync(DateTime date);
    Task<WeeklyReportDto> GetWeeklyReportAsync(DateTime startDate);
    Task<MonthlyReportDto> GetMonthlyReportAsync(int year, int month);
    Task<PersonalReportDto> GetPersonalReportAsync(string userId, DateTime startDate, DateTime endDate);
    Task<CompanyReportDto> GetCompanyReportAsync(DateTime startDate, DateTime endDate);
} 