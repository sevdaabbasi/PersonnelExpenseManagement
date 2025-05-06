namespace PersonnelExpenseManagement.Application.DTOs.Report;

public class CompanyReportDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<UserReportDto> UserReports { get; set; } = new();
    public decimal TotalAmount => UserReports.Sum(r => r.Amount);
    public int TotalCount => UserReports.Sum(r => r.Count);
    public int ApprovedCount => UserReports.Sum(r => r.ApprovedCount);
    public int RejectedCount => UserReports.Sum(r => r.RejectedCount);
    public int PendingCount => UserReports.Sum(r => r.PendingCount);
    public decimal ApprovedAmount => UserReports.Sum(r => r.ApprovedAmount);
    public decimal RejectedAmount => UserReports.Sum(r => r.RejectedAmount);
    public decimal PendingAmount => UserReports.Sum(r => r.PendingAmount);
}

public class UserReportDto
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Amount { get; set; }
    public int ApprovedCount { get; set; }
    public int RejectedCount { get; set; }
    public int PendingCount { get; set; }
    public decimal ApprovedAmount { get; set; }
    public decimal RejectedAmount { get; set; }
    public decimal PendingAmount { get; set; }
} 