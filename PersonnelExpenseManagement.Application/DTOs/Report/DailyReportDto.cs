namespace PersonnelExpenseManagement.Application.DTOs.Report;

public class DailyReportDto
{
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalCount { get; set; }
    public int ApprovedCount { get; set; }
    public int RejectedCount { get; set; }
    public int PendingCount { get; set; }
    public decimal ApprovedAmount { get; set; }
    public decimal RejectedAmount { get; set; }
    public decimal PendingAmount { get; set; }
    public List<ExpenseSummaryDto> Expenses { get; set; } = new();
}

public class ExpenseSummaryDto
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
} 