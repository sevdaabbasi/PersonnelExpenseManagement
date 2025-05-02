using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelExpenseManagement.Application.DTOs.Expense;
using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.API.Controllers;

/// <summary>
/// Controller for managing expenses
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    /// <summary>
    /// Gets all expenses
    /// </summary>
    /// <returns>List of expenses</returns>
    [HttpGet]
    [Authorize]
    public IActionResult GetAll()
    {
        return Ok(new List<Expense>());
    }

    /// <summary>
    /// Gets an expense by ID
    /// </summary>
    /// <param name="id">The expense ID</param>
    /// <returns>The expense</returns>
    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetById(int id)
    {
        return Ok(new Expense());
    }

    /// <summary>
    /// Creates a new expense
    /// </summary>
    /// <param name="createExpenseDto">The expense data to create</param>
    /// <returns>The created expense</returns>
    [HttpPost]
    [Authorize]
    public IActionResult Create([FromBody] CreateExpenseDto createExpenseDto)
    {
        // TODO: Implement expense creation logic
        return CreatedAtAction(nameof(GetById), new { id = 1 }, createExpenseDto);
    }

    /// <summary>
    /// Updates an existing expense
    /// </summary>
    /// <param name="id">The expense ID</param>
    /// <param name="expense">The updated expense</param>
    /// <returns>No content</returns>
    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(int id, [FromBody] Expense expense)
    {
        return NoContent();
    }

    /// <summary>
    /// Deletes an expense
    /// </summary>
    /// <param name="id">The expense ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        return NoContent();
    }
} 