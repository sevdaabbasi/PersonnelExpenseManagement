namespace PersonnelExpenseManagement.Application.DTOs.Report;

public class WeeklyReportDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<DailyReportDto> DailyReports { get; set; } = new();
    public decimal TotalAmount => DailyReports.Sum(r => r.TotalAmount);
    public int TotalCount => DailyReports.Sum(r => r.TotalCount);
    public int ApprovedCount => DailyReports.Sum(r => r.ApprovedCount);
    public int RejectedCount => DailyReports.Sum(r => r.RejectedCount);
    public int PendingCount => DailyReports.Sum(r => r.PendingCount);
    public decimal ApprovedAmount => DailyReports.Sum(r => r.ApprovedAmount);
    public decimal RejectedAmount => DailyReports.Sum(r => r.RejectedAmount);
    public decimal PendingAmount => DailyReports.Sum(r => r.PendingAmount);
} 