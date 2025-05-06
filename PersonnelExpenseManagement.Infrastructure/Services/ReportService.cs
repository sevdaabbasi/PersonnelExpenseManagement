using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PersonnelExpenseManagement.Application.DTOs.Report;
using PersonnelExpenseManagement.Application.Interfaces;
using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Infrastructure.Services;

public class ReportService : IReportService
{
    private readonly string _connectionString;
    private readonly IExpenseRepository _expenseRepository;

    public ReportService(IConfiguration configuration, IExpenseRepository expenseRepository)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        _expenseRepository = expenseRepository;
    }

    public async Task<DailyReportDto> GetDailyReportAsync(DateTime date)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var sql = @"
            SELECT 
                e.Id,
                u.UserName,
                ec.Name as CategoryName,
                e.Amount,
                e.Status,
                e.CreatedDate
            FROM Expenses e
            JOIN Users u ON e.UserId = u.Id
            JOIN ExpenseCategories ec ON e.ExpenseCategoryId = ec.Id
            WHERE CAST(e.CreatedDate AS DATE) = @Date";

        var expenses = await connection.QueryAsync<ExpenseSummaryDto>(sql, new { Date = date.Date });

        var report = new DailyReportDto
        {
            Date = date.Date,
            Expenses = expenses.ToList()
        };

        report.TotalCount = report.Expenses.Count;
        report.TotalAmount = report.Expenses.Sum(e => e.Amount);
        report.ApprovedCount = report.Expenses.Count(e => e.Status == "Approved");
        report.RejectedCount = report.Expenses.Count(e => e.Status == "Rejected");
        report.PendingCount = report.Expenses.Count(e => e.Status == "Pending");
        report.ApprovedAmount = report.Expenses.Where(e => e.Status == "Approved").Sum(e => e.Amount);
        report.RejectedAmount = report.Expenses.Where(e => e.Status == "Rejected").Sum(e => e.Amount);
        report.PendingAmount = report.Expenses.Where(e => e.Status == "Pending").Sum(e => e.Amount);

        return report;
    }

    public async Task<WeeklyReportDto> GetWeeklyReportAsync(DateTime startDate)
    {
        var endDate = startDate.AddDays(6);
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var sql = @"
            SELECT 
                CAST(e.CreatedDate AS DATE) as Date,
                COUNT(*) as Count,
                SUM(e.Amount) as Amount,
                SUM(CASE WHEN e.Status = 'Approved' THEN 1 ELSE 0 END) as ApprovedCount,
                SUM(CASE WHEN e.Status = 'Rejected' THEN 1 ELSE 0 END) as RejectedCount,
                SUM(CASE WHEN e.Status = 'Pending' THEN 1 ELSE 0 END) as PendingCount,
                SUM(CASE WHEN e.Status = 'Approved' THEN e.Amount ELSE 0 END) as ApprovedAmount,
                SUM(CASE WHEN e.Status = 'Rejected' THEN e.Amount ELSE 0 END) as RejectedAmount,
                SUM(CASE WHEN e.Status = 'Pending' THEN e.Amount ELSE 0 END) as PendingAmount
            FROM Expenses e
            WHERE CAST(e.CreatedDate AS DATE) BETWEEN @StartDate AND @EndDate
            GROUP BY CAST(e.CreatedDate AS DATE)
            ORDER BY Date";

        var dailyReports = await connection.QueryAsync<DailyReportDto>(sql, new { StartDate = startDate.Date, EndDate = endDate.Date });

        return new WeeklyReportDto
        {
            StartDate = startDate.Date,
            EndDate = endDate.Date,
            DailyReports = dailyReports.ToList()
        };
    }

    public async Task<MonthlyReportDto> GetMonthlyReportAsync(int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var sql = @"
            SELECT 
                CAST(e.CreatedDate AS DATE) as Date,
                COUNT(*) as Count,
                SUM(e.Amount) as Amount,
                SUM(CASE WHEN e.Status = 'Approved' THEN 1 ELSE 0 END) as ApprovedCount,
                SUM(CASE WHEN e.Status = 'Rejected' THEN 1 ELSE 0 END) as RejectedCount,
                SUM(CASE WHEN e.Status = 'Pending' THEN 1 ELSE 0 END) as PendingCount,
                SUM(CASE WHEN e.Status = 'Approved' THEN e.Amount ELSE 0 END) as ApprovedAmount,
                SUM(CASE WHEN e.Status = 'Rejected' THEN e.Amount ELSE 0 END) as RejectedAmount,
                SUM(CASE WHEN e.Status = 'Pending' THEN e.Amount ELSE 0 END) as PendingAmount
            FROM Expenses e
            WHERE CAST(e.CreatedDate AS DATE) BETWEEN @StartDate AND @EndDate
            GROUP BY CAST(e.CreatedDate AS DATE)
            ORDER BY Date";

        var dailyReports = await connection.QueryAsync<DailyReportDto>(sql, new { StartDate = startDate.Date, EndDate = endDate.Date });

        return new MonthlyReportDto
        {
            Year = year,
            Month = month,
            DailyReports = dailyReports.ToList()
        };
    }

    public async Task<PersonalReportDto> GetPersonalReportAsync(string userId, DateTime startDate, DateTime endDate)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var sql = @"
            SELECT 
                e.Id,
                ec.Name as CategoryName,
                e.Amount,
                e.Status,
                e.CreatedDate,
                e.Description,
                e.Location,
                e.PaymentMethod
            FROM Expenses e
            JOIN ExpenseCategories ec ON e.ExpenseCategoryId = ec.Id
            WHERE e.UserId = @UserId
            AND CAST(e.CreatedDate AS DATE) BETWEEN @StartDate AND @EndDate
            ORDER BY e.CreatedDate DESC";

        var expenses = await connection.QueryAsync<ExpenseDetailDto>(sql, new { UserId = userId, StartDate = startDate.Date, EndDate = endDate.Date });

        return new PersonalReportDto
        {
            UserId = userId,
            StartDate = startDate.Date,
            EndDate = endDate.Date,
            Expenses = expenses.ToList()
        };
    }

    public async Task<CompanyReportDto> GetCompanyReportAsync(DateTime startDate, DateTime endDate)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var sql = @"
            SELECT 
                u.Id as UserId,
                u.UserName,
                COUNT(*) as Count,
                SUM(e.Amount) as Amount,
                SUM(CASE WHEN e.Status = 'Approved' THEN 1 ELSE 0 END) as ApprovedCount,
                SUM(CASE WHEN e.Status = 'Rejected' THEN 1 ELSE 0 END) as RejectedCount,
                SUM(CASE WHEN e.Status = 'Pending' THEN 1 ELSE 0 END) as PendingCount,
                SUM(CASE WHEN e.Status = 'Approved' THEN e.Amount ELSE 0 END) as ApprovedAmount,
                SUM(CASE WHEN e.Status = 'Rejected' THEN e.Amount ELSE 0 END) as RejectedAmount,
                SUM(CASE WHEN e.Status = 'Pending' THEN e.Amount ELSE 0 END) as PendingAmount
            FROM Expenses e
            JOIN Users u ON e.UserId = u.Id
            WHERE CAST(e.CreatedDate AS DATE) BETWEEN @StartDate AND @EndDate
            GROUP BY u.Id, u.UserName
            ORDER BY u.UserName";

        var userReports = await connection.QueryAsync<UserReportDto>(sql, new { StartDate = startDate.Date, EndDate = endDate.Date });

        return new CompanyReportDto
        {
            StartDate = startDate.Date,
            EndDate = endDate.Date,
            UserReports = userReports.ToList()
        };
    }
} 