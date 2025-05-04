using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelExpenseManagement.Application.DTOs.Expense;
using PersonnelExpenseManagement.Domain.Entities;

namespace PersonnelExpenseManagement.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
   
    [HttpGet]
    [Authorize]
    public IActionResult GetAll()
    {
        return Ok(new List<Expense>());
    }

  
    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetById(int id)
    {
        return Ok(new Expense());
    }


    [HttpPost]
    [Authorize]
    public IActionResult Create([FromBody] CreateExpenseDto createExpenseDto)
    {
        // TODO: Implement expense creation logic
        return CreatedAtAction(nameof(GetById), new { id = 1 }, createExpenseDto);
    }


    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(int id, [FromBody] Expense expense)
    {
        return NoContent();
    }


    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        return NoContent();
    }
} 