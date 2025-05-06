using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelExpenseManagement.Application.DTOs.Report;
using PersonnelExpenseManagement.Application.Interfaces;

namespace PersonnelExpenseManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("daily")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<DailyReportDto>> GetDailyReport([FromQuery] DateTime date)
    {
        var report = await _reportService.GetDailyReportAsync(date);
        return Ok(report);
    }

    [HttpGet("weekly")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<WeeklyReportDto>> GetWeeklyReport([FromQuery] DateTime startDate)
    {
        var report = await _reportService.GetWeeklyReportAsync(startDate);
        return Ok(report);
    }

    [HttpGet("monthly")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<MonthlyReportDto>> GetMonthlyReport([FromQuery] int year, [FromQuery] int month)
    {
        var report = await _reportService.GetMonthlyReportAsync(year, month);
        return Ok(report);
    }

    [HttpGet("personal")]
    public async Task<ActionResult<PersonalReportDto>> GetPersonalReport(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var userId = User.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var report = await _reportService.GetPersonalReportAsync(userId, startDate, endDate);
        return Ok(report);
    }

    [HttpGet("company")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CompanyReportDto>> GetCompanyReport(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var report = await _reportService.GetCompanyReportAsync(startDate, endDate);
        return Ok(report);
    }
} 