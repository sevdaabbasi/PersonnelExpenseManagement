namespace PersonnelExpenseManagement.Application.DTOs.Report;

public class PersonalReportDto
{
    public string UserId { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<ExpenseDetailDto> Expenses { get; set; } = new();
    public decimal TotalAmount => Expenses.Sum(e => e.Amount);
    public int TotalCount => Expenses.Count;
    public int ApprovedCount => Expenses.Count(e => e.Status == "Approved");
    public int RejectedCount => Expenses.Count(e => e.Status == "Rejected");
    public int PendingCount => Expenses.Count(e => e.Status == "Pending");
    public decimal ApprovedAmount => Expenses.Where(e => e.Status == "Approved").Sum(e => e.Amount);
    public decimal RejectedAmount => Expenses.Where(e => e.Status == "Rejected").Sum(e => e.Amount);
    public decimal PendingAmount => Expenses.Where(e => e.Status == "Pending").Sum(e => e.Amount);
}

public class ExpenseDetailDto
{
    public string Id { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
} 