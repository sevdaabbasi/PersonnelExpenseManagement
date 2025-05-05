using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelExpenseManagement.Application.DTOs.Expense;
using PersonnelExpenseManagement.Application.Interfaces;
using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService ?? throw new ArgumentNullException(nameof(expenseService));
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseDto dto)
    {
        var userId = User.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var expense = await _expenseService.CreateExpenseAsync(userId, dto);
        return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserExpenses()
    {
        var userId = User.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var expenses = await _expenseService.GetUserExpensesAsync(userId);
        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpense(string id)
    {
        var expense = await _expenseService.GetExpenseByIdAsync(id);
        return Ok(expense);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/approve")]
    public async Task<IActionResult> ApproveExpense(string id)
    {
        var approvedBy = User.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(approvedBy))
        {
            return Unauthorized();
        }

        await _expenseService.ApproveExpenseAsync(id, approvedBy);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/reject")]
    public async Task<IActionResult> RejectExpense(string id, [FromBody] string rejectionReason)
    {
        await _expenseService.RejectExpenseAsync(id, rejectionReason);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingExpenses()
    {
        var expenses = await _expenseService.GetPendingExpensesAsync();
        return Ok(expenses);
    }
} 