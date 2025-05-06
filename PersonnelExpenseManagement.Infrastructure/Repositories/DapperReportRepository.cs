using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using PersonnelExpenseManagement.Application.DTOs.Expense;
using PersonnelExpenseManagement.Application.Interfaces;
using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.Infrastructure.Repositories;

public class DapperReportRepository : IReportRepository
{
    private readonly string _connectionString;

    public DapperReportRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<ExpenseDto>> GetUserExpensesReportAsync(string userId, DateTime startDate, DateTime endDate)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        const string sql = @"
            SELECT e.*, ec.Name as CategoryName, u.FirstName, u.LastName
            FROM Expenses e
            INNER JOIN ExpenseCategories ec ON e.ExpenseCategoryId = ec.Id
            INNER JOIN Users u ON e.UserId = u.Id
            WHERE e.UserId = @UserId 
            AND e.ExpenseDate BETWEEN @StartDate AND @EndDate
            ORDER BY e.ExpenseDate DESC";

        var expenses = await connection.QueryAsync<ExpenseDto>(sql, new { UserId = userId, StartDate = startDate, EndDate = endDate });
        return expenses;
    }

    public async Task<IEnumerable<ExpenseDto>> GetCompanyDailyExpensesReportAsync(DateTime date)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        const string sql = @"
            SELECT e.*, ec.Name as CategoryName, u.FirstName, u.LastName
            FROM Expenses e
            INNER JOIN ExpenseCategories ec ON e.ExpenseCategoryId = ec.Id
            INNER JOIN Users u ON e.UserId = u.Id
            WHERE CAST(e.ExpenseDate AS DATE) = CAST(@Date AS DATE)
            ORDER BY e.ExpenseDate DESC";

        var expenses = await connection.QueryAsync<ExpenseDto>(sql, new { Date = date });
        return expenses;
    }

    public async Task<IEnumerable<ExpenseDto>> GetCompanyWeeklyExpensesReportAsync(DateTime startDate)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        const string sql = @"
            SELECT e.*, ec.Name as CategoryName, u.FirstName, u.LastName
            FROM Expenses e
            INNER JOIN ExpenseCategories ec ON e.ExpenseCategoryId = ec.Id
            INNER JOIN Users u ON e.UserId = u.Id
            WHERE e.ExpenseDate >= @StartDate 
            AND e.ExpenseDate < DATEADD(WEEK, 1, @StartDate)
            ORDER BY e.ExpenseDate DESC";

        var expenses = await connection.QueryAsync<ExpenseDto>(sql, new { StartDate = startDate });
        return expenses;
    }

    public async Task<IEnumerable<ExpenseDto>> GetCompanyMonthlyExpensesReportAsync(int year, int month)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        const string sql = @"
            SELECT e.*, ec.Name as CategoryName, u.FirstName, u.LastName
            FROM Expenses e
            INNER JOIN ExpenseCategories ec ON e.ExpenseCategoryId = ec.Id
            INNER JOIN Users u ON e.UserId = u.Id
            WHERE YEAR(e.ExpenseDate) = @Year 
            AND MONTH(e.ExpenseDate) = @Month
            ORDER BY e.ExpenseDate DESC";

        var expenses = await connection.QueryAsync<ExpenseDto>(sql, new { Year = year, Month = month });
        return expenses;
    }

    public async Task<IEnumerable<ExpenseDto>> GetApprovedExpensesReportAsync(DateTime startDate, DateTime endDate)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        const string sql = @"
            SELECT e.*, ec.Name as CategoryName, u.FirstName, u.LastName
            FROM Expenses e
            INNER JOIN ExpenseCategories ec ON e.ExpenseCategoryId = ec.Id
            INNER JOIN Users u ON e.UserId = u.Id
            WHERE e.Status = @Status
            AND e.ExpenseDate BETWEEN @StartDate AND @EndDate
            ORDER BY e.ExpenseDate DESC";

        var expenses = await connection.QueryAsync<ExpenseDto>(sql, 
            new { Status = ExpenseStatus.Approved, StartDate = startDate, EndDate = endDate });
        return expenses;
    }

    public async Task<IEnumerable<ExpenseDto>> GetRejectedExpensesReportAsync(DateTime startDate, DateTime endDate)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        const string sql = @"
            SELECT e.*, ec.Name as CategoryName, u.FirstName, u.LastName
            FROM Expenses e
            INNER JOIN ExpenseCategories ec ON e.ExpenseCategoryId = ec.Id
            INNER JOIN Users u ON e.UserId = u.Id
            WHERE e.Status = @Status
            AND e.ExpenseDate BETWEEN @StartDate AND @EndDate
            ORDER BY e.ExpenseDate DESC";

        var expenses = await connection.QueryAsync<ExpenseDto>(sql, 
            new { Status = ExpenseStatus.Rejected, StartDate = startDate, EndDate = endDate });
        return expenses;
    }
} 