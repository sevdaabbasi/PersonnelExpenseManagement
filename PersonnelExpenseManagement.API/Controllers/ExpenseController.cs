using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelExpenseManagement.Application.DTOs.Expense;
using PersonnelExpenseManagement.Application.Interfaces;
using PersonnelExpenseManagement.Domain.Entities;
using System.Security.Claims;

namespace PersonnelExpenseManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseDto>> CreateExpense([FromBody] CreateExpenseDto dto)
    {
        if (dto == null) return BadRequest("Request cannot be null");

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var result = await _expenseService.CreateExpenseAsync(dto, userId);
        return CreatedAtAction(nameof(GetExpense), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseDto>> GetExpense(int id)
    {
        if (id <= 0) return BadRequest("Invalid ID");
        var expense = await _expenseService.GetExpenseByIdAsync(id);
        return Ok(expense);
    }

    [HttpGet("my")]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetMyExpenses()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var expenses = await _expenseService.GetExpensesByUserIdAsync(userId);
        return Ok(expenses);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAllExpenses()
    {
        var expenses = await _expenseService.GetExpensesByUserIdAsync(null);
        return Ok(expenses);
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ExpenseDto>> UpdateStatus(int id, [FromBody] UpdateExpenseStatusDto dto)
    {
        if (id <= 0) return BadRequest("Invalid ID");
        if (dto == null) return BadRequest("Request cannot be null");

        var result = await _expenseService.UpdateExpenseStatusAsync(id, dto.Status, dto.RejectionReason);
        return Ok(result);
    }
} 